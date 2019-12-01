using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Common;
using WinDivertLoader.Properties;

namespace WinDivertLoader
{
    unsafe class Program
    {
        #region Filename Constants
        private const string WinDivertDLLFileName = "WinDivert.dll";
        private const string WinDivertDriverFileName32 = "WinDivert32.sys";
        private const string WinDivertDriverFileName64 = "WinDivert64.sys";
        //private const string WinDivertDriverInstallerFileName = "WdfCoInstaller01009.dll";
        //private const string WinDivertINFFileName = "WinDivert.inf";
        #endregion

        static void Main(string[] args)
        {
            // Set Current Directory to EXE Path
            string currentDir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            if (!String.IsNullOrWhiteSpace(currentDir))
            {
                Environment.CurrentDirectory = currentDir;
            }

            // Check Arguments
            string ipAddress;
            if (args.Length == 0)
            {
                Console.WriteLine("WinDivert did not receive an IP Address argument!");
                Console.WriteLine("Using default IP Address.");
                ipAddress = "10.3.0.1";
            }
            else if (!(Regex.IsMatch(args[0], @"^[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}$")))
            {
                Console.WriteLine("WinDivert did not receive a valid IP Address argument!");
                return;
            }
            else
            {
                ipAddress = args[0];
            }

            // Create WinDivert Files
            if (!Environment.Is64BitProcess)
            {
                // x86 DLL
                if (File.Exists(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + WinDivertDLLFileName) == false)
                {
                    CommonUtilities.FileCreate(WinDivertDLLFileName, Resources.WinDivertDLL, Directory.GetCurrentDirectory());
                }
                // x86 Driver
                if (File.Exists(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + WinDivertDriverFileName32) == false)
                {
                    CommonUtilities.FileCreate(WinDivertDriverFileName32, Resources.WinDivertDriver, Directory.GetCurrentDirectory());
                }
            }
            else
            {
                // x64 DLL
                if (File.Exists(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + WinDivertDLLFileName) == false)
                {
                    CommonUtilities.FileCreate(WinDivertDLLFileName, Resources.WinDivertDLL64, Directory.GetCurrentDirectory());
                }
                // x64 Driver
                if (File.Exists(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + WinDivertDriverFileName64) == false)
                {
                    CommonUtilities.FileCreate(WinDivertDriverFileName64, Resources.WinDivertDriver64, Directory.GetCurrentDirectory());
                }
            }

            // Load WinDivert Library
            IntPtr dllHandle = NativeMethods.LoadLibrary(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + WinDivertDLLFileName);

            // Start WinDivert Client
            const int bufferLen = 65535;
            IntPtr packet = Marshal.AllocHGlobal(bufferLen);
            try
            {
                IntPtr handle = NativeMethods.DivertOpen(string.Format("ip.SrcAddr == {0} or ip.DstAddr == {0}", ipAddress), DivertStructs.DivertLayer.DivertLayerNetwork, -101, 0);
                if (handle == new IntPtr(-1))
                {
                    Console.WriteLine("Failed to open the WinDivert device!");
                }
                else
                {
                    try
                    {
                        // Packet Redirection Loop
                        while (true)
                        {
                            // Process Packet
                            int packetLen;
                            DivertStructs.DivertAddress addr;
                            if (!NativeMethods.DivertRecv(handle, packet, bufferLen, &addr, out packetLen))
                            {
                                Console.WriteLine("WinDivert failed to redirect packet; divert failed!");
                                //return;
                            }

                            // Get Packet header
                            DivertStructs.DivertIPHdr* ipHeader;
                            NativeMethods.DivertHelperParsePacket(packet, packetLen, out ipHeader, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

                            // Reverse IP Addresses
                            if (ipHeader != null)
                            {
                                UInt32 tmpAddress = ipHeader->DstAddr;
                                ipHeader->DstAddr = ipHeader->SrcAddr;
                                ipHeader->SrcAddr = tmpAddress;

                                if (addr.Direction == 0)
                                    addr.Direction = 1;
                                else
                                    addr.Direction = 0;
                            }

                            // Resend Packet
                            NativeMethods.DivertHelperCalcChecksums(packet, packetLen, 0);
                            if (!NativeMethods.DivertSend(handle, packet, packetLen, &addr, IntPtr.Zero))
                            {
                                Console.WriteLine("WinDivert failed to redirect packet; injection failed");
                                //return;
                            }
                        }
                    }
                    finally
                    {
                        NativeMethods.DivertClose(handle);
                    }
                }
            }
            finally
            {
                Marshal.FreeHGlobal(packet);

                // Unload WinDivert Library
                NativeMethods.FreeLibrary(dllHandle);
            }
        }
    }
}

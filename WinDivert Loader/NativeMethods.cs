using System;
using System.Runtime.InteropServices;

namespace WinDivertLoader
{
    internal static class NativeMethods
    {
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FreeLibrary(IntPtr hModule);

        #region WinDivert Control Functions
        [DllImport("WinDivert.dll", EntryPoint = "WinDivertHelperCalcChecksums")]
        public static extern uint DivertHelperCalcChecksums(IntPtr pPacket, int packetLen, ulong flags);

        [DllImport("WinDivert.dll", EntryPoint = "WinDivertHelperParsePacket")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static unsafe extern bool DivertHelperParsePacket([In] IntPtr pPacket, int packetLen, out DivertStructs.DivertIPHdr* ipHeader, IntPtr ppIpv6Hdr, IntPtr ppIcmpHdr, IntPtr ppIcmpv6Hdr, IntPtr ppTcpHdr, IntPtr ppUdpHdr, IntPtr ppData, IntPtr pDataLen);

        [DllImport("WinDivert.dll", EntryPoint = "WinDivertOpen")]
        public static extern IntPtr DivertOpen([In] [MarshalAs(UnmanagedType.LPStr)] string filter, DivertStructs.DivertLayer layer, short priority, ulong flags);

        [DllImport("WinDivert.dll", EntryPoint = "WinDivertRecv")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static unsafe extern bool DivertRecv([In] IntPtr handle, IntPtr pPacket, int packetLen, [Out] DivertStructs.DivertAddress* pAddr, out int readLen);

        [DllImport("WinDivert.dll", EntryPoint = "WinDivertSend")]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static unsafe extern bool DivertSend([In] IntPtr handle, [In] IntPtr pPacket, int packetLen, [In] DivertStructs.DivertAddress* pAddr, IntPtr writeLen);

        [DllImport("WinDivert.dll", EntryPoint = "WinDivertClose")]
        public static extern void DivertClose(IntPtr handle);
        #endregion
    }

    internal static class DivertStructs
    {
        // Divert Address
        [StructLayout(LayoutKind.Sequential)]
        public struct DivertAddress
        {
            public uint IfIdx;
            public uint SubIfIdx;
            public byte Direction;
        }

        // Divert IP Header
        [StructLayout(LayoutKind.Sequential)]
        public struct DivertIPHdr
        {
            public byte bitvector1;
            public byte TOS;
            public ushort Length;
            public ushort Id;
            public ushort FragOff0;
            public byte TTL;
            public byte Protocol;
            public ushort Checksum;
            public uint SrcAddr;
            public uint DstAddr;

            /*
            public uint HdrLength
            {
                get
                {
                    return bitvector1 & 4u;
                }
                set
                {
                    bitvector1 = value | bitvector1;
                }
            }
            public uint Version
            {
                get
                {
                    return (bitvector1 & 240u) / 16;
                }
                set
                {
                    bitvector1 = (value * 16) | bitvector1;
                }
            }
            */
        }

        // Divert IPV6 Header
        [StructLayout(LayoutKind.Sequential)]
        public struct DivertIPV6Hdr
        {
            public uint bitvector1;
            public ushort FlowLabel1;
            public ushort Length;
            public byte NextHdr;
            public byte HopLimit;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
            public uint[] SrcAddr;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
            public uint[] DstAddr;

            public uint TrafficClass0
            {
                get
                {
                    return bitvector1 & 15u;
                }
                set
                {
                    bitvector1 = value | bitvector1;
                }
            }
            public uint Version
            {
                get
                {
                    return (bitvector1 & 240u) / 16;
                }
                set
                {
                    bitvector1 = (value * 16) | bitvector1;
                }
            }
            public uint FlowLabel0
            {
                get
                {
                    return (bitvector1 & 3840u) / 256;
                }
                set
                {
                    bitvector1 = (value * 256) | bitvector1;
                }
            }
            public uint TrafficClass1
            {
                get
                {
                    return (bitvector1 & 61440u) / 4096;
                }
                set
                {
                    bitvector1 = (value * 4096) | bitvector1;
                }
            }
        }

        // Divert ICMP Header
        [StructLayout(LayoutKind.Sequential)]
        public struct DivertICMPHdr
        {
            public byte Type;
            public byte Code;
            public ushort Checksum;
            public uint Body;
        }

        // Divert ICMPV6 Header
        [StructLayout(LayoutKind.Sequential)]
        public struct DivertICMPV6Hdr
        {
            public byte Type;
            public byte Code;
            public ushort Checksum;
            public uint Body;
        }

        // Divert TCP Header
        [StructLayout(LayoutKind.Sequential)]
        public struct DivertTCPHdr
        {
            public ushort SrcPort;
            public ushort DstPort;
            public uint SeqNum;
            public uint AckNum;
            public uint bitvector1;
            public ushort Window;
            public ushort Checksum;
            public ushort UrgPtr;

            public uint Reserved1
            {
                get
                {
                    return bitvector1 & 15u;
                }
                set
                {
                    bitvector1 = value | bitvector1;
                }
            }
            public uint HdrLength
            {
                get
                {
                    return (bitvector1 & 240u) / 16;
                }
                set
                {
                    bitvector1 = (value * 16) | bitvector1;
                }
            }
            public uint Fin
            {
                get
                {
                    return (bitvector1 & 256u) / 256;
                }
                set
                {
                    bitvector1 = (value * 256) | bitvector1;
                }
            }
            public uint Syn
            {
                get
                {
                    return (bitvector1 & 512u) / 512;
                }
                set
                {
                    bitvector1 = (value * 512) | bitvector1;
                }
            }
            public uint Rst
            {
                get
                {
                    return (bitvector1 & 1024u) / 1024;
                }
                set
                {
                    bitvector1 = (value * 1024) | bitvector1;
                }
            }
            public uint Psh
            {
                get
                {
                    return (bitvector1 & 2048u) / 2048;
                }
                set
                {
                    bitvector1 = (value * 2048) | bitvector1;
                }
            }
            public uint Ack
            {
                get
                {
                    return (bitvector1 & 4096u) / 4096;
                }
                set
                {
                    bitvector1 = (value * 4096) | bitvector1;
                }
            }
            public uint Urg
            {
                get
                {
                    return (bitvector1 & 8192u) / 8192;
                }
                set
                {
                    bitvector1 = (value * 8192) | bitvector1;
                }
            }
            public uint Reserved2
            {
                get
                {
                    return (bitvector1 & 49152u) / 16384;
                }
                set
                {
                    bitvector1 = (value * 16384) | bitvector1;
                }
            }
        }

        // Divert UDP Header
        [StructLayout(LayoutKind.Sequential)]
        public struct DivertUDPHdr
        {
            public ushort SrcPort;
            public ushort DstPort;
            public ushort Length;
            public ushort Checksum;
        }

        // Divert Layers
        public enum DivertLayer
        {
            DivertLayerNetwork = 0,
            DivertLayerNetworkForward = 1
        }
    }
}
using System;
using System.Runtime.InteropServices;

namespace Keys
{
    internal static class NativeMethods
    {
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("PidGenX.dll", EntryPoint = "PidGenX", CharSet = CharSet.Unicode)]
        internal static extern int PidGenX(string productKey, string pkeyPath, string mspid, int unknownUsage, IntPtr productID, IntPtr digitalProductID, IntPtr digitalProductID4);
		
		[DllImport("kernel32")]
        internal static extern uint EnumSystemFirmwareTables(uint firmwareTableProviderSignature, IntPtr pFirmwareTableBuffer, uint bufferSize);
		
        [DllImport("kernel32")]
        internal static extern uint GetSystemFirmwareTable(uint firmwareTableProviderSignature, uint firmwareTableID, IntPtr pFirmwareTableBuffer, uint bufferSize);
    }
}
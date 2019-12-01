using System;
using System.Runtime.InteropServices;

namespace Common
{
    static class NativeMethods
    {
        [DllImport("advapi32.dll", EntryPoint = "OpenSCManagerW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr OpenSCManager(string machineName, string databaseName, uint dwAccess);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern IntPtr CreateService
        (
            IntPtr hSCManager,
            string lpServiceName,
            string lpDisplayName,
            uint dwDesiredAccess,
            uint dwServiceType,
            uint dwStartType,
            uint dwErrorControl,
            string lpBinaryPathName,
            string lpLoadOrderGroup,
            string lpdwTagId,
            string lpDependencies,
            string lpServiceStartName,
            string lpPassword
        );

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseServiceHandle(IntPtr hSCObject);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern IntPtr OpenService(IntPtr hSCManager, string lpServiceName, uint dwDesiredAccess);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteService(IntPtr hService);

        #region Constants
        internal const int GenericWrite = 0x40000000;
        internal const int Delete = 0x10000;
        internal const int ScManagerCreateService = 0x0002;
        internal const int ServiceWin32OwnProcess = 0x00000010;
        internal const int ServiceErrorNormal = 0x00000001;
        internal const int StandardRightsRequired = 0xF0000;
        internal const int ServiceQueryConfig = 0x0001;
        internal const int ServiceChangeConfig = 0x0002;
        internal const int ServiceQueryStatus = 0x0004;
        internal const int ServiceEnumerateDependents = 0x0008;
        internal const int ServiceStart = 0x0010;
        internal const int ServiceStop = 0x0020;
        internal const int ServicePauseContinue = 0x0040;
        internal const int ServiceInterrogate = 0x0080;
        internal const int ServiceUserDefinedControl = 0x0100;
        internal const int ServiceAllAccess = (StandardRightsRequired |
        ServiceQueryConfig |
        ServiceChangeConfig |
        ServiceQueryStatus |
        ServiceEnumerateDependents |
        ServiceStart |
        ServiceStop |
        ServicePauseContinue |
        ServiceInterrogate |
        ServiceUserDefinedControl);
        #endregion
    }
}

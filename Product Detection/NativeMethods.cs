using System.Runtime.InteropServices;

namespace ProductDetection
{
    /// <summary>
    /// Options for IsOS() function
    /// </summary>
    public enum OSType : uint
    {
        /// <summary>
        /// The program is running on one of the following versions of Windows:
        ///    * Windows 95
        ///    * Windows 98
        ///    * Windows Me
        /// Equivalent to PlatformID.Win32Windows. Note that none of those systems
        /// are supported at this time. IsOS(Windows) returns false on all supported
        /// systems.
        /// </summary>
        Windows = 0,

        /// <summary>
        /// Always returns true
        /// </summary>
        NT = 1,

        /// <summary>
        /// Always returns false
        /// </summary>
        Win95OrGreater = 2,

        /// <summary>
        /// Always returns false
        /// </summary>
        NT4OrGreater = 3,

        /// <summary>
        /// Always returns false
        /// </summary>
        Win98OrGreater = 5,

        /// <summary>
        /// Always returns false
        /// </summary>
        Win98Gold = 6,

        /// <summary>
        /// The program is running on Windows 2000 or one of its successors.
        /// </summary>
        Win2000OrGreater = 7,

        /// <summary>
        /// Do not use; use Professional.
        /// </summary>
        Win2000Pro = 8,

        /// <summary>
        /// Do not use; use Server.
        /// </summary>
        Win2000Server = 9,

        /// <summary>
        /// Do not use; use AdvancedServer.
        /// </summary>
        Win2000AdvancedServer = 10,

        /// <summary>
        /// Do not use; use DataCenter.
        /// </summary>
        Win2000DataCenter = 11,

        /// <summary>
        /// The program is running on Windows 2000 Terminal Server in either Remote
        /// Administration mode or Application Server mode, or Windows Server 2003 (or
        /// one of its successors) in Terminal Server mode or Remote Desktop for
        /// Administration mode. Consider using a more specific value such as
        /// TerminalServer, TerminalRemoteAdmin, or PersonalTerminalServer.
        /// </summary>
        Win2000Terminal = 12,

        /// <summary>
        /// The program is running on Windows Embedded, any version. Equivalent to
        /// SuiteMask.EmbeddedNT.
        /// </summary>
        Embedded = 13,

        /// <summary>
        /// The program is running as a Terminal Server client. Equivalent to
        /// GetSystemMetrics(SM_REMOTESESSION).
        /// </summary>
        TerminalClient = 14,

        /// <summary>
        /// The program is running on Windows 2000 Terminal Server in the Remote
        /// Administration mode or Windows Server 2003 (or one of its successors) in
        /// the Remote Desktop for Administration mode (these are the default
        /// installation modes). This is equivalent to SuiteMask.Terminal &&
        /// SuiteMask.SingleUserTerminalServer.
        /// </summary>
        TerminalRemoteAdmin = 15,

        /// <summary>
        /// Always returns false.
        /// </summary>
        Win95Gold = 16,

        /// <summary>
        /// Always returns false.
        /// </summary>
        MEOrGreater = 17,

        /// <summary>
        /// Always returns false.
        /// </summary>
        XPOrGreater = 18,

        /// <summary>
        /// Always returns false.
        /// </summary>
        Home = 19,

        /// <summary>
        /// The program is running on Windows NT Workstation or Windows 2000 (or one of
        /// its successors) Professional. Equivalent to PlatformID.Win32NT &&
        /// ProductType.NTWorkstation.
        /// </summary>
        Professional = 20,

        /// <summary>
        /// The program is running on Windows Datacenter Server or Windows Server
        /// Datacenter Edition, any version. Equivalent to (ProductType.NTServer ||
        /// ProductType.NTDomainController) && SuiteMask.DataCenter.
        /// </summary>
        DataCenter = 21,

        /// <summary>
        /// The program is running on Windows Advanced Server or Windows Server
        /// Enterprise Edition, any version. Equivalent to (ProductType.NTServer ||
        /// ProductType.NTDomainController) && SuiteMask.Enterprise &&
        /// !SuiteMask.DataCenter.
        /// </summary>
        AdvancedServer = 22,

        /// <summary>
        /// The program is running on Windows Server (Standard) or Windows Server
        /// Standard Edition, any version. This value will not return true for
        /// SuiteMask.DataCenter, SuiteMask.Enterprise, SuiteMask.SmallBusiness, or
        /// SuiteMask.SmallBusinessRestricted.
        /// </summary>
        Server = 23,

        /// <summary>
        /// The program is running on Windows 2000 Terminal Server in Application
        /// Server mode, or on Windows Server 2003 (or one of its successors) in
        /// Terminal Server mode. This is equivalent to SuiteMask.Terminal &&
        /// SuiteMask.SingleUserTerminalServer.
        /// </summary>
        TerminalServer = 24,

        /// <summary>
        /// The program is running on Windows XP (or one of its successors), Home
        /// Edition or Professional. This is equivalent to
        /// SuiteMask.SingleUserTerminalServer && !SuiteMask.Terminal.
        /// </summary>
        PersonalTerminalServer = 25,

        /// <summary>
        /// Fast user switching is enabled.
        /// </summary>
        FastUserSwitching = 26,

        /// <summary>
        /// Always returns false.
        /// </summary>
        WelcomeLogonUI = 27,

        /// <summary>
        /// The computer is joined to a domain.
        /// </summary>
        DomainMember = 28,

        /// <summary>
        /// The program is running on any Windows Server product. Equivalent to
        /// ProductType.NTServer || ProductType.NTDomainController.
        /// </summary>
        AnyServer = 29,

        /// <summary>
        /// The program is a 32-bit program running on 64-bit Windows.
        /// </summary>
        WOW6432 = 30,

        /// <summary>
        /// Always returns false.
        /// </summary>
        WebServer = 31,

        /// <summary>
        /// The program is running on Microsoft Small Business Server with restrictive
        /// client license in force. Equivalent to SuiteMask.SmallBusinessRestricted.
        /// </summary>
        SmallBusinessServer = 32,

        /// <summary>
        /// The program is running on Windows XP Tablet PC Edition, or one of its
        /// successors.
        /// </summary>
        TabletPC = 33,

        /// <summary>
        /// The user should be presented with administrator UI. It is possible to have
        /// server administrative UI on a non-server machine. This value informs the
        /// application that an administrator's profile has roamed to a non-server, and
        /// UI should be appropriate to an administrator. Otherwise, the user is shown
        /// a mix of administrator and nonadministrator settings.
        /// </summary>
        ServerAdminUI = 34,

        /// <summary>
        /// The program is running on Windows XP Media Center Edition, or one of its
        /// successors. Equivalent to GetSystemMetrics(SM_MEDIACENTER).
        /// </summary>
        MediaCenter = 35,

        /// <summary>
        /// The program is running on Windows Appliance Server.
        /// </summary>
        Appliance = 36,
    }

    internal static class NativeMethods
    {
        [DllImport("shlwapi.dll", SetLastError = true, EntryPoint = "#437")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsOS(OSType dwOS);
    }
}
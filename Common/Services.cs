using System;
using System.Management;
using System.Security;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Win32;
using ProductDetection;

namespace Common
{
    /// <summary>
    /// Group of Utility Methods for Services
    /// </summary>
    public static class Services
    {
        /// <summary>
        /// Start the Microsoft Office Software Licensing Service
        /// </summary>
        public static void StartOSPPSVC()
        {
            switch (OfficeVersion.GetOfficeNumber())
            {
                case 14:
                case 15:
                    EnableService("OSPPSVC");
                    StartService("OSPPSVC");
                    break;
            }
        }

        /// <summary>
        /// Stop the Microsoft Office Software Licensing Service
        /// </summary>
        public static void StopOSPPSVC()
        {
            switch (OfficeVersion.GetOfficeNumber())
            {
                case 14:
                case 15:
                    StopService("OSPPSVC");
                    DisableService("OSPPSVC");
                    break;
            }
        }

        /// <summary>
        /// Start the Microsoft Windows Software Licensing Service
        /// </summary>
        public static void StartSPPSVC()
        {
            if (OSVersion.GetWindowsNumber() >= 6.1)
            {
                EnableService("SPPSVC");
                StartService("SPPSVC");
            }
            else if (Math.Abs(OSVersion.GetWindowsNumber() - 6.0) < Double.Epsilon)
            {
                EnableService("SLSVC");
                StartService("SLSVC");
            }
        }

        /// <summary>
        /// Stop the Microsoft Windows Software Licensing Service
        /// </summary>
        public static void StopSPPSVC()
        {
            if (OSVersion.GetWindowsNumber() >= 6.1)
            {
                StopService("SPPSVC");
                DisableService("SPPSVC");
            }
            else if (Math.Abs(OSVersion.GetWindowsNumber() - 6.0) < Double.Epsilon)
            {
                StopService("SLSVC");
                DisableService("SLSVC");
            }
        }

        /// <summary>
        /// Start Any Service
        /// </summary>
        /// <param name="serviceName">Name of the Service to start</param>
        public static void StartService(string serviceName)
        {
            using (ServiceController sc = new ServiceController(serviceName))
            {
                if (sc.Status == ServiceControllerStatus.Stopped)
                {
                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 0, 20));
                }
            }
        }

        /// <summary>
        /// Stop Any Service
        /// </summary>
        /// <param name="serviceName">Name of the Service to stop</param>
        /// <param name="processName">Process Name of the Service, if different from Service Name </param>
        public static void StopService(string serviceName, string processName = "")
        {
            using (ServiceController sc = new ServiceController(serviceName))
            {
                if (sc.Status == ServiceControllerStatus.Running)
                {
                    try
                    {
                        sc.Stop();
                        sc.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 0, 10));

                        // Wait and Kill Process for Good Measure
                        Thread.Sleep(5000);
                        if (!String.IsNullOrWhiteSpace(processName))
                        {
                            CommonUtilities.KillProcess(processName);
                        }
                        else
                        {
                            CommonUtilities.KillProcess(serviceName);
                        }
                    }
                    catch (System.ServiceProcess.TimeoutException)
                    {
                        // Force Stop by Killing Process
                        if (sc.Status == ServiceControllerStatus.Running)
                        {
                            if (!String.IsNullOrWhiteSpace(processName))
                            {
                                CommonUtilities.KillProcess(processName);
                            }
                            else
                            {
                                CommonUtilities.KillProcess(serviceName);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Disable Any Service
        /// </summary>
        /// <param name="serviceName">Name of the Service to Disable</param>
        public static void DisableService(string serviceName)
        {
            // Disable Service
            using (RegistryKey service = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\" + serviceName, true))
            {
                if (service != null)
                {
                    service.SetValue("Start", 4);
                }
            }
        }

        /// <summary>
        /// Check if Service is Disabled
        /// </summary>
        /// <param name="serviceName">Name of the Service to start</param>
        /// <returns>True if the Service is Disabled, False if the Service is Enabled</returns>
        public static bool IsServiceDisabled(string serviceName)
        {
            string wmiQuery = @"SELECT * FROM Win32_Service WHERE Name='" + serviceName + @"'";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wmiQuery);
            ManagementObjectCollection results = searcher.Get();
            foreach (ManagementBaseObject service in results)
            {
                if (service["StartMode"].ToString() == "Disabled")
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Enable Any Service
        /// </summary>
        /// <param name="serviceName">Name of the Service to Enable</param>
        /// <param name="startType">Start Type of Service. 0 = Boot, 1 = System, 2 = Automatic, 3 = Manual</param>
        public static void EnableService(string serviceName, int startType = 2)
        {
            // Enable Service
            using (RegistryKey service = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\" + serviceName, true))
            {
                if (service != null)
                {
                    service.SetValue("Start", startType);
                }
            }
        }

        /// <summary>
        /// Check if Service is Enabled
        /// </summary>
        /// <param name="serviceName">Name of the Service to start</param>
        /// <returns>True if the Service is Enabled, False if the Service is Disabled</returns>
        public static bool IsServiceEnabled(string serviceName)
        {
            string wmiQuery = @"SELECT * FROM Win32_Service WHERE Name='" + serviceName + @"'";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wmiQuery);
            ManagementObjectCollection results = searcher.Get();
            foreach (ManagementBaseObject service in results)
            {
                if (service["StartMode"].ToString() == "Disabled")
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Register a new Service
        /// </summary>
        /// <param name="svcDispName">Display Name of the Service to create</param>
        /// <param name="svcName">Name of the Service to create</param>
        /// <param name="serviceDescription">Description of the Service</param>
        /// <param name="svcPath">Path to the Service Executable</param>
        /// <param name="dwStartType">Start Type of the Service (Boot=0, System=1, Automatic=2, Manual=3, Disabled=4)</param>
        /// <param name="parameters">Command Line Parameters for the Service</param>
        public static void InstallService(string svcDispName, string svcName, string serviceDescription, string svcPath, uint dwStartType, string parameters = "")
        {
            // Create Service
            IntPtr scHandle = NativeMethods.OpenSCManager(null, null, NativeMethods.ScManagerCreateService);
            if (scHandle != IntPtr.Zero)
            {
                IntPtr svHandle = NativeMethods.CreateService(scHandle, svcName, svcDispName, NativeMethods.ServiceAllAccess, NativeMethods.ServiceWin32OwnProcess, dwStartType, NativeMethods.ServiceErrorNormal, svcPath, null, null, null, null, null);
                if (svHandle == IntPtr.Zero)
                {
                    NativeMethods.CloseServiceHandle(scHandle);
                }
                else
                {
                    NativeMethods.CloseServiceHandle(scHandle);
                    NativeMethods.CloseServiceHandle(svHandle);
                }
            }

            // Set Service Description and Parameters
            using (RegistryKey service = Registry.LocalMachine.CreateSubKey(@"SYSTEM\CurrentControlSet\Services\" + svcName))
            {
                if (service != null)
                {
                    service.SetValue("Description", serviceDescription);
                    service.SetValue("ImagePath", (string)service.GetValue("ImagePath") + " " + parameters);
                }
            }
        }

        /// <summary>
        /// Unregister a service
        /// </summary>
        /// <param name="svcName">Service Name to Unregister</param>
        public static void UninstallService(string svcName)
        {
            // Delete Service
            IntPtr scHandle = NativeMethods.OpenSCManager(null, null, NativeMethods.GenericWrite);
            if (scHandle != IntPtr.Zero)
            {
                IntPtr svHandle = NativeMethods.OpenService(scHandle, svcName, NativeMethods.Delete);
                if (svHandle != IntPtr.Zero)
                {
                    NativeMethods.DeleteService(svHandle);
                    NativeMethods.CloseServiceHandle(scHandle);
                    NativeMethods.CloseServiceHandle(svHandle);
                }
            }
        }

        /// <summary>
        /// Check is a Service is Registered
        /// </summary>
        /// <param name="serviceName">Name of the Service to search for</param>
        /// <returns>True if Registered, False if not Registered</returns>
        public static bool IsServiceInstalled(string serviceName)
        {
            foreach (ServiceController controller in ServiceController.GetServices())
            {
                if (controller.ServiceName == serviceName)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Get the Path of a Windows Service Executable
        /// </summary>
        /// <param name="serviceName">Name of the Service to find</param>
        /// <param name="extension">File Extension, such as exe (Default) or sys</param>
        /// <returns>String Path of the Windows Service, Null if not found</returns>
        public static string GetServiceProgramPath(string serviceName, string extension = "exe")
        {
            using (RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(@"System\CurrentControlSet\Services\" + serviceName, true))
            {
                if (registryKey != null)
                {
                    string installPath = (string)registryKey.GetValue("ImagePath");

                    if (String.IsNullOrWhiteSpace(installPath) == false)
                    {
                        return Regex.Split(installPath.Replace("\"", string.Empty), @"\." + extension)[0] + "." + extension;
                    }
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// Get the Parameters of a Windows Service Executable
        /// </summary>
        /// <param name="serviceName">Name of the Service to find</param>
        /// <param name="extension">File Extension, such as exe (Default) or sys</param>
        /// <returns>String Parameters of the Windows Service, Null if not found</returns>
        public static string GetServiceParameters(string serviceName, string extension = "exe")
        {
            using (RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(@"System\CurrentControlSet\Services\" + serviceName, true))
            {
                if (registryKey != null)
                {
                    string installPath = (string)registryKey.GetValue("ImagePath");
                    return installPath.Replace(GetServiceProgramPath(serviceName, extension), "");
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// Set the Parameters of a Windows Service Executable
        /// </summary>
        /// <param name="serviceName">Name of the Service to find</param>
        /// <param name="parameters">Parameters to pass to the Service</param>
        /// <param name="extension">File Extension, such as exe (Default) or sys</param>
        public static void SetServiceParameters(string serviceName, string parameters, string extension = "exe")
        {
            using (RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(@"System\CurrentControlSet\Services\" + serviceName, true))
            {
                if (registryKey != null)
                {
                    registryKey.SetValue("ImagePath", GetServiceProgramPath(serviceName, extension) + " " + parameters);
                }
            }
        }
    }
}
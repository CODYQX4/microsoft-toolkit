using System;
using System.Security.AccessControl;
using System.Security.Principal;
using Common;
using Microsoft.Win32;

namespace ResetRearms
{
    class Program
    {
        static void Main(string[] args)
        {
            // Reload Registry Hive Trick
            CommonUtilities.ExecuteCommand("REG SAVE HKLM\\SYSTEM " + CommonUtilities.EscapePath(Environment.GetEnvironmentVariable("TEMP") + "\\SYSTEM.hiv"), true);
            CommonUtilities.ExecuteCommand("REG RESTORE HKLM\\SYSTEM " + CommonUtilities.EscapePath(Environment.GetEnvironmentVariable("TEMP") + "\\SYSTEM.hiv"), true);
            CommonUtilities.FileDelete(Environment.GetEnvironmentVariable("TEMP") + "\\SYSTEM.hiv");

            // Rename and Delete WPA Key
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM", true))
            {
                if (key != null)
                {
                    // TODO : Powershell Bundled Vista+ ???
                    CommonUtilities.ExecuteCommand(@"POWERSHELL -command rename-item HKLM:\SYSTEM\WPA -NewName WPA_Delete", true);
                    key.DeleteSubKeyTree("WPA_DELETE");
                    key.CreateSubKey("WPA");

                    // Create Default Values
                    CommonUtilities.ExecuteCommand("REG ADD HKLM\\SYSTEM\\WPA\\478C035F-04BC-48C7-B324-2462D786DAD7-5P-9 /t REG_BINARY /ve     /d 20c0c44b5d68c7085f442818128270ea642e5b5dba2d8885a7831a85c3d1ece262b36911a0d5bdb5e55106656d12578af73f942ffe1562ba665b18ce8969bbf74ff8ceacf3c424de22cf36560c8633b92173d5f38abbe0fbe3f408ca314725a94d599a4587daea29aa207b5cefbfcf2361b7a9beeaacc754513fdce82c16828d", true);
                    CommonUtilities.ExecuteCommand("REG ADD HKLM\\SYSTEM\\WPA\\478C035F-04BC-48C7-B324-2462D786DAD7-5P-9 /t REG_BINARY /v Time /d e318ad15241c695f751c6b19fe1ba41cebfb91bf29367de3146d79a76ace067c", true);
                    CommonUtilities.ExecuteCommand("REG ADD HKLM\\SYSTEM\\WPA\\478C035F-04BC-48C7-B324-2462D786DAD7-5P-9 /t REG_DWORD  /v Type /d 2111353691", true);
                    CommonUtilities.ExecuteCommand("REG ADD HKLM\\SYSTEM\\WPA\\8DEC0AF1-0341-4b93-85CD-72606C2DF94C-7P-1 /t REG_BINARY /ve     /d 6979d03b99a73c736882ceadf1a96320c15405927dc0b721f83cad674fb340496d75f608189d84dcd18fdaff8ea3866a3f37edc3d1eb5c0647e97bb7bea79f5dd05a66062fabb480d137cd3623563962e200b1bd42531cc3e6e4c1ffff693a208e9937f7a4d48b7463e68faf0df08811", true);
                    CommonUtilities.ExecuteCommand("REG ADD HKLM\\SYSTEM\\WPA\\8DEC0AF1-0341-4b93-85CD-72606C2DF94C-7P-2 /t REG_BINARY /ve     /d 79e3ad3e68302e43bc97f4aceb98f3e328155f42df9684935abbc4f3c1652637a70f81fdf5f469d8586bf1d1a8ff96af9ead400b1c9d5621f4b4c57ad44fc2b129ca20a19a64bcfc481c52738b876b64", true);
                    CommonUtilities.ExecuteCommand("REG ADD HKLM\\SYSTEM\\WPA\\8DEC0AF1-0341-4b93-85CD-72606C2DF94C-7P-3 /t REG_BINARY /ve     /d 65a7778c965816a2df869e044edf4671f2ac716502d74d5b30c531c1469dc758dc25c2b480393d6fb7336d3d915668a7f05ff3847468228168833bed8947b4bc", true);
                }
            }

            // Set WPA Key Permissions
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\\WPA", true))
            {
                if (key != null)
                {
                    RegistrySecurity acl = new RegistrySecurity();
                    acl.SetAccessRuleProtection(true, false);
                    acl.SetAccessRule(new RegistryAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), RegistryRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
                    key.SetAccessControl(acl);
                }
            }
        }
    }
}

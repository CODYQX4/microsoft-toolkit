using System;
using System.IO;
using System.Text;
using Common;
using OfficeUninstaller.Properties;
using ProductDetection;

namespace OfficeUninstaller
{
    /// <summary>
    /// Group of Methods for Uninstalling Microsoft Office
    /// </summary>
    public static class OfficeUninstaller
    {
        /// <summary>
        /// Names of Scripts used to uninstall a specific Microsoft Office Product
        /// </summary>
        private const string Script2003FileName = "UninstallOffice2003.vbs";
        private const string Script2007FileName = "UninstallOffice2007.vbs";
        private const string Script2010FileName = "UninstallOffice2010.vbs";
        private const string Script2013C2RFileName = "UninstallOffice2013C2R.vbs";
        private const string Script2013MSIFileName = "UninstallOffice2013MSI.vbs";
        private const string Script2016C2RFileName = "UninstallOffice2016C2R.vbs";
        private const string Script2016MSIFileName = "UninstallOffice2016MSI.vbs";

        /// <summary>
        /// Run a script to Uninstall Microsoft Office.
        /// </summary>
        /// <param name="productToUninstall">Microsoft Office Product to Uninstall</param>
        /// <returns>Results from Uninstaller Script</returns>
        public static string Uninstaller(string productToUninstall)
        {
            // Create Base Path
            string path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(path);

            using (StringWriter output = new StringWriter())
            {
                if (productToUninstall == OfficeVersion.Office2003)
                {
                    // Name of the File saved on disk
                    string saveAsName = CommonUtilities.EscapePath(path + Path.DirectorySeparatorChar + Script2003FileName);

                    // Copy the Script File to TEMP
                    CommonUtilities.FileCreate(Script2003FileName, Resources.OffScrub03, path);

                    // Run Uninstaller Script
                    output.WriteLine("----------------------------------------");
                    output.WriteLine("Running Microsoft Office 2003 MSI Uninstall Script");
                    output.WriteLine("----------------------------------------");
                    Result result = CommonUtilities.ExecuteCommand("cscript " + saveAsName + " All /DELETEUSERSETTINGS /FORCE /NOCANCEL /OSE", true);

                    // Return Output
                    if (result.HasError)
                    {
                        output.WriteLine(result.Error);
                    }
                    else
                    {
                        output.WriteLine(result.Output);
                    }
                    output.Write("----------------------------------------");
                }
                else if (productToUninstall == OfficeVersion.Office2007)
                {
                    // Name of the File saved on disk
                    string saveAsName = CommonUtilities.EscapePath(path + Path.DirectorySeparatorChar + Script2007FileName);

                    // Copy the Script File to TEMP
                    CommonUtilities.FileCreate(Script2007FileName, Resources.OffScrub07, path);

                    // Run Uninstaller Script
                    output.WriteLine("----------------------------------------");
                    output.WriteLine("Running Microsoft Office 2007 MSI Uninstall Script");
                    output.WriteLine("----------------------------------------");
                    Result result = CommonUtilities.ExecuteCommand("cscript " + saveAsName + " All /DELETEUSERSETTINGS /FORCE /NOCANCEL /OSE", true);

                    // Return Output
                    if (result.HasError)
                    {
                        output.WriteLine(result.Error);
                    }
                    else
                    {
                        output.WriteLine(result.Output);
                    }
                    output.Write("----------------------------------------");
                }
                else if (productToUninstall == OfficeVersion.Office2010)
                {
                    // Name of the File saved on disk
                    string saveAsName = CommonUtilities.EscapePath(path + Path.DirectorySeparatorChar + Script2010FileName);

                    // Copy the Script File to TEMP
                    CommonUtilities.FileCreate(Script2010FileName, Resources.OffScrub10, path);

                    // Run Uninstaller Script
                    output.WriteLine("----------------------------------------");
                    output.WriteLine("Running Microsoft Office 2010 MSI Uninstall Script");
                    output.WriteLine("----------------------------------------");
                    Result result = CommonUtilities.ExecuteCommand("cscript " + saveAsName + " All /DELETEUSERSETTINGS /FORCE /NOCANCEL /OSE", true);

                    // Return Output
                    if (result.HasError)
                    {
                        output.WriteLine(result.Error);
                    }
                    else
                    {
                        output.WriteLine(result.Output);
                    }
                    output.Write("----------------------------------------");
                }
                else if (productToUninstall == OfficeVersion.Office2013)
                {
                    // Create OSPP Removal Helper Files
                    CommonUtilities.FileCreate("CleanOSPP.exe", Resources.CleanOSPP_32, path + "\\x86\\");
                    CommonUtilities.FileCreate("CleanOSPP.exe", Resources.CleanOSPP_64, path + "\\x64\\");
                    CommonUtilities.FileCreate("msvcr100.dll", Resources.msvcr100_32, path + "\\x86\\");
                    CommonUtilities.FileCreate("msvcr100.dll", Resources.msvcr100_64, path + "\\x64\\");

                    // Name of MSI Uninstaller
                    string saveAsNameMSI = CommonUtilities.EscapePath(path + Path.DirectorySeparatorChar + Script2013MSIFileName);

                    // Copy the MSI Uninstaller Script File to TEMP
                    CommonUtilities.FileCreate(Script2013MSIFileName, Resources.OffScrub13_MSI, path);

                    // Run Traditional Uninstaller Script
                    output.WriteLine("----------------------------------------");
                    output.WriteLine("Running Microsoft Office 2013 MSI Uninstall Script");
                    output.WriteLine("----------------------------------------");
                    Result msiResult = CommonUtilities.ExecuteCommand("cscript " + saveAsNameMSI + " All /DELETEUSERSETTINGS /FORCE /NOCANCEL /NOREBOOT /OSE /REMOVELYNC /REMOVEOSPP", true);
                    if (msiResult.HasError)
                    {
                        output.WriteLine(msiResult.Error);
                    }
                    else
                    {
                        output.WriteLine(msiResult.Output);
                    }

                    // Name of Virtual Uninstaller
                    string saveAsNameC2R = CommonUtilities.EscapePath(path + Path.DirectorySeparatorChar + Script2013C2RFileName);

                    // Copy the Virtual Uninstaller Script File to TEMP
                    CommonUtilities.FileCreate(Script2013C2RFileName, Resources.OffScrub13_C2R, path);

                    // Run Virtual Uninstaller Script
                    output.WriteLine("----------------------------------------");
                    output.WriteLine("Running Microsoft Office 2013 Click To Run Uninstall Script");
                    output.WriteLine("----------------------------------------");
                    Result virtualResult = CommonUtilities.ExecuteCommand("cscript " + saveAsNameC2R + " ALL /NOCANCEL /OSE", true);
                    if (virtualResult.HasError)
                    {
                        output.WriteLine(virtualResult.Error);
                    }
                    else
                    {
                        output.WriteLine(virtualResult.Output);
                    }
                    output.Write("----------------------------------------");
                }
                else if (productToUninstall == OfficeVersion.Office2016 || productToUninstall == OfficeVersion.Office2019)
                {
                    // Create OSPP Removal Helper Files
                    CommonUtilities.FileCreate("CleanOSPP.exe", Resources.CleanOSPP_32, path + "\\x86\\");
                    CommonUtilities.FileCreate("CleanOSPP.exe", Resources.CleanOSPP_64, path + "\\x64\\");
                    CommonUtilities.FileCreate("msvcr100.dll", Resources.msvcr100_32, path + "\\x86\\");
                    CommonUtilities.FileCreate("msvcr100.dll", Resources.msvcr100_64, path + "\\x64\\");

                    // Name of MSI Uninstaller
                    string saveAsNameMSI = CommonUtilities.EscapePath(path + Path.DirectorySeparatorChar + Script2016MSIFileName);

                    // Copy the MSI Uninstaller Script File to TEMP
                    CommonUtilities.FileCreate(Script2016MSIFileName, Resources.OffScrub16_MSI, path);

                    // Run Traditional Uninstaller Script
                    output.WriteLine("----------------------------------------");
                    output.WriteLine("Running Microsoft Office 2016 MSI Uninstall Script");
                    output.WriteLine("----------------------------------------");
                    Result msiResult = CommonUtilities.ExecuteCommand("cscript " + saveAsNameMSI + " All /DELETEUSERSETTINGS /FORCE /NOCANCEL /NOREBOOT /OSE /REMOVELYNC /REMOVEOSPP", true);
                    if (msiResult.HasError)
                    {
                        output.WriteLine(msiResult.Error);
                    }
                    else
                    {
                        output.WriteLine(msiResult.Output);
                    }

                    // Name of Virtual Uninstaller
                    string saveAsNameC2R = CommonUtilities.EscapePath(path + Path.DirectorySeparatorChar + Script2016C2RFileName);

                    // Copy the Virtual Uninstaller Script File to TEMP
                    CommonUtilities.FileCreate(Script2016C2RFileName, Resources.OffScrub16_C2R, path);

                    // Run Virtual Uninstaller Script
                    output.WriteLine("----------------------------------------");
                    output.WriteLine("Running Microsoft Office 2016/2019 Click To Run Uninstall Script");
                    output.WriteLine("----------------------------------------");
                    Result virtualResult = CommonUtilities.ExecuteCommand("cscript " + saveAsNameC2R + " ALL /NOCANCEL /OSE", true);
                    if (virtualResult.HasError)
                    {
                        output.WriteLine(virtualResult.Error);
                    }
                    else
                    {
                        output.WriteLine(virtualResult.Output);
                    }

                    // Run Clean OSPP
                    output.WriteLine("----------------------------------------");
                    output.WriteLine("Running Microsoft Office 2016/2019 Clean OSPP");
                    output.WriteLine("----------------------------------------");
                    Result cleanOSPPResult = CommonUtilities.ExecuteCommand(CommonUtilities.EscapePath(path + Path.DirectorySeparatorChar + Architecture.GetOSArch() + "\\CleanOSPP.exe"), true);
                    if (cleanOSPPResult.HasError)
                    {
                        output.WriteLine(cleanOSPPResult.Error);
                    }
                    else
                    {
                        output.WriteLine(cleanOSPPResult.Output);
                    }
                    output.Write("----------------------------------------");
                }
                else
                {
                    throw new Exception("Could not find an uninstaller script for this Microsoft Office Edition!");
                }

                // Delete Temporary Folder and Return Output
                CommonUtilities.FolderDelete(path);
                return output.ToString();
            }
        }
    }
}
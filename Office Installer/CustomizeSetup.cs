using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using Common;
using DiscUtils.Iso9660;
using DiscUtils.Udf;
using MiscUtil.Compression.Vcdiff;
using OfficeInstaller.Properties;
using ProductDetection;
using SharpCompress.Archives;
using SharpCompress.Common;
using DiscUtils;
using SharpCompress.Archives;
using SharpCompress.Readers;

namespace OfficeInstaller
{
    /// <summary>
    /// Group of Methods for Customizing Microsoft Office Setup
    /// </summary>
    public static class CustomizeSetup
    {
        /// <summary>
        /// State Information
        /// </summary>
        public static string SelectedInstallationPath = string.Empty;
        public static InstallerInfo InstallerInformation;

        #region ISO Extraction Functions
        // TODO: Document
        private static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

        private static void DoCopyFile(string file, string fileToCreate, IFileSystem iso)
        {
            using (Stream getFileStream = iso.OpenFile(@file, FileMode.Open))
            {
                using (Stream pushFileStream = File.OpenWrite(fileToCreate))
                {
                    CopyStream(getFileStream, pushFileStream);
                }
            }
        }

        private static void DoCopyFilesfromISO(IFileSystem iso, string baseDirectory, string parent = "\\", bool recursive = true)
        {
            if (recursive)
            {
                foreach (string directory in iso.GetDirectories(parent))
                {
                    Directory.CreateDirectory(baseDirectory + directory);
                    DoCopyFilesfromISO(iso, baseDirectory, directory);
                }
            }

            foreach (string file in iso.GetFiles(parent))
            {
                string fileName = file;
                if (fileName.Contains(";1"))
                {
                    fileName = fileName.Substring(0, fileName.Length - 2);
                }
                string fileToCreate = baseDirectory + file.Substring(0, fileName.Length);
                iso.GetFileInfo(Path.Combine(parent, file));
                DoCopyFile(file, fileToCreate, iso);
            }
        }

        public static void CopyFilesfromISO(string isoName, string baseDirectory)
        {
            using (FileStream isoStream = File.OpenRead(isoName))
            {
                if (UdfReader.Detect(isoStream))
                {
                    using (UdfReader iso = new UdfReader(isoStream))
                    {
                        DoCopyFilesfromISO(iso, baseDirectory);
                    }
                }
                else if (CDReader.Detect(isoStream))
                {
                    using (CDReader iso = new CDReader(isoStream, true))
                    {
                        DoCopyFilesfromISO(iso, baseDirectory);
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// Make an ISO Image of the Microsoft Office Installer
        /// </summary>
        /// <param name="folderPath">Directory where Microsoft Office Installer is extracted</param>
        /// <param name="fileName">Name of ISO File to Create</param>
        public static void MakeISO(string folderPath, string fileName)
        {
            CDBuilder builder = new CDBuilder {UseJoliet = true, VolumeIdentifier = "Microsoft Office"};

            List<string> filePaths = new List<string>();
            filePaths.AddRange(Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories));

            foreach (string file in filePaths)
            {
                string name = file.Substring(folderPath.Length);
                builder.AddFile(name, file);
            }

            builder.Build(fileName);
        }

        /// <summary>
        /// Add a Microsoft Office Language Pack to the Microsoft Office Installer
        /// </summary>
        /// <param name="filePath">Path to Microsoft Office Language Pack</param>
        public static void AddLanguagePack(string filePath)
        {
            string languagePackPath = Environment.GetEnvironmentVariable("temp") + "\\OfficeLPTemp";

            if (String.Compare(Path.GetExtension(filePath), ".iso", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                CopyFilesfromISO(filePath, languagePackPath + Path.DirectorySeparatorChar);
            }
            else
            {
                Process script = new Process { StartInfo = { FileName = CommonUtilities.EscapePath(filePath), Arguments = " /extract:" + CommonUtilities.EscapePath(languagePackPath) + " /quiet" }};
                script.Start();
                script.WaitForExit();
            }

            // Check for Setup EXE File
            if (!File.Exists(languagePackPath + "\\setup.exe"))
            {
                CommonUtilities.FolderDelete(languagePackPath);
                throw new Exception("setup.exe not found at the extraction path.");
            }

            // Check for SKU.WW Folders
            string[] wwFolders = Directory.GetDirectories(languagePackPath, "*.WW");
            if (wwFolders.Length != 0)
            {
                CommonUtilities.FolderDelete(languagePackPath);
                throw new Exception("This is a Microsoft Office installer not a Microsoft Office Language Pack!.");
            }

            // Find and Copy Language Pack Resources
            bool foundLanguages = false;
            foreach (string lpEnding in new LanguageList().GetLanguageCodes())
            {
                // Delete Language Pack Installer Language Folders
                CommonUtilities.FolderDelete(languagePackPath + "\\OMUI." + lpEnding);
                CommonUtilities.FolderDelete(languagePackPath + "\\PMUI." + lpEnding);
                CommonUtilities.FolderDelete(languagePackPath + "\\VisMUI." + lpEnding);
                CommonUtilities.FolderDelete(languagePackPath + "\\XMUI." + lpEnding);

                // Copy Microsoft Office Language Folders
                string[] lpFolders = Directory.GetDirectories(languagePackPath, "*." + lpEnding);
                if (lpFolders.Length != 0)
                {
                    foundLanguages = true;
                    foreach (string lpFolder in lpFolders)
                    {
                        CommonUtilities.FolderCopy(new DirectoryInfo(lpFolder), new DirectoryInfo(InstallerInformation.InstallerPath + lpFolder.Replace(Path.GetDirectoryName(lpFolder) + Path.DirectorySeparatorChar, "")));
                    }
                }
            }
            if (!foundLanguages)
            {
                CommonUtilities.FolderDelete(languagePackPath);
                throw new Exception("Could not detect any Language Pack Resources in this file!.");
            }

            // Delete Extracted Files
            CommonUtilities.FolderDelete(languagePackPath);

            // Update Installer Information
            InstallerInformation = new InstallerInfo(InstallerInformation.InstallerPath);
        }

        /// <summary>
        /// Remove a Language from the Microsoft Office Installer
        /// </summary>
        /// <param name="languageName">Name of Language to Remove</param>
        public static void RemoveLanguagePack(string languageName)
        {
            if (InstallerInformation.Languages.Count > 1)
            {
                LanguageList languageList = new LanguageList();
                string languageCode = languageList.GetLanguageCode(languageName);

                if (String.IsNullOrWhiteSpace(languageCode))
                {
                    throw new Exception("Failed to determine selected language and ending");
                }

                if (Directory.Exists(InstallerInformation.InstallerPath + "Office." + languageCode) == false)
                {
                    throw new Exception("This Language is not installed!");
                }
                foreach (string folder in Directory.GetDirectories(InstallerInformation.InstallerPath, "*." + languageCode))
                {
                    CommonUtilities.FolderDelete(folder);
                }

                InstallerInformation.Languages.Remove(languageName);
            }
            else
            {
                throw new Exception("You only have one language installed!");
            }
        }

        /// <summary>
        /// Add a Microsoft Office Update to the Microsoft Office Installer
        /// </summary>
        /// <param name="filePath">Path to Microsoft Office Update</param>
        public static void AddUpdate(string filePath)
        {
            string updateFolderPath = InstallerInformation.InstallerPath + "Updates";

            if (String.Compare(Path.GetExtension(filePath), ".msp", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                File.Copy(filePath, updateFolderPath + Path.DirectorySeparatorChar + Path.GetFileName(filePath));
            }
            else
            {
                using (Process script = new Process { StartInfo = { FileName = CommonUtilities.EscapePath(filePath), Arguments = " /extract:" + CommonUtilities.EscapePath(updateFolderPath) + " /quiet" }})
                {
                    script.Start();
                    script.WaitForExit();
                }
            }
        }

        /// <summary>
        /// Run Microsoft Office Customization Tool
        /// </summary>
        public static void RunOCT()
        {
            using (Process script = new Process {StartInfo = {FileName = InstallerInformation.InstallerPath + "Setup.exe", Arguments = " /admin"}})
            {
                script.Start();
                script.WaitForExit();
            }
        }

        /// <summary>
        /// Convert a Microsoft Office installer from Retail to Volume, or vice-versa
        /// </summary>
        /// <param name="licenseChannel">Current License Channel</param>
        /// <param name="productName">Microsoft Office Product to Convert</param>
        public static void ChangeChannel(string licenseChannel, string productName)
        {
            string architecture = InstallerInformation.InstallerArchitecture;
            string retailFolder = new ProductListRetail().GetProductFolder(productName);
            string volumeFolder = new ProductListVolume().GetProductFolder(productName);

            string fullInstallerPath = string.Empty;
            if (licenseChannel == "Retail")
            {
                fullInstallerPath = InstallerInformation.InstallerPath + retailFolder + Path.DirectorySeparatorChar;
            }
            else if (licenseChannel == "Volume")
            {
                fullInstallerPath = InstallerInformation.InstallerPath + volumeFolder + Path.DirectorySeparatorChar;
            }

            XmlDocument doc = new XmlDocument();
            byte[] patchXml;
            switch (InstallerInformation.SetupVersionFull)
            {
                case "14.0.4755.1000":
                    patchXml = Resources.Office14_XML;
                    break;
                case "15.0.4420.1017":
                case "15.0.4433.1506":
                case "15.0.4454.1000":
                    patchXml = Resources.Office15_XML;
                    break;
                case "15.0.4569.1503":
                    patchXml = Resources.Office15SP1_XML;
                    break;
                // TODO: Office 2016 Channel Changer
                default:
                    throw new Exception("Unsupported Microsoft Office Edition!");
            }

            doc.Load(new MemoryStream(CommonUtilities.DecompressResource(patchXml)));

            // Root
            XmlElement root = doc.DocumentElement; 
            if (root != null)
            {
                // Products Node
                XmlNodeList productList = root.ChildNodes; 

                // Find Desired Product Node in Products Node
                XmlNode product = null;
                foreach (XmlNode productInstance in productList)
                {
                    XmlAttributeCollection productAttributes = productInstance.Attributes;
                    if (productAttributes != null && productAttributes["name"].Value == productName)
                    {
                        product = productInstance;
                        break;
                    }
                }
                       
                // Product Unsupported
                if (product == null)
                {
                    throw new ApplicationException("This product is not supported due to lack of patches.");
                }

                // Patch Product
                PatchProcessor(product, fullInstallerPath, licenseChannel, architecture);
            }
              
            #region File Renaming, Zip Files, and Cleanup
            // Rename Folders
            if (licenseChannel == "Retail")
            {
                Directory.Move(fullInstallerPath, InstallerInformation.InstallerPath + Path.DirectorySeparatorChar + volumeFolder);
            }
            else if (licenseChannel == "Volume")
            {
                Directory.Move(fullInstallerPath, InstallerInformation.InstallerPath + Path.DirectorySeparatorChar + retailFolder);
            }
            
            // Get Extraction Folder
            string unpackDirectoryAdmin = InstallerInformation.InstallerPath + Path.DirectorySeparatorChar + "Admin";
            string unpackDirectoryProduct = InstallerInformation.InstallerPath + Path.DirectorySeparatorChar;
            string unpackDirectorySetup = InstallerInformation.InstallerPath + Path.DirectorySeparatorChar;
            if (licenseChannel == "Retail")
            {
                unpackDirectoryProduct += volumeFolder;
            }
            else if (licenseChannel == "Volume")
            {
                unpackDirectoryProduct += retailFolder;
            }

            // Create Admin Folder if it doesn't exist
            if (Directory.Exists(InstallerInformation.InstallerPath + Path.DirectorySeparatorChar + "Admin")) return;

            // Extract Archives
            byte[] x86, x64, shared, productZip32, setupZip32, productZip64, setupZip64;
            switch (InstallerInformation.SetupVersionFull)
            {
                case "14.0.4755.1000":
                    x86 = Resources.Office14_Admin32;
                    x64 = Resources.Office14_Admin64;
                    shared = Resources.Office14_AdminShared;
                    productZip32 = null;
                    productZip64 = null;
                    setupZip32 = null;
                    setupZip64 = null;
                    break;
                case "15.0.4420.1017":
                case "15.0.4433.1506":
                case "15.0.4454.1000":
                    x86 = Resources.Office15_Admin32;
                    x64 = Resources.Office15_Admin64;
                    shared = Resources.Office15_AdminShared;
                    productZip32 = Resources.Office15_ProductRoot32;
                    productZip64 = Resources.Office15_ProductRoot64;
                    setupZip32 =  Resources.Office15_SetupRoot32;
                    setupZip64 = Resources.Office15_SetupRoot64;
                    break;
                case "15.0.4569.1503":
                    x86 = Resources.Office15SP1_Admin32;
                    x64 = Resources.Office15SP1_Admin64;
                    shared = Resources.Office15SP1_AdminShared;
                    productZip32 = Resources.Office15SP1_ProductRoot32;
                    productZip64 = Resources.Office15SP1_ProductRoot64;
                    setupZip32 =  Resources.Office15SP1_SetupRoot32;
                    setupZip64 = Resources.Office15SP1_SetupRoot64;
                    break;
                default:
                    throw new Exception("Unsupported Microsoft Office Edition!");
            }

            if (InstallerInformation.InstallerArchitecture == Architecture.X86)
            {
                // Extract Admin Folder
                using (IArchive archive32 = ArchiveFactory.Open(new MemoryStream(x86)))
                {
                    foreach (IArchiveEntry entry in archive32.Entries)
                    {
                        if (!entry.IsDirectory)
                        {
                            entry.WriteToDirectory(unpackDirectoryAdmin, new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                        }
                    }
                }

                // Extract Product Folder
                if (productZip32 != null)
                {
                    using (IArchive archiveProduct = ArchiveFactory.Open(new MemoryStream(productZip32)))
                    {
                        foreach (IArchiveEntry entry in archiveProduct.Entries)
                        {
                            if (!entry.IsDirectory)
                            {
                                entry.WriteToDirectory(unpackDirectoryProduct, new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                            }
                        }
                    }
                }

                // Extract Setup Folder
                if (setupZip32 != null)
                {
                    using (IArchive archiveSetup = ArchiveFactory.Open(new MemoryStream(setupZip32)))
                    {
                        foreach (IArchiveEntry entry in archiveSetup.Entries)
                        {
                            if (!entry.IsDirectory)
                            {
                                entry.WriteToDirectory(unpackDirectorySetup, new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                            }
                        }
                    }
                }
            }
            else if (InstallerInformation.InstallerArchitecture == Architecture.X64)
            {
                // Extract Admin Folder
                using (IArchive archive64 = ArchiveFactory.Open(new MemoryStream(x64)))
                {
                    foreach (IArchiveEntry entry in archive64.Entries)
                    {
                        if (!entry.IsDirectory)
                        {
                            entry.WriteToDirectory(unpackDirectoryAdmin, new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                        }
                    }
                }

                // Extract Product Folder
                if (productZip64 != null)
                {
                    using (IArchive archiveProduct = ArchiveFactory.Open(new MemoryStream(productZip64)))
                    {
                        foreach (IArchiveEntry entry in archiveProduct.Entries)
                        {
                            if (!entry.IsDirectory)
                            {
                                entry.WriteToDirectory(unpackDirectoryProduct, new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                            }
                        }
                    }
                }

                // Extract Setup Folder
                if (setupZip64 != null)
                {
                    using (IArchive archiveSetup = ArchiveFactory.Open(new MemoryStream(setupZip64)))
                    {
                        foreach (IArchiveEntry entry in archiveSetup.Entries)
                        {
                            if (!entry.IsDirectory)
                            {
                                entry.WriteToDirectory(unpackDirectorySetup, new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                            }
                        }
                    }
                }
            }

            // Extract Admin Folder Components Shared Between x86 and x64
            using (IArchive archiveShared = ArchiveFactory.Open(new MemoryStream(shared)))
            {
                foreach (IArchiveEntry entry in archiveShared.Entries)
                {
                    if (!entry.IsDirectory)
                    {
                        entry.WriteToDirectory(unpackDirectoryAdmin, new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                    }
                }
            }
            #endregion
        }

        public static bool ChangeChannelSupported()
        {
            try
            {
                switch (InstallerInformation.SetupVersionFull)
                {
                    case "14.0.4755.1000":
                    case "15.0.4420.1017":
                    case "15.0.4433.1506":
                    case "15.0.4454.1000":
                    case "15.0.4569.1503":
                        return true;
                    // TODO: Office 2016 Channel Changer
                    default:
                        return false;
                }
            }
            catch (FileNotFoundException)
            {
               return false;
            }
        }

        private static void PatchProcessor(XmlNode product, string fullInstallerPath, string licenseChannel, string architecture)
        {
            #region XDELTA Patching
            // List of Patch Nodes for the Product
            XmlNodeList patchList = null;
            foreach (XmlNode productChild in product.ChildNodes)
            {
                if (productChild.Name == "Patches")
                {
                    patchList = productChild.ChildNodes;
                    break;
                }
            }

            // Process Patch Nodes
            if (patchList != null)
            {
                foreach (XmlNode patchInstance in patchList)
                {
                    // Get Patch Attributes (Target File Names)
                    XmlAttributeCollection patchAttributes = patchInstance.Attributes;
                    if (patchAttributes != null)
                    {
                        string retailFileName = patchAttributes["RetailFile"].Value;
                        string volumeFileName = patchAttributes["VolumeFile"].Value;
                        string version = patchAttributes["Version"].Value;
                        string patchDataBase64 = string.Empty;

                        // Skip Patches Based On Version
                        if (!version.Contains(InstallerInformation.SetupVersionFull))
                        {
                            continue;
                        }

                        // Find Desired PatchData Node for the Source License Channel and Architecture
                        foreach (XmlNode patchDataInstance in patchInstance)
                        {
                            XmlAttributeCollection patchDataAttributes = patchDataInstance.Attributes;
                            if (patchDataAttributes != null && !patchDataAttributes["TargetChannel"].Value.ToUpper().Contains(licenseChannel.ToUpper()) && patchDataAttributes["Architecture"].Value == architecture)
                            {
                                patchDataBase64 = patchDataInstance.InnerText;
                                break;
                            }
                        }

                        string originalFile = string.Empty;
                        string targetFile = string.Empty;

                        if (retailFileName == volumeFileName)
                        {
                            originalFile = fullInstallerPath + Path.DirectorySeparatorChar + retailFileName;
                            targetFile = originalFile + ".new";
                        }
                        else if (licenseChannel == "Retail")
                        {
                            originalFile = fullInstallerPath + Path.DirectorySeparatorChar + retailFileName;
                            targetFile = fullInstallerPath + Path.DirectorySeparatorChar + volumeFileName;
                        }
                        else if (licenseChannel == "Volume")
                        {
                            originalFile = fullInstallerPath + Path.DirectorySeparatorChar + volumeFileName;
                            targetFile = fullInstallerPath + Path.DirectorySeparatorChar + retailFileName;
                        }

                        // Execute Patcher
                        using (FileStream original = File.OpenRead(originalFile))
                        {
                            using (Stream patchStream = new MemoryStream(Convert.FromBase64String(patchDataBase64)))
                            {
                                using (FileStream target = File.Open(targetFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                                {
                                    VcdiffDecoder.Decode(original, patchStream, target);
                                }
                            }
                        }

                        // Delete Old File
                        CommonUtilities.FileDelete(originalFile);

                        // Rename New File
                        if (targetFile.Contains(".new"))
                        {
                            File.Move(targetFile, targetFile.Replace(".new", ""));
                        }
                    }
                }
            }
            #endregion

            #region File Replacing
            // List of File Nodes for the Product
            XmlNodeList fileList = null;
            foreach (XmlNode productChild in product.ChildNodes)
            {
                if (productChild.Name == "Files")
                {
                    fileList = productChild.ChildNodes;
                    break;
                }
            }

            // Process Patch Nodes
            if (fileList != null)
            {
                foreach (XmlNode fileInstance in fileList)
                {
                    // Get File Attributes (Target File Names)
                    XmlAttributeCollection fileAttributes = fileInstance.Attributes;
                    if (fileAttributes != null)
                    {
                        string retailFileName = fileAttributes["RetailFile"].Value;
                        string volumeFileName = fileAttributes["VolumeFile"].Value;
                        string version = fileAttributes["Version"].Value;
                        string fileDataBase64 = string.Empty;

                        // Skip Files Based On Version
                        if (!version.Contains(InstallerInformation.SetupVersionFull))
                        {
                            continue;
                        }

                        // Find Desired FileData Node for the Source License Channel and Architecture
                        foreach (XmlNode fileDataInstance in fileInstance)
                        {
                            XmlAttributeCollection fileDataAttributes = fileDataInstance.Attributes;
                            if (fileDataAttributes != null && !fileDataAttributes["TargetChannel"].Value.ToUpper().Contains(licenseChannel.ToUpper()) && fileDataAttributes["Architecture"].Value == architecture)
                            {
                                fileDataBase64 = fileDataInstance.InnerText;
                                break;
                            }
                        }

                        string originalFile = string.Empty;
                        string targetFile = string.Empty;

                        if (retailFileName == volumeFileName)
                        {
                            originalFile = fullInstallerPath + Path.DirectorySeparatorChar + retailFileName;
                            targetFile = originalFile;
                        }
                        else if (licenseChannel == "Retail")
                        {
                            originalFile = fullInstallerPath + Path.DirectorySeparatorChar + retailFileName;
                            targetFile = fullInstallerPath + Path.DirectorySeparatorChar + volumeFileName;
                        }
                        else if (licenseChannel == "Volume")
                        {
                            originalFile = fullInstallerPath + Path.DirectorySeparatorChar + volumeFileName;
                            targetFile = fullInstallerPath + Path.DirectorySeparatorChar + retailFileName;
                        }

                        // Delete Old File
                        CommonUtilities.FileDelete(originalFile);

                        // Create New File
                        CommonUtilities.FileCreate(Path.GetFileName(targetFile), Convert.FromBase64String(fileDataBase64), Path.GetDirectoryName(targetFile));
                    }
                }
            }
            #endregion
        }
    }

    /// <summary>
    /// Class to hold State Information and Details about Microsoft Office Setup
    /// </summary>
    public class InstallerInfo
    {
        public readonly string InstallerArchitecture = string.Empty;
        public readonly string InstallerPath = string.Empty;
        public readonly int SetupVersionMajor;
        public readonly string SetupVersionFull;
        public readonly List<string> Languages = new List<string>();
        public readonly List<string> RetailProducts = new List<string>();
        public readonly List<string> VolumeProducts = new List<string>();

        public InstallerInfo(string installerPath)
        {
            Languages = new List<string>();
            RetailProducts = new List<string>();
            VolumeProducts = new List<string>();
            InstallerArchitecture = string.Empty;
            InstallerPath = installerPath;
            SetupVersionMajor = 0;
            SetupVersionFull = string.Empty;

            // Determine What Products are in the Installer and their License Channels
            ProductListRetail retailList = new ProductListRetail();
            foreach (string productFolder in retailList.GetFolders())
            {
                if (Directory.Exists(InstallerPath + productFolder))
                {
                    RetailProducts.Add(retailList.GetProductName(productFolder));
                }
            }
            ProductListVolume volumeList = new ProductListVolume();
            foreach (string productFolder in volumeList.GetFolders())
            {
                if (Directory.Exists(InstallerPath + productFolder))
                {
                    VolumeProducts.Add(volumeList.GetProductName(productFolder));
                }
            }

            // Determine Installer Languages and Installer Architecture
            LanguageList languageList = new LanguageList();
            foreach (string languageCode in languageList.GetLanguageCodes())
            {
                if (Directory.Exists(InstallerPath + "Office64." + languageCode))
                {
                    Languages.Add(languageList.GetLanguageName(languageCode));
                    InstallerArchitecture = Architecture.X86;
                }
                else if (Directory.Exists(InstallerPath + "Office32." + languageCode))
                {
                    Languages.Add(languageList.GetLanguageName(languageCode));
                    InstallerArchitecture = Architecture.X64;
                }
            }

            // Get Microsoft Office Setup Version
            FileVersionInfo setupVersion = FileVersionInfo.GetVersionInfo(InstallerPath + "setup.exe");
            SetupVersionMajor = setupVersion.FileMajorPart;
            if (SetupVersionMajor <= 14)
            {
                // Microsoft Office 2010 and Older
                SetupVersionFull = setupVersion.FileVersion;
            }
            else
            {
                // Microsoft Office 2013 and Newer
                try
                {
                    FileVersionInfo setupDLLVersion = FileVersionInfo.GetVersionInfo(InstallerPath + "setup.dll");
                    SetupVersionFull = setupDLLVersion.FileVersion;
                }
                catch (FileNotFoundException)
                {
                    throw new ApplicationException("This is not a valid/supported Microsoft Office installer!");
                }
            }
        }
    }

    /// <summary>
    /// List of Language Names and Codes, and methods to match them
    /// </summary>
    public class LanguageList
    {
        /// <summary>
        /// Hashtable that uses Language Code as the index for easy Language Name retrieval
        /// </summary>
        private static readonly Hashtable LanguageTable = new Hashtable();

        static LanguageList()
        {
            // TODO: Office 2016 Languages?
            AddLanguage("ar-sa", "Arabic");
            AddLanguage("pt-br", "Brazilian");
            AddLanguage("bg-bg", "Bulgarian");
            AddLanguage("zh-cn", "Chinese (Simp.)");
            AddLanguage("zh-tw", "Chinese (Trad.)");
            AddLanguage("hr-hr", "Croatian");
            AddLanguage("cs-cz", "Czech");
            AddLanguage("da-dk", "Danish");
            AddLanguage("nl-nl", "Dutch");
            AddLanguage("en-us", "English");
            AddLanguage("et-ee", "Estonian");
            AddLanguage("fi-fi", "Finnish");
            AddLanguage("fr-fr", "French");
            AddLanguage("de-de", "German");
            AddLanguage("el-gr", "Greek");
            AddLanguage("he-il", "Hebrew");
            AddLanguage("hi-in", "Hindi");
            AddLanguage("hu-hu", "Hungarian");
            AddLanguage("it-it", "Italian");
            AddLanguage("ja-jp", "Japanese");
            AddLanguage("kk-kz", "Kazakh");
            AddLanguage("ko-kr", "Korean");
            AddLanguage("lv-lv", "Latvian");
            AddLanguage("lt-lt", "Lithuania");
            AddLanguage("nb-no", "Norwegian");
            AddLanguage("pl-pl", "Polish");
            AddLanguage("pt-pt", "Portuguese");
            AddLanguage("ro-ro", "Romanian");
            AddLanguage("ru-ru", "Russian");
            AddLanguage("sr-latn-cs", "Serbian");
            AddLanguage("sk-sk", "Slovak");
            AddLanguage("sl-si", "Slovenian");
            AddLanguage("es-es", "Spanish");
            AddLanguage("sv-se", "Swedish");
            AddLanguage("th-th", "Thai");
            AddLanguage("tr-tr", "Turkish");
            AddLanguage("uk-ua", "Ukrainian");
        }

        /// <summary>
        /// Get a Language Name from the Language Hashtable
        /// </summary>
        /// <param name="languageCode"> Hashtable index to access </param>
        /// <returns> The Language Name in the desired index </returns>
        public string GetLanguageName(string languageCode)
        {
            return LanguageTable[languageCode].ToString();
        }

        /// <summary>
        /// Get a Language Code from the Language Hashtable
        /// </summary>
        /// <param name="languageName"> Hashtable index to match </param>
        /// <returns> The Language Code matching the Language Name </returns>
        public string GetLanguageCode(string languageName)
        {
            foreach (DictionaryEntry e in LanguageTable)
            {
                if (e.Value.ToString() == languageName)
                {
                    return e.Key.ToString();
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Add or Replace a Language in the Hashtable
        /// </summary>
        /// <param name="code"> Index to create or replace </param>
        /// <param name="name"> Language to Add or Replace </param>
        protected static void AddLanguage(string code, string name)
        {
            LanguageTable[code] = name;
        }

        /// <summary>
        /// Get a List of all Codes in the LanguageList
        /// </summary>
        /// <returns> String List of all Language Codes in the Hashtable </returns>
        public List<string> GetLanguageCodes()
        {
            List<string> codes = new List<string>();
            foreach (DictionaryEntry e in LanguageTable)
            {
                codes.Add(e.Key.ToString());
            }
            return codes;
        }
    }

    /// <summary>
    /// List of Product Names and Product Folders, and methods to match them
    /// </summary>
    public class ProductList
    {
        /// <summary>
        /// Hashtable that uses Product Folder as the index for easy Product Name retrieval
        /// </summary>
        internal Hashtable ProductTable = new Hashtable();

        /// <summary>
        /// Get a Product Name from the Product Hashtable
        /// </summary>
        /// <param name="productFolder"> Hashtable index to access </param>
        /// <returns> The Product Name in the desired index </returns>
        public string GetProductName(string productFolder)
        {
            return ProductTable[productFolder].ToString();
        }

        /// <summary>
        /// Get a Product Folder from the Product Hashtable
        /// </summary>
        /// <param name="productName"> Hashtable index to match </param>
        /// <returns> The Product Folder matching the Product Name </returns>
        public string GetProductFolder(string productName)
        {
            foreach (DictionaryEntry e in ProductTable)
            {
                if (e.Value.ToString() == productName)
                {
                    return e.Key.ToString();
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Add or Replace a Product in the Hashtable
        /// </summary>
        /// <param name="productFolder"> Index to create or replace </param>
        /// <param name="productName"> Language to Add or Replace </param>
        protected void AddProduct(string productFolder, string productName)
        {
            ProductTable[productFolder] = productName;
        }

        /// <summary>
        /// Get a List of all Folders in the ProductList
        /// </summary>
        /// <returns> String List of all Product Folders in the Hashtable </returns>
        public List<string> GetFolders()
        {
            List<string> folders = new List<string>();
            foreach (DictionaryEntry e in ProductTable)
            {
                folders.Add(e.Key.ToString());
            }
            return folders;
        }
    }

    /// <summary>
    /// Retail List of Products
    /// </summary>
    public class ProductListRetail : ProductList
    {
        public ProductListRetail()
        {
            ProductTable = new Hashtable();
            // TODO: Lync/Skype
            AddProduct("AccessR.WW", "Access");
            AddProduct("ExcelR.WW", "Excel");
            AddProduct("GrooveR.WW", "SharePoint Workspace");
            AddProduct("InfoPathR.WW", "InfoPath");
            AddProduct("OneNoteR.WW", "OneNote");
            AddProduct("OutlookR.WW", "Outlook");
            AddProduct("PowerPointR.WW", "PowerPoint");
            AddProduct("PrjProR.WW", "Project Professional");
            AddProduct("PrjStdR.WW", "Project Standard");
            AddProduct("ProPlusR.WW", "Professional Plus");
            AddProduct("PublisherR.WW", "Publisher");
            AddProduct("SmallBusBasicsR.WW", "Small Business Basics");
            AddProduct("StandardR.WW", "Standard");
            AddProduct("VisProR.WW", "Visio Professional");
            AddProduct("VisStdR.WW", "Visio Standard");
            AddProduct("VisioR.WW", "Visio");
            AddProduct("WordR.WW", "Word");
        }
    }

    /// <summary>
    /// Volume List of Products
    /// </summary>
    public class ProductListVolume : ProductList
    {
        public ProductListVolume()
        {
            ProductTable = new Hashtable();
            // TODO: Lync/Skype
            AddProduct("Access.WW", "Access");
            AddProduct("Excel.WW", "Excel");
            AddProduct("Groove.WW", "SharePoint Workspace");
            AddProduct("InfoPath.WW", "InfoPath");
            AddProduct("OneNote.WW", "OneNote");
            AddProduct("Outlook.WW", "Outlook");
            AddProduct("PowerPoint.WW", "PowerPoint");
            AddProduct("PrjPro.WW", "Project Professional");
            AddProduct("PrjStd.WW", "Project Standard");
            AddProduct("ProPlus.WW", "Professional Plus");
            AddProduct("Publisher.WW", "Publisher");
            AddProduct("SmallBusBasics.WW", "Small Business Basics");
            AddProduct("Standard.WW", "Standard");
            AddProduct("VisPro.WW", "Visio Professional");
            AddProduct("VisStd.WW", "Visio Standard");
            AddProduct("Visio.WW", "Visio");
            AddProduct("Word.WW", "Word");
        }
    }
}
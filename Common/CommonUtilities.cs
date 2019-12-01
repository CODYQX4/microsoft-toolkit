using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using SharpCompress;
using SharpCompress.Archives;

namespace Common
{
    /// <summary>
    /// Group of Commonly used Utility Methods
    /// </summary>
    public static class CommonUtilities
    {
        #region File and Process Functions
        /// <summary>
        /// How many times to attempt to copy, delete or create a file.
        /// </summary>
        private const int FileAttempts = 5;

        /// <summary>
        /// Quote a path if it has spaces
        /// </summary>
        /// <param name="path">Path to file or folder</param>
        /// <returns>Path in quotes if it has spaces in the path</returns>
        public static string EscapePath(string path)
        {
            return !string.IsNullOrEmpty(path) ?
                path.Contains(" ") ? "\"" + path + "\"" : path
                : string.Empty;
        }

        /// <summary>
        /// Execute a Process using the command line, and get output. Prevent the command from running under WOW64
        /// </summary>
        /// <param name="command">Command to execute</param>
        /// <param name="saveOutput">Whether or not to store the Result of the command</param>
        /// <param name="wowRedirection">False to use 64-Bit CMD, True to use 32-Bit CMD</param>
        /// <returns>Output result of the command</returns>
        public static Result ExecuteCommand(string command, bool saveOutput, bool wowRedirection = false)
        {
            Result cmdOutput = new Result();

            ProcessStartInfo processInfo;
            if (Environment.Is64BitOperatingSystem && !Environment.Is64BitProcess && wowRedirection == false)
            {
                processInfo = new ProcessStartInfo(Environment.GetEnvironmentVariable("windir") + "\\Sysnative\\" + "cmd.exe", "/C " + command);
            }
            else
            {
                processInfo = new ProcessStartInfo("cmd.exe", "/C " + command);
            }
            // Don't show Command Prompt
            processInfo.CreateNoWindow = true;
            // With this the system shell is not used to start the process
            processInfo.UseShellExecute = false;
            // With this the output of the cmd is redirected to the Process.StandardOutput stream
            processInfo.RedirectStandardOutput = true;

            using (Process process = Process.Start(processInfo))
            {
                if (process != null)
                {
                    using (StreamReader myStreamReader = process.StandardOutput)
                    {
                        while (process.HasExited == false)
                        {
                            Thread.Sleep(100);
                        }

                        // Assigns the ExitCode to the result
                        cmdOutput.ExitCode = process.ExitCode;

                        if (saveOutput)
                        {
                            // Assign the corresponding messsage
                            if (cmdOutput.HasError)
                            {
                                cmdOutput.Error = myStreamReader.ReadToEnd();
                            }
                            else
                            {
                                cmdOutput.Output = myStreamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }

            return cmdOutput;
        }


        /// <summary>
        /// Decompress Single File Resource
        /// </summary>
        /// <param name="compressedResource">Compressed (Archived) Single File Resource as Byte Array</param>
        /// <returns>Single File Extracted from Archive as Byte Array</returns>
        public static byte[] DecompressResource(byte[] compressedResource)
        {
            // Decompress XML
            using (IArchive archive32 = ArchiveFactory.Open(new MemoryStream(compressedResource)))
            {
                foreach (IArchiveEntry entry in archive32.Entries)
                {
                    if (!entry.IsDirectory)
                    {
                        using (Stream stream = entry.OpenEntryStream())
                        {
                            byte[] resource = new byte[stream.Length];
                            for (int i = 0; i < stream.Length; i++)
                            {
                                resource[i] = (byte)stream.ReadByte();
                            }
                            return resource;
                        }
                    }
                }
            }
            throw new Exception("Failed to decompress resource!");
        }

        /// <summary>
        /// Write a byte array to a file
        /// </summary>
        /// <param name="fileName">Name of file to create</param>
        /// <param name="fileData">Byte Array representation of the file</param>
        /// <param name="createPath">Path to create file in</param>
        public static void FileCreate(string fileName, byte[] fileData, string createPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(createPath);

            // If the directory does not exist, it must be created
            if (directoryInfo.Exists == false)
            {
                bool created = false;
                for (int attempts = 0; attempts < FileAttempts && created == false; attempts++)
                {
                    try
                    {
                        directoryInfo.Create();
                        created = true;
                    }
                    catch (IOException)
                    {
                        Thread.Sleep(50);
                    }
                }

                // If the directory was not created there was an error
                if (created == false)
                {
                    throw new IOException("The directory \"" + directoryInfo.FullName + "\" could not be created");
                }
            }

            // Attempt to create file
            for (int attempts = 0; attempts < FileAttempts && File.Exists(createPath + Path.DirectorySeparatorChar + fileName) == false; attempts++)
            {
                try
                {
                    FileInfo fileInfoOutputFile = new FileInfo(createPath + Path.DirectorySeparatorChar + fileName);

                    // Check if the file exists.
                    if (fileInfoOutputFile.Exists)
                    {
                        // Delete If Exists.
                        fileInfoOutputFile.Delete();
                    }

                    // Open a stream to write to the file.
                    using (FileStream streamToOutputFile = fileInfoOutputFile.OpenWrite())
                    {
                        streamToOutputFile.Write(fileData, 0, fileData.Length);
                    }
                }
                catch (IOException)
                {
                    Thread.Sleep(50);
                }
            }

            // If the file was not copied there was an error
            if (File.Exists(createPath + Path.DirectorySeparatorChar + fileName) == false)
            {
                throw new IOException("The file \"" + createPath + Path.DirectorySeparatorChar + fileName + "\" could not be created");
            }
        }

        /// <summary>
        /// Delete a file
        /// </summary>
        /// <param name="fileName">Full path to file to delete</param>
        public static void FileDelete(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);

            // If the file exists it has to be deleted
            if (fileInfo.Exists)
            {
                for (int attempts = 0; attempts < FileAttempts && fileInfo.Exists; attempts++)
                {
                    try
                    {
                        fileInfo.Attributes = FileAttributes.Normal;
                        fileInfo.Delete();
                    }
                    catch (IOException)
                    {
                        Thread.Sleep(50);
                    }
                }

                // If the file was not deleted there was an error
                if (fileInfo.Exists)
                {
                    throw new IOException("The file \"" + fileInfo.FullName + "\" could not be deleted");
                }
            }
        }

        /// <summary>
        /// Format Indent an XML Document
        /// </summary>
        /// <param name="xml">XML to Format</param>
        /// <returns>String Representation of Formatted XML Document</returns>
        public static string FormatXml(string xml)
        {
            try
            {
                XDocument doc = XDocument.Parse(xml);
                return doc.ToString();
            }
            catch (Exception)
            {
                return xml;
            }
        }

        /// <summary>
        /// Check is a Directory is Writable using a Temporary File
        /// </summary>
        /// <param name="path">Directory to Check for Write Access</param>
        /// <returns>True if the Path is Writable, False if the Path is not Writable</returns>
        public static bool IsDirectoryWritable(string path)
        {
            try
            {
                using (File.Create(Path.Combine(path, Path.GetRandomFileName()), 1, FileOptions.DeleteOnClose)) {}
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Terminate a Process
        /// </summary>
        /// <param name="name">Name of the Process to kill</param>
        public static void KillProcess(string name)
        {
            foreach (Process processToKill in Process.GetProcessesByName(name))
            {
                KillProcess(processToKill);
            }
        }

        /// <summary>
        /// Terminate a Process
        /// </summary>
        /// <param name="process">Process to Kill</param>
        public static void KillProcess(Process process)
        {
            process.Kill();
            while (process.HasExited == false)
            {
                Thread.Sleep(200);
            }
        }

        /// <summary>
        /// Search and replace text in a file
        /// </summary>
        /// <param name="filePath">Full Path of the file to operate on</param>
        /// <param name="searchText">Text to Search for</param>
        /// <param name="replaceText">Text to Replace the Search Text with</param>
        public static void ReplaceTextInFile(string filePath, string searchText, string replaceText)
        {
            string content;

            // Determine File Encoding
            Encoding enc;
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                if (file.CanSeek)
                {
                    byte[] bom = new byte[4]; // Get the byte-order mark, if there is one 
                    file.Read(bom, 0, 4);
                    if ((bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) || // utf-8 
                        (bom[0] == 0xff && bom[1] == 0xfe) || // ucs-2le, ucs-4le, and ucs-16le 
                        (bom[0] == 0xfe && bom[1] == 0xff) || // utf-16 and ucs-2 
                        (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff)) // ucs-4 
                    {
                        enc = Encoding.Unicode;
                    }
                    else
                    {
                        enc = Encoding.ASCII;
                    }

                    // Now reposition the file cursor back to the start of the file 
                    file.Seek(0, SeekOrigin.Begin);
                }
                else
                {
                    // The file cannot be randomly accessed, so you need to decide what to set the default to 
                    // based on the data provided. If you're expecting data from a lot of older applications, 
                    // default your encoding to Encoding.ASCII. If you're expecting data from a lot of newer 
                    // applications, default your encoding to Encoding.Unicode. Also, since binary files are 
                    // single byte-based, so you will want to use Encoding.ASCII, even though you'll probably 
                    // never need to use the encoding then since the Encoding classes are really meant to get 
                    // strings from the byte array that is the file. 

                    enc = Encoding.Unicode;
                }
            }
            // Read All Text
            using (StreamReader reader = new StreamReader(filePath, enc))
            {
                content = reader.ReadToEnd();
            }

            // Replace Text
            content = Regex.Replace(content, searchText, replaceText);

            // Write All Text
            using (StreamWriter writer = new StreamWriter(filePath, false, enc))
            {
                writer.Write(content);
            }
        }

        /// <summary>
        /// Read a Byte Array Representation of Tokens.dat and Parse Out All Licenses
        /// </summary>
        /// <param name="tokens">Byte Array Representation of Tokens.dat</param>
        /// <param name="startPattern">Start XML Tag Pattern</param>
        /// <param name="endPattern">End XML Tag Pattern</param>
        /// <returns>List of all Found Licenses in XmlDocument Form</returns>
        public static List<XmlDocument> ParseTokensForLicenses(byte[] tokens, string startPattern, string endPattern)
        {
            // List to Hold all Found Licenses
            List<XmlDocument> foundLicenses = new List<XmlDocument>();

            // Start and End Patterns of a License
            byte[] licenseStartPattern = Encoding.ASCII.GetBytes(startPattern);
            byte[] licenseEndPattern = Encoding.ASCII.GetBytes(endPattern);

            // Tokens Size Sanity Check
            if (licenseStartPattern.Length > tokens.Length || licenseEndPattern.Length > tokens.Length)
            {
                throw new Exception("Invalid Tokens Array Size!");
            }

            // Loop Through Array To Find Start Index of License
            for (int i = 0; i < tokens.Length - licenseStartPattern.Length; i++)
            {
                // Read from Current I to J Bytes, Up to the Length of License Start Pattern
                bool foundStart = true;
                for (int j = 0; j < licenseStartPattern.Length; j++)
                {
                    // Move to Next I
                    if (tokens[i + j] != licenseStartPattern[j])
                    {
                        foundStart = false;
                        break;
                    }
                }
                if (foundStart)
                {
                    // Loop Through Array To Find End Index of License
                    for (int k = i; k < tokens.Length - licenseEndPattern.Length; k++)
                    {
                        // Read from Current K to L Bytes, Up to the Length of License End Pattern
                        bool foundEnd = true;
                        for (int l = 0; l < licenseEndPattern.Length; l++)
                        {
                            // Move to Next K
                            if (tokens[k + l] != licenseEndPattern[l])
                            {
                                foundEnd = false;
                                break;
                            }
                        }
                        if (foundEnd)
                        {
                            XmlDocument license = new XmlDocument();
                            license.LoadXml(Encoding.ASCII.GetString(tokens, i, k + licenseEndPattern.Length - i));
                            foundLicenses.Add(license);
                            i = k;
                            break;
                        }
                    }
                }
            }
            return foundLicenses;
        }
        #endregion

        #region Folder Functions
        /// <summary>
        /// Copy a Folder to another folder
        /// </summary>
        /// <param name="source">Source Folder</param>
        /// <param name="target">Destination Folder</param>
        public static void FolderCopy(DirectoryInfo source, DirectoryInfo target)
        {
            // Check if the target directory exists, if not, create it.
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }

            // Copy each file into its new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                FolderCopy(diSourceSubDir, nextTargetSubDir);
            }
        }

        /// <summary>
        /// Delete a folder
        /// </summary>
        /// <param name="directoryName">Folder to Delete</param>
        public static void FolderDelete(string directoryName)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryName);

            // If the directory exists it has to be deleted
            if (directoryInfo.Exists)
            {
                bool deleted = false;
                for (int attempts = 0; attempts < FileAttempts && deleted == false; attempts++)
                {
                    try
                    {
                        directoryInfo.Attributes = FileAttributes.Normal;
                        Directory.Delete(directoryName, true);
                        deleted = true;
                        Thread.Sleep(2000);
                    }
                    catch (IOException)
                    {
                        Thread.Sleep(50);
                    }
                }

                // If the folder was not deleted there was an error
                if (Directory.Exists(directoryName))
                {
                    throw new IOException("The directory \"" + directoryName + "\" could not be deleted");
                }
            }
        }

        /// <summary>
        /// Delete an Empty Folder and Subfolders
        /// </summary>
        /// <param name="rootDirectory">Root Folder to Delete</param>
        public static void DeleteEmptyFolders(string rootDirectory)
        {
            try
            {
                foreach (string d in Directory.EnumerateDirectories(rootDirectory))
                {
                    DeleteEmptyFolders(d);
                }

                IEnumerable<string> entries = Directory.EnumerateFileSystemEntries(rootDirectory);

                if (!entries.Any())
                {
                    try
                    {
                        Directory.Delete(rootDirectory);
                    }
                    catch (UnauthorizedAccessException) { }
                    catch (DirectoryNotFoundException) { }
                }
            }
            catch (UnauthorizedAccessException) { }
        }
        #endregion
        #region Network Functions
        public static IPAddress GetBroadcastAddress(IPAddress address, IPAddress subnetMask)
        {
            byte[] ipAdressBytes = address.GetAddressBytes();
            byte[] subnetMaskBytes = subnetMask.GetAddressBytes();

            if (ipAdressBytes.Length != subnetMaskBytes.Length)
                throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

            byte[] broadcastAddress = new byte[ipAdressBytes.Length];
            for (int i = 0; i < broadcastAddress.Length; i++)
            {
                broadcastAddress[i] = (byte)(ipAdressBytes[i] | (subnetMaskBytes[i] ^ 255));
            }
            return new IPAddress(broadcastAddress);
        }

        public static IPAddress GetNetworkAddress(IPAddress address, IPAddress subnetMask)
        {
            byte[] ipAdressBytes = address.GetAddressBytes();
            byte[] subnetMaskBytes = subnetMask.GetAddressBytes();

            if (ipAdressBytes.Length != subnetMaskBytes.Length)
                throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

            byte[] broadcastAddress = new byte[ipAdressBytes.Length];
            for (int i = 0; i < broadcastAddress.Length; i++)
            {
                broadcastAddress[i] = (byte)(ipAdressBytes[i] & (subnetMaskBytes[i]));
            }
            return new IPAddress(broadcastAddress);
        }

        public static bool IsValidSubnet(IPAddress subnet)
        {
            byte[] ipOctets = subnet.GetAddressBytes();
            bool restAreOnes = false;
            for (int i = 3; i >= 0; i--)
            {
                for (int j = 0; j < 8; j++)
                {
                    bool bitValue = (ipOctets[i] >> j & 1) == 1;
                    if (restAreOnes && !bitValue)
                        return false;
                    restAreOnes = bitValue;
                }
            }
            return true;
        }

        /// <summary>
        /// Check if the User is Connected to the Internet
        /// </summary>
        public static bool IsConnectedToInternet
        {
            get
            {
                try
                {
                    HttpWebRequest hwebRequest = (HttpWebRequest)WebRequest.Create("http://www.microsoft.com");
                    hwebRequest.Timeout = 10000;
                    HttpWebResponse hWebResponse = (HttpWebResponse)hwebRequest.GetResponse();
                    if (hWebResponse.StatusCode == HttpStatusCode.OK)
                    {
                        return true;
                    }
                    return false;
                }
                catch { return false; }
            }
        }
        #endregion
    }
}
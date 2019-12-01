using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Configuration;
using KMSEmulator;
using MicrosoftToolkit.Properties;

namespace MicrosoftToolkit
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // Set Current Directory to EXE Path
            string currentDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            if (!String.IsNullOrWhiteSpace(currentDir))
            {
                Environment.CurrentDirectory = currentDir;
            }

            // Get Settings from File
            if (File.Exists(SettingsManager.SettingsPath + SettingsManager.SettingsFile))
            {
                // Load Settings
                ApplicationSettingsBase settings = Settings.Default;
                SettingsManager.Load(ref settings);
            }
            else
            {
                // Create Default Settings
                SettingsManager.Save(Settings.Default);
            }

            // Start KMS Server
            //KMSServer.Start(null, new KMSServerSettings { GenerateRandomKMSPID = true });

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI());
        }
    }
}
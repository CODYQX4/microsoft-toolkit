using System;
using System.Configuration;
using System.IO;
using AutoKMS.Properties;
using Configuration;

namespace AutoKMS
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
            string currentDir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
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

            // Run Program
            AutoKMS autoKMS = new AutoKMS();
            autoKMS.RunAutoKMS();
        }
    }
}
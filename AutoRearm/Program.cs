using System;
using System.IO;

namespace AutoRearm
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

            // Run Program
            AutoRearm autoRearm = new AutoRearm();
            autoRearm.RunAutoRearm();
        }
    }
}
using System;
using System.IO;
using System.Reflection;

namespace KMSEmulator.Logging
{
    public class FileLogger : ILogger
    {
        private string _path;

        public void LogMessage(string message, bool timestamp = false)
        {
            if (_path == null)
            {
                _path = GetExePath() + "\\KMSEmulator.log";
            }

            if (timestamp)
            {
                File.AppendAllText(_path, DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss:fff ") + message + Environment.NewLine);
            }
            else
            {
                File.AppendAllText(_path, message + Environment.NewLine);
            }
        }

        private static string GetExePath()
        {
            string fullPath = Assembly.GetEntryAssembly().Location;
            return fullPath.Substring(0, fullPath.LastIndexOf('\\'));
        }
    }
}

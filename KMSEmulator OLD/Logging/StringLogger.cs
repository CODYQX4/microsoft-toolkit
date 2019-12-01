using System;
using System.Text;

namespace KMSEmulator.Logging
{
    public class StringLogger : ILogger
    {
        private static readonly StringBuilder LogBuffer = new StringBuilder();

        public void LogMessage(string message, bool timestamp = false)
        {
            if (timestamp)
            {
                LogBuffer.AppendLine(DateTime.Now.ToString("s") + "\t" + message);
            }
            else
            {
                LogBuffer.AppendLine(message);
            }
        }

        public string ReadLog(bool erase = true)
        {
            string log = LogBuffer.ToString();
            if (erase)
            {
                LogBuffer.Clear();
            }
            return log;
        }
    }
}

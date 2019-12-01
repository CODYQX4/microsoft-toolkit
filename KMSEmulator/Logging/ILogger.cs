namespace KMSEmulator.Logging
{
    public interface ILogger
    {
        void LogMessage(string message, bool timestamp = false);
    }
}

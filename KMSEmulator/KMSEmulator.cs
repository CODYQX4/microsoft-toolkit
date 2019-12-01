using System.Diagnostics;
using System.Threading;
using KMSEmulator.KMS;
using KMSEmulator.Logging;
using KMSEmulator.RPC;

namespace KMSEmulator
{
    public static class KMSServer
    {
        /// <summary>
        /// Settings Handle for KMS Server Emulator
        /// </summary>
        internal static IKMSServerSettings Settings;

        /// <summary>
        /// TCP Server Listener Handle
        /// </summary>
        private static TCPServer _listener;

        /// <summary>
        /// Check if KMS Server is Listening for KMS Clients
        /// </summary>
        /// <returns>True if the KMS Server is listening, False if it is not listening</returns>
        public static bool IsRunning()
        {
            return _listener.Running;
        }

        /// <summary>
        /// Start KMS Server
        /// </summary>
        /// <param name="logger">Any Logger method that implements ILogger.</param>
        /// <param name="settings">KMS Server Settings Object</param>
        public static void Start(ILogger logger, KMSServerSettings settings = null)
        {
            // Prevent Running Twice
            if (_listener != null && _listener.Running)
            {
                //throw new Exception("Cannot run two instances of KMS Server.");
                _listener.Stop();
            }

            // Initialize Logger if No Logger was Set
            if (logger == null)
            {
                logger = new StringLogger();
            }

            // Initialize KMS Server Settings to use with RPC Message Handler
            if (settings != null)
            {
                Settings = settings;
            }
            else
            {
                Settings = new KMSServerSettings();
            }
            RpcMessageHandler messageHandler = new RpcMessageHandler(Settings, new KMSRequestHandler(Settings, logger));

            // Kill Any Processes using the desired TCP/IP Port
            if (settings != null && settings.KillProcessOnPort)
            {
                foreach (TcpRow tcpRow in ManagedIpHelper.GetExtendedTcpTable(true))
                {
                    if (tcpRow.LocalEndPoint.Port == Settings.Port)
                    {
                        Process.GetProcessById(tcpRow.ProcessId).Kill();
                        Thread.Sleep(5000);
                        break;
                    }
                }
            }

            // Configure and Start KMS Server
            _listener = new TCPServer(messageHandler, logger);
            _listener.Start(Settings.Port);

            // Log KMS Server TCP Server Startup
            if (_listener.Running)
            {
                logger.LogMessage("KMS Port: " + Settings.Port);
                logger.LogMessage("KMS HWID: " + Settings.DefaultKMSHWID);
                logger.LogMessage("KMS Activation Interval: " + Settings.VLActivationInterval);
                logger.LogMessage("KMS Renewal Interval: " + Settings.VLRenewalInterval);
                logger.LogMessage("KMS Port Process Termination: " + Settings.KillProcessOnPort);
                logger.LogMessage("");
                logger.LogMessage("KMS Server Emulator started successfully.");
                logger.LogMessage("");
            }
        }

        /// <summary>
        /// Stop KMS Server
        /// </summary>
        public static void Stop()
        {
            _listener.Stop();
        }
    }
}

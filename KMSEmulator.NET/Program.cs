using System;
using KMSEmulator.Logging;

namespace KMSEmulator.NET
{
	class Program
	{
		static void Main(string[] args)
		{
			// Set KMS Server Settings
			KMSServerSettings kmsSettings = new KMSServerSettings
			{
				KillProcessOnPort = true,
				GenerateRandomKMSPID = true,
				DefaultKMSHWID = "364F463A8863D35F"
			};

			// Console 
			ConsoleLogger log = new ConsoleLogger();

			// Start KMS Server
			KMSServer.Start(log, kmsSettings);
			Console.ReadKey();
		}
	}
}

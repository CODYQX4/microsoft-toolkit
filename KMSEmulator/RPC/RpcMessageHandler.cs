using System;
using KMSEmulator.KMS;
using KMSEmulator.RPC.Bind;
using KMSEmulator.RPC.Request;

namespace KMSEmulator.RPC
{
	public class RpcMessageHandler : IMessageHandler
	{
		private IMessageHandler RequestMessageHandler { get; set; }
		private IKMSServerSettings Settings { get; set; }

		public RpcMessageHandler(IKMSServerSettings settings, IMessageHandler requestMessageHandler)
		{
			RequestMessageHandler = requestMessageHandler;
			Settings = settings;
		}

		public byte[] HandleRequest(byte[] request)
		{
			byte messageType = request[2];

			IMessageHandler requestHandler = GetMessageHandler(messageType);

			byte[] response = requestHandler.HandleRequest(request);
			return response;
		}

		private IMessageHandler GetMessageHandler(byte messageType)
		{
			IMessageHandler requestHandler;
			switch (messageType)
			{
				case 0x0b:
					requestHandler = new RpcBindMessageHandler(Settings);
					break;
				case 0x00:
					requestHandler = new RpcRequestMessageHandler(RequestMessageHandler);
					break;
				default:
					throw new ApplicationException("Unhandled message type: " + messageType);
			}
			return requestHandler;
		}
	}
}

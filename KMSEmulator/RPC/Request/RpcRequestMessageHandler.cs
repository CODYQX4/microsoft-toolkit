using System.IO;

namespace KMSEmulator.RPC.Request
{
	class RpcRequestMessageHandler : IMessageHandler
	{
		private IMessageHandler RequestMessageHandler { get; set; }
		public RpcRequestMessageHandler(IMessageHandler requestMessageHandler)
		{
			RequestMessageHandler = requestMessageHandler;
		}

		public byte[] HandleRequest(byte[] b)
		{
			RpcRequestMessage request = CreateRequest(b);

			RpcResponseMessage response = CreateResponse(request);

			byte[] responseBytes = CreateResponseArray(response);

			return responseBytes;
		}

		private static RpcRequestMessage CreateRequest(byte[] b)
		{
			using (MemoryStream stream = new MemoryStream(b))
			{
				using (BinaryReader binaryReader = new BinaryReader(stream))
				{
					RpcRequestMessage request = new RpcRequestMessage { Version = binaryReader.ReadByte(), VersionMinor = binaryReader.ReadByte(), PacketType = binaryReader.ReadByte(), PacketFlags = binaryReader.ReadByte(), DataRepresentation = binaryReader.ReadUInt32(), FragLength = binaryReader.ReadUInt16(), AuthLength = binaryReader.ReadUInt16(), CallId = binaryReader.ReadUInt32(), AllocHint = binaryReader.ReadUInt32(), ContextId = binaryReader.ReadUInt16(), Opnum = binaryReader.ReadUInt16() };
					request.Data = binaryReader.ReadBytes((int)request.AllocHint);
					return request;
				}
			}
		}

		private RpcResponseMessage CreateResponse(RpcRequestMessage request)
		{
			byte[] kmsRequestData = request.Data;
			byte[] responseBytes = RequestMessageHandler.HandleRequest(kmsRequestData);

			RpcResponseMessage response = new RpcResponseMessage { Data = responseBytes };
			int envelopeLength = response.Data.Length;
			response.Version = request.Version;
			response.VersionMinor = request.VersionMinor;
			response.PacketType = 0x02;
			response.PacketFlags = 0x03;
			response.DataRepresentation = request.DataRepresentation;
			response.FragLength = (ushort)(24 + envelopeLength);
			response.AuthLength = request.AuthLength;
			response.CallId = request.CallId;
			response.AllocHint = (uint)envelopeLength;
			response.ContextId = request.ContextId;
			response.CancelCount = 0x00;
			response.Opnum = (byte)request.Opnum;

			return response;
		}

		private byte[] CreateResponseArray(RpcResponseMessage response)
		{
			using (MemoryStream stream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(stream))
				{
					binaryWriter.Write(response.Version);
					binaryWriter.Write(response.VersionMinor);
					binaryWriter.Write(response.PacketType);
					binaryWriter.Write(response.PacketFlags);
					binaryWriter.Write(response.DataRepresentation);
					binaryWriter.Write(response.FragLength);
					binaryWriter.Write(response.AuthLength);
					binaryWriter.Write(response.CallId);
					binaryWriter.Write(response.AllocHint);
					binaryWriter.Write(response.ContextId);
					binaryWriter.Write(response.CancelCount);
					binaryWriter.Write(response.Opnum);
					binaryWriter.Write(response.Data);
					binaryWriter.Flush();
					stream.Position = 0;
					return stream.ToArray();
				}
			}
		}
	}
}

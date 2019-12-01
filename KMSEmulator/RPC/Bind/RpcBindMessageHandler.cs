using System.IO;
using System.Text;
using KMSEmulator.KMS;

namespace KMSEmulator.RPC.Bind
{
    class RpcBindMessageHandler : IMessageHandler
    {
        private readonly IKMSServerSettings _settings;
        public RpcBindMessageHandler(IKMSServerSettings setting)
        {
            _settings = setting;
        }

        public byte[] HandleRequest(byte[] b)
        {
            RpcBindRequest request = CreateRequest(b);

            RpcBindResponse response = CreateResponse(request);

            byte[] responseBytes = CreateResponseArray(response);

            return responseBytes;
        }

        private static byte[] CreateResponseArray(RpcBindResponse response)
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
                    binaryWriter.Write(response.MaxXmitFrag);
                    binaryWriter.Write(response.MaxRecvFrag);
                    binaryWriter.Write(response.AssocGroup);
                    binaryWriter.Write(response.SecondaryAddressLength);
                    binaryWriter.Write(response.SecondaryAddress);
                    //binaryWriter.Write((byte)0x00);
                    int padBytes = -(2 + response.SecondaryAddress.Length) & 0x3;
                    binaryWriter.Write(new byte[padBytes]); // array initialized to 0x00 by default
                    binaryWriter.Write(response.NumberOfResults);
                    foreach (CtxItem item in response.CtxItems)
                    {
                        binaryWriter.Write(item.AckResult);
                        binaryWriter.Write(item.AckReason);
                        binaryWriter.Write(item.TransferSyntax);
                        binaryWriter.Write(item.SyntaxVersion);
                    }
                    binaryWriter.Flush();
                    stream.Position = 0;
                    return stream.ToArray();
                }
            }
        }

        private RpcBindResponse CreateResponse(RpcBindRequest request)
        {
            RpcBindResponse response = new RpcBindResponse {Version = request.Version, VersionMinor = request.VersionMinor, PacketType = 0x0c, PacketFlags = 0x13, DataRepresentation = request.DataRepresentation, FragLength = 36 + 3 * 24, AuthLength = request.AuthLength, CallId = request.CallId, MaxXmitFrag = request.MaxXmitFrag, MaxRecvFrag = request.MaxRecvFrag, AssocGroup = 0x1063bf3f};
            byte[] secondaryAddress = Encoding.ASCII.GetBytes(_settings.Port + "\0");
            response.SecondaryAddressLength = (ushort)secondaryAddress.Length;
            response.SecondaryAddress = secondaryAddress;
            response.NumberOfResults = 3;
            response.CtxItems = new CtxItem[3];
            response.CtxItems[0] = new CtxItem {AckResult = 0, AckReason = 0, TransferSyntax = new byte[] {0x04, 0x5d, 0x88, 0x8a, 0xeb, 0x1c, 0xc9, 0x11, 0x9f, 0xe8, 0x08, 0x00, 0x2b, 0x10, 0x48, 0x60}, SyntaxVersion = 2};
            response.CtxItems[1] = new CtxItem {AckResult = 2, AckReason = 2, TransferSyntax = new byte[16], SyntaxVersion = 0};
            response.CtxItems[2] = new CtxItem {AckResult = 3, AckReason = 3, TransferSyntax = new byte[16], SyntaxVersion = 0};

            return response;
        }

        private static RpcBindRequest CreateRequest(byte[] b)
        {
            using (MemoryStream stream = new MemoryStream(b))
            {
                using (BinaryReader binaryReader = new BinaryReader(stream))
                {
                    RpcBindRequest request = new RpcBindRequest {Version = binaryReader.ReadByte(), VersionMinor = binaryReader.ReadByte(), PacketType = binaryReader.ReadByte(), PacketFlags = binaryReader.ReadByte(), DataRepresentation = binaryReader.ReadUInt32(), FragLength = binaryReader.ReadUInt16(), AuthLength = binaryReader.ReadUInt16(), CallId = binaryReader.ReadUInt32(), MaxXmitFrag = binaryReader.ReadUInt16(), MaxRecvFrag = binaryReader.ReadUInt16(), AssocGroup = binaryReader.ReadUInt32(), NumCtxItems = binaryReader.ReadUInt32()};
                    return request;
                }
            }
        }
    }
}

namespace KMSEmulator.RPC.Request
{
    class RpcRequestMessage : RpcMessageBase
    {
        public uint AllocHint { get; set; }
        public ushort ContextId { get; set; }
        public ushort Opnum { get; set; }
        public byte[] Data { get; set; }
    }
}

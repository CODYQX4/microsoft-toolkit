namespace KMSEmulator.RPC.Request
{
    class RpcResponseMessage : RpcMessageBase
    {
        public uint AllocHint { get; set; }
        public ushort ContextId { get; set; }
        public byte CancelCount { get; set; }
        public byte Opnum { get; set; }
        public byte[] Data { get; set; }
    }
}

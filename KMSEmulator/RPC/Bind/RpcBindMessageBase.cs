namespace KMSEmulator.RPC.Bind
{
    class RpcBindMessageBase :RpcMessageBase
    {
        public ushort MaxXmitFrag { get; set; }
        public ushort MaxRecvFrag { get; set; }
        public uint AssocGroup { get; set; }
    }
}

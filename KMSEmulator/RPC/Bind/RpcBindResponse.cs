namespace KMSEmulator.RPC.Bind
{
    class RpcBindResponse : RpcBindMessageBase
    {
        public ushort SecondaryAddressLength { get; set; }
        public byte[] SecondaryAddress { get; set; }
        public uint NumberOfResults { get; set; }
        public CtxItem[] CtxItems { get; set; }
    }

    class CtxItem
    {
        public ushort AckResult { get; set; }
        public ushort AckReason { get; set; }
        public byte[] TransferSyntax { get; set; }
        public uint SyntaxVersion { get; set; }

    }
}

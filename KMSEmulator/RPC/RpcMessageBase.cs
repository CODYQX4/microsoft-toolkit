namespace KMSEmulator.RPC
{
    class RpcMessageBase
    {
        public byte Version { get; set; }
        public byte VersionMinor { get; set; }
        public byte PacketType { get; set; }
        public byte PacketFlags { get; set; }
        public uint DataRepresentation { get; set; }
        public ushort FragLength { get; set; }
        public ushort AuthLength { get; set; }
        public uint CallId { get; set; }
    }
}

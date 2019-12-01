namespace KMSEmulator.KMS
{
	abstract class KMSResponseBase
	{
		public abstract uint BodyLength
		{
			get;
		}

		public byte[] Unknown
		{
			get
			{
				return new byte[] { 0x00, 0x00, 0x02, 0x00 };
			}
		}
		public uint BodyLength2
		{
			get
			{
				return BodyLength;
			}
		}

		public byte[] Padding
		{
			get
			{
				// Add 4 zero bytes for HRESULT "S_OK" then pad to next 32-bit boundary
				return new byte[4 + (((~BodyLength & 3) + 1) & 3)];
			}
		}
	}
}

namespace AspNetCore.ReportingServices.Rendering.WordRenderer
{
	public class HyperlinkFieldInfo : FieldInfo
	{
		private const byte HyperlinkCode = 88;

		private static readonly byte[] StartData = new byte[2]
		{
			19,
			88
		};

		private static readonly byte[] MiddleData = new byte[2]
		{
			20,
			255
		};

		private static readonly byte[] EndData = new byte[2]
		{
			21,
			128
		};

		public override byte[] Start
		{
			get
			{
				return HyperlinkFieldInfo.StartData;
			}
		}

		public override byte[] Middle
		{
			get
			{
				return HyperlinkFieldInfo.MiddleData;
			}
		}

		public override byte[] End
		{
			get
			{
				return HyperlinkFieldInfo.EndData;
			}
		}

		public HyperlinkFieldInfo(int offset, Location location)
			: base(offset, location)
		{
		}
	}
}

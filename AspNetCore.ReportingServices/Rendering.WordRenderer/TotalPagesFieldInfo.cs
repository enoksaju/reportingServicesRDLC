namespace AspNetCore.ReportingServices.Rendering.WordRenderer
{
	public class TotalPagesFieldInfo : FieldInfo
	{
		private const byte TotalPagesCode = 26;

		private static readonly byte[] StartData = new byte[2]
		{
			19,
			26
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
				return TotalPagesFieldInfo.StartData;
			}
		}

		public override byte[] Middle
		{
			get
			{
				return TotalPagesFieldInfo.MiddleData;
			}
		}

		public override byte[] End
		{
			get
			{
				return TotalPagesFieldInfo.EndData;
			}
		}

		public TotalPagesFieldInfo(int offset, Location location)
			: base(offset, location)
		{
		}
	}
}

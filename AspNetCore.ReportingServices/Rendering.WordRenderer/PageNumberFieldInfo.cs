namespace AspNetCore.ReportingServices.Rendering.WordRenderer
{
	public class PageNumberFieldInfo : FieldInfo
	{
		private const byte PageNumberCode = 33;

		private static readonly byte[] StartData = new byte[2]
		{
			19,
			33
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
				return PageNumberFieldInfo.StartData;
			}
		}

		public override byte[] Middle
		{
			get
			{
				return PageNumberFieldInfo.MiddleData;
			}
		}

		public override byte[] End
		{
			get
			{
				return PageNumberFieldInfo.EndData;
			}
		}

		public PageNumberFieldInfo(int offset, Location location)
			: base(offset, location)
		{
		}
	}
}

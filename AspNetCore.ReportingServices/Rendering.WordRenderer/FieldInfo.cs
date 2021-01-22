using System.IO;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer
{
	public abstract class FieldInfo
	{
		public enum Location
		{
			Start,
			Middle,
			End
		}

		public const byte StartCode = 19;

		public const byte MiddleCode = 20;

		public const byte EndCode = 21;

		protected int m_offset;

		protected Location m_location;

		public int Offset
		{
			get
			{
				return this.m_offset;
			}
		}

		public abstract byte[] Start
		{
			get;
		}

		public abstract byte[] Middle
		{
			get;
		}

		public abstract byte[] End
		{
			get;
		}

		public FieldInfo(int offset, Location location)
		{
			this.m_offset = offset;
			this.m_location = location;
		}

		public void WriteData(BinaryWriter dataWriter)
		{
			switch (this.m_location)
			{
			case Location.Start:
				dataWriter.Write(this.Start);
				break;
			case Location.Middle:
				dataWriter.Write(this.Middle);
				break;
			case Location.End:
				dataWriter.Write(this.End);
				break;
			}
		}
	}
}

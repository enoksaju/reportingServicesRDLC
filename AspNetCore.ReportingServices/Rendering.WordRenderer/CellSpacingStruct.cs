using AspNetCore.ReportingServices.Rendering.Utilities;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer
{
	public class CellSpacingStruct
	{
		public enum Location
		{
			Top = 1,
			Left,
			Bottom = 4,
			Right = 8,
			All = 0xF
		}

		private byte m_itcFirst;

		private byte m_itcLim;

		private byte m_grfbrc;

		private byte m_ftsWidth;

		private ushort m_wWidth;

		public int ItcFirst
		{
			set
			{
				this.m_itcFirst = (byte)value;
			}
		}

		public int ItcLim
		{
			set
			{
				this.m_itcLim = (byte)value;
			}
		}

		public int GrfBrc
		{
			set
			{
				this.m_grfbrc = (byte)value;
			}
		}

		public int FtsWidth
		{
			set
			{
				this.m_ftsWidth = (byte)value;
			}
		}

		public int Width
		{
			get
			{
				return this.m_wWidth;
			}
			set
			{
				this.m_wWidth = (ushort)value;
			}
		}

		public bool Empty
		{
			get
			{
				return this.m_wWidth == 0;
			}
		}

		public CellSpacingStruct(Location location)
		{
			this.m_grfbrc = (byte)location;
			this.m_ftsWidth = 3;
		}

		public void serialize(byte[] buf, int offset)
		{
			buf[offset] = this.m_itcFirst;
			buf[offset + 1] = this.m_itcLim;
			buf[offset + 2] = this.m_grfbrc;
			buf[offset + 3] = this.m_ftsWidth;
			LittleEndian.PutUShort(buf, offset + 4, this.m_wWidth);
		}

		public byte[] ToByteArray()
		{
			byte[] array = new byte[9];
			LittleEndian.PutUShort(array, 54834);
			array[2] = 6;
			this.serialize(array, 3);
			return array;
		}
	}
}

using AspNetCore.ReportingServices.Rendering.Utilities;
using System.Drawing;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer
{
	public class BorderCode
	{
		private int m_ico24;

		private int m_info;

		private static readonly BitField m_dptLineWidth = new BitField(255);

		private static readonly BitField m_brcType = new BitField(65280);

		private static readonly BitField m_dptSpace = new BitField(2031616);

		private static readonly BitField m_fShadow = new BitField(2097152);

		private static readonly BitField m_fFrame = new BitField(4194304);

		private byte m_97dptLineWidth;

		private byte m_97brcType;

		private ushort m_97info2;

		private static readonly BitField m_97dptSpace = new BitField(7936);

		private static readonly BitField m_97fShadow = new BitField(8192);

		private static readonly BitField _97fFrame = new BitField(16384);

		public LineStyle Style
		{
			get
			{
				return (LineStyle)BorderCode.m_brcType.GetValue(this.m_info);
			}
			set
			{
				this.m_info = BorderCode.m_brcType.SetValue(this.m_info, (int)value);
				this.m_97brcType = (byte)value;
			}
		}

		public int LineWidth
		{
			get
			{
				return BorderCode.m_dptLineWidth.GetValue(this.m_info);
			}
			set
			{
				this.m_info = BorderCode.m_dptLineWidth.SetValue(this.m_info, value);
				this.m_97dptLineWidth = (byte)value;
			}
		}

		public bool HasShadow
		{
			set
			{
				this.m_info = BorderCode.m_fShadow.SetBoolean(this.m_info, value);
				this.m_97info2 = (ushort)BorderCode.m_97fShadow.SetBoolean(this.m_97info2, value);
			}
		}

		public int Size
		{
			get
			{
				return 8;
			}
		}

		public int Ico24
		{
			get
			{
				return this.m_ico24;
			}
			set
			{
				this.m_ico24 = value;
			}
		}

		public bool Empty
		{
			get
			{
				if (this.LineWidth != 0)
				{
					return this.Style == LineStyle.None;
				}
				return true;
			}
		}

		public int Ico97
		{
			get
			{
				return 1;
			}
		}

		public BorderCode()
		{
			this.m_ico24 = -16777216;
		}

		public void SetColor(int ico24)
		{
			this.m_ico24 = ico24;
		}

		public Color GetColor()
		{
			return WordColor.getColor(this.m_ico24);
		}

		public void SetColor(ref Color color)
		{
			this.m_ico24 = WordColor.GetIco24(color);
		}

		public void Serialize2K3(byte[] buf, int offset)
		{
			LittleEndian.PutInt(buf, offset, this.m_ico24);
			int val = this.m_info;
			if (this.Style == LineStyle.Double)
			{
				val = BorderCode.m_dptLineWidth.SetValue(this.m_info, BorderCode.m_dptLineWidth.GetValue(this.m_info) / 2);
			}
			LittleEndian.PutInt(buf, offset + 4, val);
		}

		public void Serialize97(byte[] buf, int offset)
		{
			buf[offset + 1] = this.m_97brcType;
			if (this.m_97brcType == 3)
			{
				buf[offset] = (byte)((int)this.m_97dptLineWidth / 2);
			}
			else
			{
				buf[offset] = this.m_97dptLineWidth;
			}
			LittleEndian.PutUShort(buf, offset + 2, this.m_97info2);
		}

		public virtual byte[] toByteArray()
		{
			byte[] array = new byte[8];
			this.Serialize2K3(array, 0);
			return array;
		}
	}
}

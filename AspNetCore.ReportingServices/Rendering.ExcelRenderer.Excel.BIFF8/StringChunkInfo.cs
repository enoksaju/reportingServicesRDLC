namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.Excel.BIFF8
{
	public class StringChunkInfo
	{
		private bool mCompressed;

		private byte[] mData = StringChunkInfo.EMPTYARRAY;

		private int mCharPos;

		private int mCharsTotal;

		private static byte[] EMPTYARRAY = new byte[0];

		public byte[] Bytes
		{
			get
			{
				return this.mData;
			}
		}

		public int CharPos
		{
			get
			{
				return this.mCharPos;
			}
			set
			{
				this.mCharPos = value;
			}
		}

		public int CharsTotal
		{
			get
			{
				return this.mCharsTotal;
			}
			set
			{
				this.mData = StringChunkInfo.EMPTYARRAY;
				this.mCharPos = 0;
				this.mCharsTotal = value;
			}
		}

		public byte[] Data
		{
			get
			{
				return this.mData;
			}
			set
			{
				if (value == null)
				{
					this.mData = StringChunkInfo.EMPTYARRAY;
				}
				else
				{
					this.mData = value;
				}
			}
		}

		public bool Compressed
		{
			get
			{
				return this.mCompressed;
			}
			set
			{
				this.mCompressed = value;
			}
		}

		public bool HasMore
		{
			get
			{
				return this.mCharPos < this.mCharsTotal;
			}
		}
	}
}

using System.IO;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer
{
	public class EscherClientAnchorRecord : EscherRecord
	{
		public const string RECORD_DESCRIPTION = "MsofbtClientAnchor";

		public static ushort RECORD_ID = 61456;

		private ushort field_1_flag;

		private ushort field_2_col1;

		private ushort field_3_dx1;

		private ushort field_4_row1;

		private ushort field_5_dy1;

		private ushort field_6_col2;

		private ushort field_7_dx2;

		private ushort field_8_row2;

		private ushort field_9_dy2;

		private byte[] remainingData;

		private bool shortRecord;

		public override int RecordSize
		{
			get
			{
				if (!this.shortRecord)
				{
					return 26 + ((this.remainingData != null) ? this.remainingData.Length : 0);
				}
				return 8 + this.remainingData.Length;
			}
		}

		public override string RecordName
		{
			get
			{
				return "ClientAnchor";
			}
		}

		public virtual ushort Flag
		{
			get
			{
				return this.field_1_flag;
			}
			set
			{
				this.field_1_flag = value;
			}
		}

		public virtual ushort Col1
		{
			get
			{
				return this.field_2_col1;
			}
			set
			{
				this.field_2_col1 = value;
			}
		}

		public virtual ushort Dx1
		{
			get
			{
				return this.field_3_dx1;
			}
			set
			{
				this.field_3_dx1 = value;
			}
		}

		public virtual ushort Row1
		{
			get
			{
				return this.field_4_row1;
			}
			set
			{
				this.field_4_row1 = value;
			}
		}

		public virtual ushort Dy1
		{
			get
			{
				return this.field_5_dy1;
			}
			set
			{
				this.field_5_dy1 = value;
			}
		}

		public virtual ushort Col2
		{
			get
			{
				return this.field_6_col2;
			}
			set
			{
				this.field_6_col2 = value;
			}
		}

		public virtual ushort Dx2
		{
			get
			{
				return this.field_7_dx2;
			}
			set
			{
				this.field_7_dx2 = value;
			}
		}

		public virtual ushort Row2
		{
			get
			{
				return this.field_8_row2;
			}
			set
			{
				this.field_8_row2 = value;
			}
		}

		public virtual ushort Dy2
		{
			get
			{
				return this.field_9_dy2;
			}
			set
			{
				this.field_9_dy2 = value;
			}
		}

		public virtual byte[] RemainingData
		{
			get
			{
				return this.remainingData;
			}
			set
			{
				this.remainingData = value;
			}
		}

		public virtual bool ShortRecord
		{
			set
			{
				this.shortRecord = value;
			}
		}

		public EscherClientAnchorRecord()
		{
			this.remainingData = new byte[4];
			this.shortRecord = true;
		}

		public override int Serialize(BinaryWriter dataWriter)
		{
			if (this.remainingData == null)
			{
				this.remainingData = new byte[0];
			}
			dataWriter.Write(this.getOptions());
			dataWriter.Write(this.GetRecordId());
			int num = 8;
			if (this.shortRecord)
			{
				dataWriter.Write(this.remainingData.Length);
				dataWriter.Write(this.remainingData);
				return num + this.remainingData.Length;
			}
			int value = this.remainingData.Length + 18;
			dataWriter.Write(value);
			dataWriter.Write(this.field_1_flag);
			dataWriter.Write(this.field_2_col1);
			dataWriter.Write(this.field_3_dx1);
			dataWriter.Write(this.field_4_row1);
			dataWriter.Write(this.field_5_dy1);
			dataWriter.Write(this.field_6_col2);
			dataWriter.Write(this.field_7_dx2);
			dataWriter.Write(this.field_8_row2);
			dataWriter.Write(this.field_9_dy2);
			dataWriter.Write(this.remainingData);
			return num + 18 + this.remainingData.Length;
		}

		public override ushort GetRecordId()
		{
			return EscherClientAnchorRecord.RECORD_ID;
		}
	}
}

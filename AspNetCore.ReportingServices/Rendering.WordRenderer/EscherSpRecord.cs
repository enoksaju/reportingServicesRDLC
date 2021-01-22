using System.IO;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer
{
	public class EscherSpRecord : EscherRecord
	{
		public const string RECORD_DESCRIPTION = "MsofbtSp";

		public const int FLAG_GROUP = 1;

		public const int FLAG_CHILD = 2;

		public const int FLAG_PATRIARCH = 4;

		public const int FLAG_DELETED = 8;

		public const int FLAG_OLESHAPE = 16;

		public const int FLAG_HAVEMASTER = 32;

		public const int FLAG_FLIPHORIZ = 64;

		public const int FLAG_FLIPVERT = 128;

		public const int FLAG_CONNECTOR = 256;

		public const int FLAG_HAVEANCHOR = 512;

		public const int FLAG_BACKGROUND = 1024;

		public const int FLAG_HASSHAPETYPE = 2048;

		public static ushort RECORD_ID = 61450;

		private int field_1_shapeId;

		private int field_2_flags;

		public override int RecordSize
		{
			get
			{
				return 16;
			}
		}

		public override string RecordName
		{
			get
			{
				return "Sp";
			}
		}

		public virtual int ShapeId
		{
			get
			{
				return this.field_1_shapeId;
			}
			set
			{
				this.field_1_shapeId = value;
			}
		}

		public virtual int Flags
		{
			get
			{
				return this.field_2_flags;
			}
			set
			{
				this.field_2_flags = value;
			}
		}

		public EscherSpRecord()
		{
		}

		public override int Serialize(BinaryWriter dataWriter)
		{
			dataWriter.Write(this.getOptions());
			dataWriter.Write(this.GetRecordId());
			int value = 8;
			dataWriter.Write(value);
			dataWriter.Write(this.field_1_shapeId);
			dataWriter.Write(this.field_2_flags);
			return 16;
		}

		public override ushort GetRecordId()
		{
			return EscherSpRecord.RECORD_ID;
		}
	}
}

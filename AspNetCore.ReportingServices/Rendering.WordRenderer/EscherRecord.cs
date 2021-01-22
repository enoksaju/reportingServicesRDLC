using AspNetCore.ReportingServices.Rendering.Utilities;
using System;
using System.Collections;
using System.IO;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer
{
	public abstract class EscherRecord : ICloneable
	{
		public class EscherRecordHeader
		{
			private ushort options;

			private ushort recordId;

			private int remainingBytes;

			public virtual ushort Options
			{
				get
				{
					return this.options;
				}
			}

			public virtual ushort RecordId
			{
				get
				{
					return this.recordId;
				}
			}

			public virtual int RemainingBytes
			{
				get
				{
					return this.remainingBytes;
				}
			}

			public EscherRecordHeader()
			{
			}

			public static EscherRecordHeader readHeader(byte[] data, int offset)
			{
				EscherRecordHeader escherRecordHeader = new EscherRecordHeader();
				escherRecordHeader.options = LittleEndian.getUShort(data, offset);
				escherRecordHeader.recordId = LittleEndian.getUShort(data, offset + 2);
				escherRecordHeader.remainingBytes = LittleEndian.getInt(data, offset + 4);
				return escherRecordHeader;
			}

			public override string ToString()
			{
				return "EscherRecordHeader{options=" + this.options + ", recordId=" + this.recordId + ", remainingBytes=" + this.remainingBytes + "}";
			}
		}

		public const int HEADER_SIZE = 8;

		private ushort options;

		private ushort recordId;

		public virtual bool ContainerRecord
		{
			get
			{
				return (this.options & 0xF) == 15;
			}
		}

		public abstract int RecordSize
		{
			get;
		}

		public virtual IList ChildRecords
		{
			get
			{
				return ArrayList.ReadOnly(new ArrayList());
			}
			set
			{
				throw new ArgumentException("This record does not support child records.");
			}
		}

		public abstract string RecordName
		{
			get;
		}

		public virtual short Instance
		{
			get
			{
				return (short)(this.options >> 4);
			}
		}

		public EscherRecord()
		{
		}

		protected internal virtual int readHeader(byte[] data, int offset)
		{
			EscherRecordHeader escherRecordHeader = EscherRecordHeader.readHeader(data, offset);
			this.options = escherRecordHeader.Options;
			this.recordId = escherRecordHeader.RecordId;
			return escherRecordHeader.RemainingBytes;
		}

		public virtual ushort getOptions()
		{
			return this.options;
		}

		public virtual void setOptions(ushort options)
		{
			this.options = options;
		}

		public abstract int Serialize(BinaryWriter dataWriter);

		public virtual ushort GetRecordId()
		{
			return this.recordId;
		}

		public virtual void SetRecordId(ushort recordId)
		{
			this.recordId = recordId;
		}

		public virtual object Clone()
		{
			throw new SystemException("The class " + base.GetType().FullName + " needs to define a clone method");
		}

		public virtual EscherRecord GetChild(int index)
		{
			return (EscherRecord)this.ChildRecords[index];
		}

		public virtual void Display(StreamWriter w, int indent)
		{
			for (int i = 0; i < indent * 4; i++)
			{
				w.Write(' ');
			}
			w.WriteLine(this.RecordName);
		}
	}
}

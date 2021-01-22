using System.Collections;
using System.IO;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer
{
	public class EscherContainerRecord : EscherRecord
	{
		public const ushort DGG_CONTAINER = 61440;

		public const ushort BSTORE_CONTAINER = 61441;

		public const ushort DG_CONTAINER = 61442;

		public const ushort SPGR_CONTAINER = 61443;

		public const ushort SP_CONTAINER = 61444;

		public const ushort SOLVER_CONTAINER = 61445;

		private IList childRecords = new ArrayList();

		public override int RecordSize
		{
			get
			{
				int num = 0;
				IEnumerator enumerator = this.ChildRecords.GetEnumerator();
				while (enumerator.MoveNext())
				{
					EscherRecord escherRecord = (EscherRecord)enumerator.Current;
					num += escherRecord.RecordSize;
				}
				return 8 + num;
			}
		}

		public override IList ChildRecords
		{
			get
			{
				return this.childRecords;
			}
			set
			{
				this.childRecords = value;
			}
		}

		public override string RecordName
		{
			get
			{
				switch (this.GetRecordId())
				{
				case 61440:
					return "DggContainer";
				case 61441:
					return "BStoreContainer";
				case 61442:
					return "DgContainer";
				case 61443:
					return "SpgrContainer";
				case 61444:
					return "SpContainer";
				case 61445:
					return "SolverContainer";
				default:
					return "Container 0x";
				}
			}
		}

		public override int Serialize(BinaryWriter dataWriter)
		{
			dataWriter.Write(this.getOptions());
			dataWriter.Write(this.GetRecordId());
			int num = 0;
			IEnumerator enumerator = this.ChildRecords.GetEnumerator();
			while (enumerator.MoveNext())
			{
				EscherRecord escherRecord = (EscherRecord)enumerator.Current;
				num += escherRecord.RecordSize;
			}
			dataWriter.Write(num);
			int num2 = 8;
			IEnumerator enumerator2 = this.ChildRecords.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				EscherRecord escherRecord2 = (EscherRecord)enumerator2.Current;
				num2 += escherRecord2.Serialize(dataWriter);
			}
			return num2;
		}

		public override void Display(StreamWriter w, int indent)
		{
			base.Display(w, indent);
			IEnumerator enumerator = this.childRecords.GetEnumerator();
			while (enumerator.MoveNext())
			{
				EscherRecord escherRecord = (EscherRecord)enumerator.Current;
				escherRecord.Display(w, indent + 1);
			}
		}

		public virtual void addChildRecord(EscherRecord record)
		{
			this.childRecords.Add(record);
		}

		public virtual EscherRecord getChildById(ushort recordId)
		{
			IEnumerator enumerator = this.childRecords.GetEnumerator();
			while (enumerator.MoveNext())
			{
				EscherRecord escherRecord = (EscherRecord)enumerator.Current;
				if (escherRecord.GetRecordId() == recordId)
				{
					return escherRecord;
				}
			}
			return null;
		}
	}
}

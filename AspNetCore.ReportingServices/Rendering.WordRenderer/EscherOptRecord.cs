using System.Collections;
using System.IO;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer
{
	public class EscherOptRecord : EscherRecord
	{
		private class AnonymousClassComparator : IComparer
		{
			private EscherOptRecord enclosingInstance;

			public AnonymousClassComparator(EscherOptRecord enclosingInstance)
			{
				this.InitBlock(enclosingInstance);
			}

			private void InitBlock(EscherOptRecord enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}

			public virtual int Compare(object o1, object o2)
			{
				EscherProperty escherProperty = (EscherProperty)o1;
				EscherProperty escherProperty2 = (EscherProperty)o2;
				if (escherProperty.PropertyNumber >= escherProperty2.PropertyNumber)
				{
					if (escherProperty.PropertyNumber != escherProperty2.PropertyNumber)
					{
						return 1;
					}
					return 0;
				}
				return -1;
			}
		}

		public const string RECORD_DESCRIPTION = "msofbtOPT";

		public static ushort RECORD_ID = 61451;

		private IList m_properties = new ArrayList();

		public override int RecordSize
		{
			get
			{
				return 8 + this.PropertiesSize;
			}
		}

		public override string RecordName
		{
			get
			{
				return "Opt";
			}
		}

		private int PropertiesSize
		{
			get
			{
				int num = 0;
				IEnumerator enumerator = this.m_properties.GetEnumerator();
				while (enumerator.MoveNext())
				{
					EscherProperty escherProperty = (EscherProperty)enumerator.Current;
					num += escherProperty.PropertySize;
				}
				return num;
			}
		}

		public virtual IList EscherProperties
		{
			get
			{
				return this.m_properties;
			}
		}

		public EscherOptRecord()
		{
		}

		public override int Serialize(BinaryWriter dataWriter)
		{
			dataWriter.Write(this.getOptions());
			dataWriter.Write(this.GetRecordId());
			dataWriter.Write(this.PropertiesSize);
			int num = 8;
			IEnumerator enumerator = this.m_properties.GetEnumerator();
			while (enumerator.MoveNext())
			{
				EscherProperty escherProperty = (EscherProperty)enumerator.Current;
				num += escherProperty.serializeSimplePart(dataWriter);
			}
			IEnumerator enumerator2 = this.m_properties.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				EscherProperty escherProperty2 = (EscherProperty)enumerator2.Current;
				num += escherProperty2.serializeComplexPart(dataWriter);
			}
			return num;
		}

		public override ushort getOptions()
		{
			this.setOptions((ushort)(this.m_properties.Count << 4 | 3));
			return base.getOptions();
		}

		public virtual EscherProperty getEscherProperty(int index)
		{
			return (EscherProperty)this.m_properties[index];
		}

		public virtual EscherProperty getEscherPropertyByID(int id)
		{
			for (int i = 0; i < this.m_properties.Count; i++)
			{
				EscherProperty escherProperty = (EscherProperty)this.m_properties[i];
				if (escherProperty.Id == id)
				{
					return escherProperty;
				}
			}
			return null;
		}

		public virtual void addEscherProperty(EscherProperty prop)
		{
			this.m_properties.Add(prop);
		}

		public virtual void sortProperties()
		{
			((ArrayList)this.m_properties).Sort(new AnonymousClassComparator(this));
		}
	}
}

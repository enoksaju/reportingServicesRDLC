using System.IO;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer
{
	public abstract class EscherProperty
	{
		private ushort id;

		public virtual ushort Id
		{
			get
			{
				return this.id;
			}
		}

		public virtual ushort PropertyNumber
		{
			get
			{
				return (ushort)(this.id & 0x3FFF);
			}
		}

		public virtual bool Complex
		{
			get
			{
				return (this.id & 0x8000) != 0;
			}
		}

		public virtual bool BlipId
		{
			get
			{
				return (this.id & 0x4000) != 0;
			}
		}

		public virtual int PropertySize
		{
			get
			{
				return 6;
			}
		}

		public EscherProperty(ushort id)
		{
			this.id = id;
		}

		public EscherProperty(ushort propertyNumber, bool isComplex, bool isBlipId)
		{
			this.id = (ushort)(propertyNumber + (isComplex ? 32768 : 0) + (isBlipId ? 16384 : 0));
		}

		public abstract int serializeSimplePart(BinaryWriter dataWriter);

		public abstract int serializeComplexPart(BinaryWriter dataWriter);
	}
}

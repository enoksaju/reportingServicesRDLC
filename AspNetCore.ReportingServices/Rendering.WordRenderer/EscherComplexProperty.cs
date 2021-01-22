using System.IO;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer
{
	public class EscherComplexProperty : EscherProperty
	{
		public byte[] complexData = new byte[0];

		public virtual byte[] ComplexData
		{
			get
			{
				return this.complexData;
			}
			set
			{
				this.complexData = value;
			}
		}

		public override int PropertySize
		{
			get
			{
				return 6 + this.complexData.Length;
			}
		}

		public EscherComplexProperty(ushort id, byte[] complexData)
			: base(id)
		{
			this.complexData = complexData;
		}

		public EscherComplexProperty(ushort propertyNumber, bool isBlipId, byte[] complexData)
			: base(propertyNumber, true, isBlipId)
		{
			this.complexData = complexData;
		}

		public override int serializeSimplePart(BinaryWriter dataWriter)
		{
			dataWriter.Write(this.Id);
			dataWriter.Write(this.complexData.Length);
			return 6;
		}

		public override int serializeComplexPart(BinaryWriter dataWriter)
		{
			dataWriter.Write(this.complexData);
			return this.complexData.Length;
		}

		public override int GetHashCode()
		{
			return this.Id * 11;
		}
	}
}

using System;

namespace AspNetCore.ReportingServices.Rendering.ImageRenderer
{
	public sealed class CMapMapping : IComparable<CMapMapping>
	{
		public readonly ushort Source;

		public readonly ushort Destination;

		public CMapMapping(ushort source, ushort destination)
		{
			this.Source = source;
			this.Destination = destination;
		}

		public int CompareTo(CMapMapping other)
		{
			if (other == null)
			{
				return 1;
			}
			return this.Source.CompareTo(other.Source);
		}

		public int GetSourceLeftByte()
		{
			return this.Source >> 8;
		}

		public int GetSourceDelta(CMapMapping other)
		{
			return this.Source - other.Source;
		}

		public int GetDestinationDelta(CMapMapping other)
		{
			return this.Destination - other.Destination;
		}
	}
}

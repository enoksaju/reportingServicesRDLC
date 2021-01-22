using System.Collections.Generic;
using System.IO;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer
{
	public class FieldsTable
	{
		public const byte StartCode = 19;

		public const byte MiddleCode = 20;

		public const byte EndCode = 21;

		private List<FieldInfo> m_offsets;

		public int Size
		{
			get
			{
				return (this.m_offsets.Count + 1) * 4 + this.m_offsets.Count * 2;
			}
		}

		public FieldsTable()
		{
			this.m_offsets = new List<FieldInfo>();
		}

		public void Add(FieldInfo info)
		{
			this.m_offsets.Add(info);
		}

		public void WriteTo(BinaryWriter dataWriter, int startCP, int endCP)
		{
			for (int i = 0; i < this.m_offsets.Count; i++)
			{
				dataWriter.Write(this.m_offsets[i].Offset - startCP);
			}
			dataWriter.Write(endCP);
			dataWriter.Flush();
			for (int j = 0; j < this.m_offsets.Count; j++)
			{
				this.m_offsets[j].WriteData(dataWriter);
			}
		}
	}
}

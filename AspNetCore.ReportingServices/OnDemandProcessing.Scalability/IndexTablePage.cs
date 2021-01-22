using AspNetCore.ReportingServices.ReportProcessing;
using System.IO;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public sealed class IndexTablePage
	{
		public byte[] Buffer;

		public bool Dirty;

		public int PageNumber;

		public IndexTablePage PreviousPage;

		public IndexTablePage NextPage;

		public IndexTablePage(int size)
		{
			this.Buffer = new byte[size];
			this.Dirty = false;
			this.PreviousPage = null;
			this.NextPage = null;
		}

		public void Read(Stream stream)
		{
			int num = stream.Read(this.Buffer, 0, this.Buffer.Length);
			if (num == 0)
			{
				for (int i = 0; i < this.Buffer.Length; i++)
				{
					this.Buffer[i] = 0;
				}
			}
			else if (num < this.Buffer.Length)
			{
				Global.Tracer.Assert(false);
			}
			this.Dirty = false;
		}

		public void Write(Stream stream)
		{
			stream.Write(this.Buffer, 0, this.Buffer.Length);
			this.Dirty = false;
		}
	}
}

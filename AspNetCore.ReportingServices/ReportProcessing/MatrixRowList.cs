using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class MatrixRowList : ArrayList
	{
		public new MatrixRow this[int index]
		{
			get
			{
				return (MatrixRow)base[index];
			}
		}

		public MatrixRowList()
		{
		}

		public MatrixRowList(int capacity)
			: base(capacity)
		{
		}
	}
}

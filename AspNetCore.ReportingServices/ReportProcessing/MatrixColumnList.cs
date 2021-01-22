using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class MatrixColumnList : ArrayList
	{
		public new MatrixColumn this[int index]
		{
			get
			{
				return (MatrixColumn)base[index];
			}
		}

		public MatrixColumnList()
		{
		}

		public MatrixColumnList(int capacity)
			: base(capacity)
		{
		}
	}
}

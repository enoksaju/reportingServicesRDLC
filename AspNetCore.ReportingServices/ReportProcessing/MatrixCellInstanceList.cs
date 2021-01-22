using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class MatrixCellInstanceList : ArrayList
	{
		public new MatrixCellInstance this[int index]
		{
			get
			{
				return (MatrixCellInstance)base[index];
			}
		}

		public MatrixCellInstanceList()
		{
		}

		public MatrixCellInstanceList(int capacity)
			: base(capacity)
		{
		}
	}
}

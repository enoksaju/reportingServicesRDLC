using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class MatrixCellInstancesList : ArrayList
	{
		public new MatrixCellInstanceList this[int index]
		{
			get
			{
				return (MatrixCellInstanceList)base[index];
			}
		}

		public MatrixCellInstancesList()
		{
		}

		public MatrixCellInstancesList(int capacity)
			: base(capacity)
		{
		}
	}
}

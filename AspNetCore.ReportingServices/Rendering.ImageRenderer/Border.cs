using System.Collections.Generic;

namespace AspNetCore.ReportingServices.Rendering.ImageRenderer
{
	public class Border
	{
		public int RowZIndex;

		public int ColumnZIndex;

		public int RowIndex;

		public int ColumnIndex;

		public bool CompareRowFirst = true;

		public List<Operation> Operations;
	}
}

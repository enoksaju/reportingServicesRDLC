using System.Collections.Generic;

namespace AspNetCore.ReportingServices.Rendering.ImageRenderer
{
	public sealed class PDFLabel
	{
		public readonly string UniqueName;

		public readonly string Label;

		public List<PDFLabel> Children;

		public PDFLabel Parent;

		public PDFLabel(string uniqueName, string label)
		{
			this.UniqueName = uniqueName;
			this.Label = label;
		}
	}
}

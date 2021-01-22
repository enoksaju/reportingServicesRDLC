using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.ReportRendering
{
	public sealed class Line : ReportItem
	{
		public bool Slant
		{
			get
			{
				return ((AspNetCore.ReportingServices.ReportProcessing.Line)base.ReportItemDef).LineSlant;
			}
		}

		public Line(string uniqueName, int intUniqueName, AspNetCore.ReportingServices.ReportProcessing.Line reportItemDef, LineInstance reportItemInstance, RenderingContext renderingContext)
			: base(uniqueName, intUniqueName, reportItemDef, reportItemInstance, renderingContext)
		{
		}
	}
}

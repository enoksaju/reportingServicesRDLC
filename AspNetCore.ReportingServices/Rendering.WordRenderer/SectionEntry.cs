using AspNetCore.ReportingServices.Rendering.RPLProcessing;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer
{
	public class SectionEntry
	{
		public string SectionId;

		public RPLItemMeasurement HeaderMeasurement;

		public RPLItemMeasurement FooterMeasurement;

		public SectionEntry(RPLReportSection section)
		{
			this.SectionId = section.ID;
			this.HeaderMeasurement = section.Header;
			this.FooterMeasurement = section.Footer;
		}
	}
}

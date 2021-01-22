namespace AspNetCore.Reporting.Gauge.WebForms
{
	public interface ISelectable
	{
		void DrawSelection(GaugeGraphics g, bool designTimeSelection);
	}
}

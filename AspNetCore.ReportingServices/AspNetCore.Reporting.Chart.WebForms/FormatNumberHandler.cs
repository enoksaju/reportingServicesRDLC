namespace AspNetCore.Reporting.Chart.WebForms
{
	public delegate string FormatNumberHandler(object sender, double value, string format, ChartValueTypes valueType, int elementId, ChartElementType elementType);
}

namespace AspNetCore.Reporting.Chart.WebForms
{
	public interface IDataPointFilter
	{
		bool FilterDataPoint(DataPoint point, Series series, int pointIndex);
	}
}

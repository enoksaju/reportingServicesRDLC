namespace AspNetCore.Reporting.Gauge.WebForms
{
	public interface IPointerProvider
	{
		double Position
		{
			get;
			set;
		}

		void DataValueChanged(bool initialize);
	}
}

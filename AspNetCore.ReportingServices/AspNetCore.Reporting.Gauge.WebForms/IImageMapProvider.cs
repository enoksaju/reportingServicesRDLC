namespace AspNetCore.Reporting.Gauge.WebForms
{
	public interface IImageMapProvider
	{
		string Href
		{
			set;
		}

		object Tag
		{
			get;
			set;
		}

		string GetToolTip();

		string GetHref();

		string GetMapAreaAttributes();
	}
}

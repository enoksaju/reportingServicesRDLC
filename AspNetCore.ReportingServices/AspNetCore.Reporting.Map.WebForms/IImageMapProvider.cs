namespace AspNetCore.Reporting.Map.WebForms
{
	public interface IImageMapProvider
	{
		object Tag
		{
			get;
			set;
		}

		string Href
		{
			get;
			set;
		}

		string GetToolTip();

		string GetHref();

		string GetMapAreaAttributes();
	}
}

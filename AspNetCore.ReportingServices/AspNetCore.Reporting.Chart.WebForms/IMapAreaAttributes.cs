namespace AspNetCore.Reporting.Chart.WebForms
{
	public interface IMapAreaAttributes
	{
		string ToolTip
		{
			get;
			set;
		}

		string Href
		{
			get;
			set;
		}

		string MapAreaAttributes
		{
			get;
			set;
		}

		object Tag
		{
			get;
			set;
		}
	}
}

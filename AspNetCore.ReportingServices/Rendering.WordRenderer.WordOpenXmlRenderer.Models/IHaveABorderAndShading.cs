namespace AspNetCore.ReportingServices.Rendering.WordRenderer.WordOpenXmlRenderer.Models
{
	public interface IHaveABorderAndShading
	{
		string BackgroundColor
		{
			set;
		}

		OpenXmlBorderPropertiesModel BorderTop
		{
			get;
		}

		OpenXmlBorderPropertiesModel BorderBottom
		{
			get;
		}

		OpenXmlBorderPropertiesModel BorderLeft
		{
			get;
		}

		OpenXmlBorderPropertiesModel BorderRight
		{
			get;
		}
	}
}

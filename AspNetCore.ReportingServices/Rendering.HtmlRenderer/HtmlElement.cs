namespace AspNetCore.ReportingServices.Rendering.HtmlRenderer
{
	internal interface HtmlElement
	{
		void Render(IOutputStream outputStream);
	}
}

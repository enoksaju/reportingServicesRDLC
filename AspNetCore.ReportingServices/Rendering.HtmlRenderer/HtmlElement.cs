namespace AspNetCore.ReportingServices.Rendering.HtmlRenderer
{
	public interface HtmlElement
	{
		void Render(IOutputStream outputStream);
	}
}

namespace AspNetCore.ReportingServices.Rendering.HtmlRenderer
{
	public interface IHtmlRenderer
	{
		void WriteStream(byte[] bytes);

		void WriteStream(string value);
	}
}

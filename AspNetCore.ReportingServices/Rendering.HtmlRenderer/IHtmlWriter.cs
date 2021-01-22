namespace AspNetCore.ReportingServices.Rendering.HtmlRenderer
{
	public interface IHtmlWriter
	{
		bool IsBrowserIE
		{
			get;
		}

		void WriteClassName(byte[] className, byte[] classNameIfNoPrefix);
	}
}

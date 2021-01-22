namespace AspNetCore.ReportingServices.Rendering.HtmlRenderer
{
	public interface IOutputStream
	{
		void Write(string text);

		void Write(byte[] text);
	}
}

namespace AspNetCore.ReportingServices.Rendering.HtmlRenderer
{
	public interface IBackgroundRepeatAttribute
	{
		void Render(IOutputStream outputStream);
	}
}

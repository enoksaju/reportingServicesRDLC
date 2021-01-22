namespace AspNetCore.ReportingServices.Rendering.HtmlRenderer
{
	public sealed class NoBackgroundRepeat : IBackgroundRepeatAttribute
	{
		public void Render(IOutputStream outputStream)
		{
			outputStream.Write(HTMLElements.m_noRepeat);
		}
	}
}

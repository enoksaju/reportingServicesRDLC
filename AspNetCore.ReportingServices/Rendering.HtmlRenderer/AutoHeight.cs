namespace AspNetCore.ReportingServices.Rendering.HtmlRenderer
{
	public sealed class AutoHeight : ISize
	{
		public void Render(IOutputStream outputStream)
		{
			outputStream.Write(HTMLElements.m_auto);
		}
	}
}

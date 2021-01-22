namespace AspNetCore.ReportingServices.Rendering.HtmlRenderer
{
	public sealed class AutoScaleTo100Percent : ISize
	{
		public void Render(IOutputStream outputStream)
		{
			outputStream.Write(HTMLElements.m_defaultPixelSize);
		}
	}
}

using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.ReportRendering
{
	public sealed class ImageMapAreaRendering : MemberBase
	{
		public ImageMapAreaInstance m_mapAreaInstance;

		public RenderingContext m_renderingContext;

		public ImageMapAreaRendering()
			: base(false)
		{
		}
	}
}

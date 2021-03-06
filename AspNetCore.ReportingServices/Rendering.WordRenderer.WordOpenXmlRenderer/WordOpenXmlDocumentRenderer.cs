using AspNetCore.ReportingServices.Interfaces;
using AspNetCore.ReportingServices.Rendering.SPBProcessing;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer.WordOpenXmlRenderer
{
	public class WordOpenXmlDocumentRenderer : WordDocumentRendererBase
	{
		public override string LocalizedName
		{
			get
			{
				return WordRenderRes.WordOpenXmlLocalizedName;
			}
		}

		public override IWordWriter NewWordWriter()
		{
			return new WordOpenXmlWriter();
		}

		protected override WordRenderer NewWordRenderer(CreateAndRegisterStream createAndRegisterStream, DeviceInfo deviceInfoObj, AspNetCore.ReportingServices.Rendering.SPBProcessing.SPBProcessing spbProcessing, IWordWriter writer, string reportName)
		{
			return new WordOpenXmlRenderer(createAndRegisterStream, spbProcessing, writer, deviceInfoObj, reportName);
		}
	}
}

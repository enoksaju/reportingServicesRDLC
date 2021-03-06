using System.IO;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public interface IDynamicImageInstance
	{
		void SetDpi(int xDpi, int yDpi);

		void SetSize(double width, double height);

		Stream GetImage(DynamicImageInstance.ImageType type, out ActionInfoWithDynamicImageMapCollection actionImageMaps);
	}
}

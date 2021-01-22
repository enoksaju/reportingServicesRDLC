using System;
using System.IO;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public interface IDVMappingLayer : IDisposable
	{
		float DpiX
		{
			set;
		}

		float DpiY
		{
			set;
		}

		double? WidthOverride
		{
			set;
		}

		double? HeightOverride
		{
			set;
		}

		Stream GetImage(DynamicImageInstance.ImageType type);

		ActionInfoWithDynamicImageMapCollection GetImageMaps();

		Stream GetCoreXml();
	}
}

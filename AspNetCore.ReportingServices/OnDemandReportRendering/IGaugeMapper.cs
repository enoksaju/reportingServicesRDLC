using System;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public interface IGaugeMapper : IDVMappingLayer, IDisposable
	{
		void RenderGaugePanel();

		void RenderDataGaugePanel();
	}
}

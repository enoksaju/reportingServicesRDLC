using System;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public interface IChartMapper : IDVMappingLayer, IDisposable
	{
		void RenderChart();
	}
}

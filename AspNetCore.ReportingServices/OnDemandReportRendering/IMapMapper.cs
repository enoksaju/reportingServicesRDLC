using System;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public interface IMapMapper : IDVMappingLayer, IDisposable
	{
		void RenderMap();
	}
}

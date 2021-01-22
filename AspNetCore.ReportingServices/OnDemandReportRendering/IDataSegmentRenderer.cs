using AspNetCore.ReportingServices.Interfaces;
using System.IO;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public interface IDataSegmentRenderer
	{
		void RenderSegment(Report report, Stream dataSegmentQuery, CreateAndRegisterStream createAndRegisterStream);

		void ExecuteQueries(Stream executeQueriesRequest, ExecuteQueriesContext executeQueriesContext);
	}
}

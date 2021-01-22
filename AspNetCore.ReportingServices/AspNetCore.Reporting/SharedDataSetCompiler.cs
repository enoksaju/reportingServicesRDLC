using AspNetCore.ReportingServices.DataExtensions;
using AspNetCore.ReportingServices.Diagnostics;
using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.Reporting
{
	public delegate DataSetPublishingResult SharedDataSetCompiler(DataSetInfo dataSetInfo, ICatalogItemContext dataSetContext);
}

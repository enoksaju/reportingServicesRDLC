using System.Collections.Specialized;

namespace AspNetCore.ReportingServices.Diagnostics
{
	public interface IReportParameterLookup
	{
		string GetReportParamsInstanceId(NameValueCollection reportParameters);
	}
}

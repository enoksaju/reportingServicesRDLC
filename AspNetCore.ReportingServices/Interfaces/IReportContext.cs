using System.Collections.Generic;

namespace AspNetCore.ReportingServices.Interfaces
{
	internal interface IReportContext
	{
		string ReportName
		{
			get;
		}

		string ReportPath
		{
			get;
		}

		bool IsLinkedReport
		{
			get;
		}

		string LinkedReportTargetName
		{
			get;
		}

		string LinkedReportTargetPath
		{
			get;
		}

		bool IsSubreport
		{
			get;
		}

		string ParentReportName
		{
			get;
		}

		string ParentReportPath
		{
			get;
		}

		IDictionary<string, IParameter> QueryParameters
		{
			get;
		}
	}
}

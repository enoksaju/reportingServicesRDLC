using System.Collections.Generic;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public interface IROMActionOwner
	{
		string UniqueName
		{
			get;
		}

		List<string> FieldsUsedInValueExpression
		{
			get;
		}
	}
}

using System.Collections.Generic;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	public interface IActionOwner
	{
		Action Action
		{
			get;
		}

		List<string> FieldsUsedInValueExpression
		{
			get;
			set;
		}
	}
}

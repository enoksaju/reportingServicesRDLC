using System.Collections.Generic;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
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

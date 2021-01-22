using System.Collections.Generic;

namespace AspNetCore.ReportingServices.DataProcessing
{
	public interface IRestartable
	{
		IDataParameter[] StartAt(List<ScopeValueFieldName> scopeValueFieldNameCollection);
	}
}

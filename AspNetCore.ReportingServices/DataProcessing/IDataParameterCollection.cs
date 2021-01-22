using System.Collections;

namespace AspNetCore.ReportingServices.DataProcessing
{
	public interface IDataParameterCollection : IEnumerable
	{
		int Add(IDataParameter parameter);
	}
}

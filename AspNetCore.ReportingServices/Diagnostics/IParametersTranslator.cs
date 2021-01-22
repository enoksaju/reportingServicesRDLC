using System.Collections.Specialized;

namespace AspNetCore.ReportingServices.Diagnostics
{
	public interface IParametersTranslator
	{
		void GetParamsInstance(string paramsInstanceId, out ExternalItemPath itemPath, out NameValueCollection parameters);
	}
}

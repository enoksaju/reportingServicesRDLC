using System.Collections;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence
{
	public delegate T CreateDictionary<T>(int dictionaryLength) where T : IDictionary;
}

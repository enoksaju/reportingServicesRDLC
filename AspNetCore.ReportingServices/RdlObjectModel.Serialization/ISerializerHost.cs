using System;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.RdlObjectModel.Serialization
{
	public interface ISerializerHost
	{
		Type GetSubstituteType(Type type);

		void OnDeserialization(object value);

		IEnumerable<ExtensionNamespace> GetExtensionNamespaces();
	}
}

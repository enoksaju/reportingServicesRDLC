using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using System;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class DrillthroughParameters : NameObjectCollectionBase, INameObjectCollection
	{
		public DrillthroughParameters()
		{
		}

		public DrillthroughParameters(int capacity)
			: base(capacity)
		{
		}

		public DrillthroughParameters(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public void Add(string key, object value)
		{
			base.BaseAdd(key, value);
		}

		public string GetKey(int index)
		{
			return base.BaseGetKey(index);
		}

		public object GetValue(int index)
		{
			return base.BaseGet(index);
		}
	}
}

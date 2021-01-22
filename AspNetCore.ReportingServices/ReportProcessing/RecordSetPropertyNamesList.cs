using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class RecordSetPropertyNamesList : ArrayList
	{
		public new RecordSetPropertyNames this[int index]
		{
			get
			{
				return (RecordSetPropertyNames)base[index];
			}
		}

		public RecordSetPropertyNamesList()
		{
		}

		public RecordSetPropertyNamesList(int capacity)
			: base(capacity)
		{
		}

		public StringList GetPropertyNames(int aliasIndex)
		{
			if (aliasIndex >= 0 && aliasIndex < this.Count)
			{
				return this[aliasIndex].PropertyNames;
			}
			return null;
		}

		public string GetPropertyName(int aliasIndex, int propertyIndex)
		{
			StringList propertyNames = this.GetPropertyNames(aliasIndex);
			if (propertyNames != null && propertyIndex >= 0 && propertyIndex < propertyNames.Count)
			{
				return propertyNames[propertyIndex];
			}
			return null;
		}
	}
}

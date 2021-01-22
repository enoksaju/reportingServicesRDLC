using System;
using System.Collections;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class StyleAttributeHashtable : Hashtable
	{
		public AttributeInfo this[string index]
		{
			get
			{
				return (AttributeInfo)base[index];
			}
		}

		public StyleAttributeHashtable()
		{
		}

		public StyleAttributeHashtable(int capacity)
			: base(capacity)
		{
		}

		private StyleAttributeHashtable(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

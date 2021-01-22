using System;
using System.Collections;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class EmbeddedImageHashtable : Hashtable
	{
		public ImageInfo this[string index]
		{
			get
			{
				return (ImageInfo)base[index];
			}
		}

		public EmbeddedImageHashtable()
		{
		}

		public EmbeddedImageHashtable(int capacity)
			: base(capacity)
		{
		}

		private EmbeddedImageHashtable(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

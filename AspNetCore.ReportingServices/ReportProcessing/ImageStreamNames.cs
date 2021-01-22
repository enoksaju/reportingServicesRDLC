using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ImageStreamNames : Hashtable
	{
		public ImageInfo this[string url]
		{
			get
			{
				return (ImageInfo)base[url];
			}
			set
			{
				base[url] = value;
			}
		}

		public ImageStreamNames()
		{
		}

		public ImageStreamNames(int capacity)
			: base(capacity)
		{
		}
	}
}

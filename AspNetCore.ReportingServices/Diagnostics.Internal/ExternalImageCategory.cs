namespace AspNetCore.ReportingServices.Diagnostics.Internal
{
    public sealed class ExternalImageCategory
	{
		public string Count
		{
			get;
			set;
		}

		public string ByteCount
		{
			get;
			set;
		}

		public string ResourceFetchTime
		{
			get;
			set;
		}

		public ExternalImageCategory()
		{
		}
	}
}

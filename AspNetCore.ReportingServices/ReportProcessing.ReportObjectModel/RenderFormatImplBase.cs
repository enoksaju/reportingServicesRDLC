using System;

namespace AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel
{
	public abstract class RenderFormatImplBase : MarshalByRefObject
	{
		public abstract string Name
		{
			get;
		}

		public abstract bool IsInteractive
		{
			get;
		}

		public abstract ReadOnlyNameValueCollection DeviceInfo
		{
			get;
		}
	}
}

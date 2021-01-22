namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class BackgroundImageInstance : BaseInstance, IImageInstance
	{
		public abstract byte[] ImageData
		{
			get;
		}

		public abstract string StreamName
		{
			get;
		}

		public abstract string MIMEType
		{
			get;
		}

		public abstract BackgroundRepeatTypes BackgroundRepeat
		{
			get;
		}

		public abstract Positions Position
		{
			get;
		}

		public abstract ReportColor TransparentColor
		{
			get;
		}

		public BackgroundImageInstance(IReportScope reportScope)
			: base(reportScope)
		{
		}
	}
}

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class DataValueInstance : BaseInstance
	{
		public abstract string Name
		{
			get;
		}

		public abstract object Value
		{
			get;
		}

		public DataValueInstance(IReportScope repotScope)
			: base(repotScope)
		{
		}
	}
}

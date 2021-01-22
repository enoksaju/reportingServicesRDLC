namespace AspNetCore.ReportingServices.ReportRendering
{
	public interface ICustomReportItem
	{
		CustomReportItem CustomItem
		{
			set;
		}

		ReportItem RenderItem
		{
			get;
		}

		Action Action
		{
			get;
		}

		ChangeType Process();
	}
}

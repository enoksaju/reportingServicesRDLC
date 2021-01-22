namespace AspNetCore.ReportingServices.ReportProcessing
{
	public interface IPageBreakItem
	{
		bool HasPageBreaks(bool atStart);

		bool IgnorePageBreaks();
	}
}

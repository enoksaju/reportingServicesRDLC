using AspNetCore.ReportingServices.ReportPublishing;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	public interface ICreateSubtotals
	{
		void CreateAutomaticSubtotals(AutomaticSubtotalContext context);
	}
}

namespace AspNetCore.Reporting.Gauge.WebForms
{
	public interface IToolTipProvider
	{
		string GetToolTip(HitTestResult ht);
	}
}

namespace AspNetCore.ReportingServices.ReportRendering
{
	public interface IDeepCloneable
	{
		ReportItem DeepClone();
	}
}

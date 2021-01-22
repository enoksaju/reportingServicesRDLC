namespace AspNetCore.ReportingServices.ReportProcessing
{
	public interface IIndexInto
	{
		object GetChildAt(int index, out NonComputedUniqueNames nonCompNames);
	}
}

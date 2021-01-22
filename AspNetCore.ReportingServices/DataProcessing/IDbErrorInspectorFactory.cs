namespace AspNetCore.ReportingServices.DataProcessing
{
	public interface IDbErrorInspectorFactory
	{
		IDbErrorInspector CreateErrorInspector();
	}
}

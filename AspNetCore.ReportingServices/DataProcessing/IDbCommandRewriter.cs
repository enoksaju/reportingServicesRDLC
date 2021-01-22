namespace AspNetCore.ReportingServices.DataProcessing
{
	public interface IDbCommandRewriter
	{
		string RewrittenCommandText
		{
			get;
		}
	}
}

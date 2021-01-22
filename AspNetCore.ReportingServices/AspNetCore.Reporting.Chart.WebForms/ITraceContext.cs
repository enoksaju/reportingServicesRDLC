namespace AspNetCore.Reporting.Chart.WebForms
{
	public interface ITraceContext
	{
		bool TraceEnabled
		{
			get;
		}

		void Write(string category, string message);
	}
}

namespace AspNetCore.ReportingServices.DataProcessing
{
	public interface IDataMultiValueParameter : IDataParameter
	{
		object[] Values
		{
			get;
			set;
		}
	}
}

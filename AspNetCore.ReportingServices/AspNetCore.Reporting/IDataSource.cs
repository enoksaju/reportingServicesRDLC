namespace AspNetCore.Reporting
{
	public interface IDataSource
	{
		string Name
		{
			get;
		}

		object Value
		{
			get;
		}
	}
}

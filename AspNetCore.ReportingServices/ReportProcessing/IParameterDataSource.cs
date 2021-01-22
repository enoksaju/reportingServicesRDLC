namespace AspNetCore.ReportingServices.ReportProcessing
{
	public interface IParameterDataSource
	{
		int DataSourceIndex
		{
			get;
		}

		int DataSetIndex
		{
			get;
		}

		int ValueFieldIndex
		{
			get;
		}

		int LabelFieldIndex
		{
			get;
		}
	}
}

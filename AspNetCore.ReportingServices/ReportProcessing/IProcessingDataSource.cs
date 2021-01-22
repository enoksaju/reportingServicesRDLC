using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	public interface IProcessingDataSource
	{
		Guid ID
		{
			get;
		}

		string Name
		{
			get;
		}

		string Type
		{
			get;
			set;
		}

		string SharedDataSourceReferencePath
		{
			set;
		}

		string DataSourceReference
		{
			get;
		}

		bool IntegratedSecurity
		{
			get;
		}
	}
}

using AspNetCore.ReportingServices.ReportProcessing;
using System;

namespace AspNetCore.ReportingServices.OnDemandProcessing
{
	public interface IInternalProcessingContext
	{
		ErrorContext ErrorContext
		{
			get;
		}

		bool SnapshotProcessing
		{
			get;
			set;
		}

		DateTime ExecutionTime
		{
			get;
		}

		bool EnableDataBackedParameters
		{
			get;
		}
	}
}

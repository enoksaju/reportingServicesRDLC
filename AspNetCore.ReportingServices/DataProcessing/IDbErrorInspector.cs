using System;

namespace AspNetCore.ReportingServices.DataProcessing
{
	public interface IDbErrorInspector
	{
		bool IsQueryTimeout(Exception e);

		bool IsQueryMemoryLimitExceeded(Exception e);

		bool IsOnPremisesServiceException(Exception e);
	}
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	public interface IDocumentMap : IEnumerator<OnDemandDocumentMapNode>, IDisposable, IEnumerator
	{
		void Close();
	}
}

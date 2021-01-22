using AspNetCore.ReportingServices.ReportProcessing;
using System;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	[Serializable]
	public sealed class DocumentMapNode : OnDemandDocumentMapNode
	{
		public DocumentMapNode(string aLabel, string aId, int aLevel)
			: base(aLabel, aId, aLevel)
		{
		}
	}
}

using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class CustomReportItemInstanceInfo : ReportItemInstanceInfo
	{
		public CustomReportItemInstanceInfo(ReportProcessing.ProcessingContext pc, CustomReportItem reportItemDef, CustomReportItemInstance owner)
			: base(pc, reportItemDef, owner, true)
		{
		}

		public CustomReportItemInstanceInfo(CustomReportItem reportItemDef)
			: base(reportItemDef)
		{
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList members = new MemberInfoList();
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.None, members);
		}
	}
}

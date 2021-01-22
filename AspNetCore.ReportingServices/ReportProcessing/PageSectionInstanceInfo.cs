using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class PageSectionInstanceInfo : ReportItemInstanceInfo
	{
		public PageSectionInstanceInfo(ReportProcessing.ProcessingContext pc, PageSection reportItemDef, PageSectionInstance owner)
			: base(pc, reportItemDef, owner, true)
		{
		}

		public PageSectionInstanceInfo(PageSection reportItemDef)
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

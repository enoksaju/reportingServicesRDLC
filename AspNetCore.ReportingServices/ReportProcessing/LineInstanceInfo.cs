using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class LineInstanceInfo : ReportItemInstanceInfo
	{
		public LineInstanceInfo(ReportProcessing.ProcessingContext pc, Line reportItemDef, ReportItemInstance owner, int index)
			: base(pc, reportItemDef, owner, index)
		{
		}

		public LineInstanceInfo(Line reportItemDef)
			: base(reportItemDef)
		{
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList members = new MemberInfoList();
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemInstanceInfo, members);
		}
	}
}

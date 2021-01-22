using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class RectangleInstanceInfo : ReportItemInstanceInfo
	{
		public RectangleInstanceInfo(ReportProcessing.ProcessingContext pc, Rectangle reportItemDef, RectangleInstance owner, int index)
			: base(pc, reportItemDef, owner, index)
		{
		}

		public RectangleInstanceInfo(Rectangle reportItemDef)
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

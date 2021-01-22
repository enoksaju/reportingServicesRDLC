using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class LineInstance : ReportItemInstance
	{
		public LineInstance(ReportProcessing.ProcessingContext pc, Line reportItemDef, int index)
			: base(pc.CreateUniqueName(), reportItemDef)
		{
			base.m_instanceInfo = new LineInstanceInfo(pc, reportItemDef, this, index);
		}

		public LineInstance()
		{
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList members = new MemberInfoList();
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemInstance, members);
		}

		public override ReportItemInstanceInfo ReadInstanceInfo(IntermediateFormatReader reader)
		{
			Global.Tracer.Assert(base.m_instanceInfo is OffsetInfo);
			return reader.ReadLineInstanceInfo((Line)base.m_reportItemDef);
		}
	}
}

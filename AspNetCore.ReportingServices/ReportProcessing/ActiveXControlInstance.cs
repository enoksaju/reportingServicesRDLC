using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ActiveXControlInstance : ReportItemInstance
	{
		public ActiveXControlInstanceInfo InstanceInfo
		{
			get
			{
				if (base.m_instanceInfo is OffsetInfo)
				{
					Global.Tracer.Assert(false, string.Empty);
					return null;
				}
				return (ActiveXControlInstanceInfo)base.m_instanceInfo;
			}
		}

		public ActiveXControlInstance(ReportProcessing.ProcessingContext pc, ActiveXControl reportItemDef, int index)
			: base(pc.CreateUniqueName(), reportItemDef)
		{
			base.m_instanceInfo = new ActiveXControlInstanceInfo(pc, reportItemDef, this, index);
		}

		public ActiveXControlInstance()
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
			return reader.ReadActiveXControlInstanceInfo((ActiveXControl)base.m_reportItemDef);
		}
	}
}

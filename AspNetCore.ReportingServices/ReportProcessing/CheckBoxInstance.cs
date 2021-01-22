using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class CheckBoxInstance : ReportItemInstance
	{
		public CheckBoxInstanceInfo InstanceInfo
		{
			get
			{
				if (base.m_instanceInfo is OffsetInfo)
				{
					Global.Tracer.Assert(false, string.Empty);
					return null;
				}
				return (CheckBoxInstanceInfo)base.m_instanceInfo;
			}
		}

		public CheckBoxInstance(ReportProcessing.ProcessingContext pc, CheckBox reportItemDef, int index)
			: base(pc.CreateUniqueName(), reportItemDef)
		{
			base.m_instanceInfo = new CheckBoxInstanceInfo(pc, reportItemDef, this, index);
		}

		public CheckBoxInstance()
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
			return reader.ReadCheckBoxInstanceInfo((CheckBox)base.m_reportItemDef);
		}
	}
}

using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ReportItemColInstanceInfo : InstanceInfo
	{
		private NonComputedUniqueNames[] m_childrenNonComputedUniqueNames;

		public NonComputedUniqueNames[] ChildrenNonComputedUniqueNames
		{
			get
			{
				return this.m_childrenNonComputedUniqueNames;
			}
			set
			{
				this.m_childrenNonComputedUniqueNames = value;
			}
		}

		public ReportItemColInstanceInfo(ReportProcessing.ProcessingContext pc, ReportItemCollection reportItemsDef, ReportItemColInstance owner)
		{
			if (pc != null)
			{
				this.m_childrenNonComputedUniqueNames = owner.ChildrenNonComputedUniqueNames;
				if (pc.ChunkManager != null && !pc.DelayAddingInstanceInfo)
				{
					if (reportItemsDef.FirstInstance)
					{
						pc.ChunkManager.AddInstanceToFirstPage(this, owner, pc.InPageSection);
						reportItemsDef.FirstInstance = false;
					}
					else
					{
						pc.ChunkManager.AddInstance(this, owner, pc.InPageSection);
					}
				}
			}
		}

		public ReportItemColInstanceInfo()
		{
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.ChildrenNonComputedUniqueNames, Token.Array, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.NonComputedUniqueNames));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.InstanceInfo, memberInfoList);
		}
	}
}

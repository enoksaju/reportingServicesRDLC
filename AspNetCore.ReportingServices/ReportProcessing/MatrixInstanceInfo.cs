using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class MatrixInstanceInfo : ReportItemInstanceInfo
	{
		private NonComputedUniqueNames m_cornerNonComputedNames;

		private string m_noRows;

		public NonComputedUniqueNames CornerNonComputedNames
		{
			get
			{
				return this.m_cornerNonComputedNames;
			}
			set
			{
				this.m_cornerNonComputedNames = value;
			}
		}

		public string NoRows
		{
			get
			{
				return this.m_noRows;
			}
			set
			{
				this.m_noRows = value;
			}
		}

		public MatrixInstanceInfo(ReportProcessing.ProcessingContext pc, Matrix reportItemDef, MatrixInstance owner)
			: base(pc, reportItemDef, owner, false)
		{
			if (0 < reportItemDef.CornerReportItems.Count && !reportItemDef.CornerReportItems.IsReportItemComputed(0))
			{
				this.m_cornerNonComputedNames = NonComputedUniqueNames.CreateNonComputedUniqueNames(pc, reportItemDef.CornerReportItems[0]);
			}
			reportItemDef.CornerNonComputedUniqueNames = this.m_cornerNonComputedNames;
			if (!pc.DelayAddingInstanceInfo)
			{
				if (reportItemDef.FirstInstance)
				{
					pc.ChunkManager.AddInstanceToFirstPage(this, owner, pc.InPageSection);
					reportItemDef.FirstInstance = false;
				}
				else
				{
					pc.ChunkManager.AddInstance(this, owner, pc.InPageSection);
				}
			}
			this.m_noRows = pc.ReportRuntime.EvaluateDataRegionNoRowsExpression(reportItemDef, reportItemDef.ObjectType, reportItemDef.Name, "NoRows");
		}

		public MatrixInstanceInfo(Matrix reportItemDef)
			: base(reportItemDef)
		{
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.CornerNonComputedNames, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.NonComputedUniqueNames));
			memberInfoList.Add(new MemberInfo(MemberName.NoRows, Token.String));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemInstanceInfo, memberInfoList);
		}
	}
}

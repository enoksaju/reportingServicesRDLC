using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ListInstanceInfo : ReportItemInstanceInfo
	{
		private string m_noRows;

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

		public ListInstanceInfo(ReportProcessing.ProcessingContext pc, List reportItemDef, ListInstance owner)
			: base(pc, reportItemDef, owner, true)
		{
			this.m_noRows = pc.ReportRuntime.EvaluateDataRegionNoRowsExpression(reportItemDef, reportItemDef.ObjectType, reportItemDef.Name, "NoRows");
		}

		public ListInstanceInfo(List reportItemDef)
			: base(reportItemDef)
		{
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.NoRows, Token.String));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemInstanceInfo, memberInfoList);
		}
	}
}

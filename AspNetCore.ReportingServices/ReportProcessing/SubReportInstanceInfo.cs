using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class SubReportInstanceInfo : ReportItemInstanceInfo
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

		public SubReportInstanceInfo(ReportProcessing.ProcessingContext pc, SubReport reportItemDef, SubReportInstance owner, int index)
			: base(pc, reportItemDef, owner, index)
		{
			this.m_noRows = pc.ReportRuntime.EvaluateSubReportNoRowsExpression(reportItemDef, reportItemDef.Name, "NoRows");
		}

		public SubReportInstanceInfo(SubReport reportItemDef)
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

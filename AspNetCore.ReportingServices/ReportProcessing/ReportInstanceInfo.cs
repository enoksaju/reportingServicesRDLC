using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ReportInstanceInfo : ReportItemInstanceInfo
	{
		private ParameterInfoCollection m_parameters;

		private string m_reportName;

		private bool m_noRows;

		private int m_bodyUniqueName;

		public ParameterInfoCollection Parameters
		{
			get
			{
				return this.m_parameters;
			}
			set
			{
				this.m_parameters = value;
			}
		}

		public string ReportName
		{
			get
			{
				return this.m_reportName;
			}
			set
			{
				this.m_reportName = value;
			}
		}

		public bool NoRows
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

		public int BodyUniqueName
		{
			get
			{
				return this.m_bodyUniqueName;
			}
			set
			{
				this.m_bodyUniqueName = value;
			}
		}

		public ReportInstanceInfo(ReportProcessing.ProcessingContext pc, Report reportItemDef, ReportInstance owner, ParameterInfoCollection parameters, bool noRows)
			: base(pc, reportItemDef, owner, true)
		{
			this.m_bodyUniqueName = pc.CreateUniqueName();
			this.m_reportName = pc.ReportContext.ItemName;
			this.m_parameters = new ParameterInfoCollection();
			if (parameters != null && parameters.Count > 0)
			{
				parameters.CopyTo(this.m_parameters);
			}
			this.m_noRows = noRows;
		}

		public ReportInstanceInfo(Report reportItemDef)
			: base(reportItemDef)
		{
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.Parameters, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ParameterInfoCollection));
			memberInfoList.Add(new MemberInfo(MemberName.ReportName, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.NoRows, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.BodyUniqueName, Token.Int32));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemInstanceInfo, memberInfoList);
		}
	}
}

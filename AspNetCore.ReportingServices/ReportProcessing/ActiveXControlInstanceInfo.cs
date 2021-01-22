using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ActiveXControlInstanceInfo : ReportItemInstanceInfo
	{
		private object[] m_parameterValues;

		public object[] ParameterValues
		{
			get
			{
				return this.m_parameterValues;
			}
			set
			{
				this.m_parameterValues = value;
			}
		}

		public ActiveXControlInstanceInfo(ReportProcessing.ProcessingContext pc, ActiveXControl reportItemDef, ReportItemInstance owner, int index)
			: base(pc, reportItemDef, owner, index)
		{
			if (reportItemDef.Parameters != null)
			{
				this.m_parameterValues = new object[reportItemDef.Parameters.Count];
			}
		}

		public ActiveXControlInstanceInfo(ActiveXControl reportItemDef)
			: base(reportItemDef)
		{
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.ParameterValues, Token.Array, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.Variant));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemInstanceInfo, memberInfoList);
		}
	}
}

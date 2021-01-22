using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class CheckBoxInstanceInfo : ReportItemInstanceInfo
	{
		private bool m_value;

		private bool m_duplicate;

		public bool Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		public bool Duplicate
		{
			get
			{
				return this.m_duplicate;
			}
			set
			{
				this.m_duplicate = value;
			}
		}

		public CheckBoxInstanceInfo(ReportProcessing.ProcessingContext pc, CheckBox reportItemDef, ReportItemInstance owner, int index)
			: base(pc, reportItemDef, owner, index)
		{
		}

		public CheckBoxInstanceInfo(CheckBox reportItemDef)
			: base(reportItemDef)
		{
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.Value, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.Duplicate, Token.Boolean));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemInstanceInfo, memberInfoList);
		}
	}
}

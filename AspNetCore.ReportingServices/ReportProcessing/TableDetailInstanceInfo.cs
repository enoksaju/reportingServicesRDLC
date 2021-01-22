using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class TableDetailInstanceInfo : InstanceInfo
	{
		private bool m_startHidden;

		public bool StartHidden
		{
			get
			{
				return this.m_startHidden;
			}
			set
			{
				this.m_startHidden = value;
			}
		}

		public TableDetailInstanceInfo(ReportProcessing.ProcessingContext pc, TableDetail tableDetailDef, TableDetailInstance owner, Table tableDef)
		{
			if (pc.ShowHideType != 0)
			{
				this.m_startHidden = pc.ProcessReceiver(owner.UniqueName, tableDetailDef.Visibility, tableDetailDef.ExprHost, tableDef.ObjectType, tableDef.Name);
			}
			tableDetailDef.StartHidden = this.m_startHidden;
			pc.ChunkManager.AddInstance(this, owner, pc.InPageSection);
		}

		public TableDetailInstanceInfo()
		{
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.StartHidden, Token.Boolean));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.InstanceInfo, memberInfoList);
		}
	}
}

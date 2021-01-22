using AspNetCore.ReportingServices.ReportProcessing.ExprHostObjectModel;
using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class TableRowInstanceInfo : InstanceInfo
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

		public TableRowInstanceInfo(ReportProcessing.ProcessingContext pc, TableRow rowDef, TableRowInstance owner, Table tableDef, IndexedExprHost rowVisibilityHiddenExprHost)
		{
			if (pc.ShowHideType != 0)
			{
				this.m_startHidden = pc.ProcessReceiver(owner.UniqueName, rowDef.Visibility, rowVisibilityHiddenExprHost, tableDef.ObjectType, tableDef.Name);
			}
			rowDef.StartHidden = this.m_startHidden;
			pc.ChunkManager.AddInstance(this, owner, pc.InPageSection);
		}

		public TableRowInstanceInfo()
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

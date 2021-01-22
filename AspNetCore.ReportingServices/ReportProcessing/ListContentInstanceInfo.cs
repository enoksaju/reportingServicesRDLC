using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ListContentInstanceInfo : InstanceInfo
	{
		private bool m_startHidden;

		private string m_label;

		private DataValueInstanceList m_customPropertyInstances;

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

		public string Label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
			}
		}

		public DataValueInstanceList CustomPropertyInstances
		{
			get
			{
				return this.m_customPropertyInstances;
			}
			set
			{
				this.m_customPropertyInstances = value;
			}
		}

		public ListContentInstanceInfo(ReportProcessing.ProcessingContext pc, ListContentInstance owner, List listDef)
		{
			if (pc.ShowHideType != 0)
			{
				this.m_startHidden = pc.ProcessReceiver(owner.UniqueName, listDef.Visibility, listDef.ExprHost, listDef.ObjectType, listDef.Name);
			}
			listDef.StartHidden = this.m_startHidden;
			if (listDef.Grouping != null)
			{
				if (listDef.Grouping.GroupLabel != null)
				{
					this.m_label = pc.NavigationInfo.RegisterLabel(pc.ReportRuntime.EvaluateGroupingLabelExpression(listDef.Grouping, listDef.ObjectType, listDef.Name));
				}
				if (listDef.Grouping.CustomProperties != null)
				{
					this.m_customPropertyInstances = listDef.Grouping.CustomProperties.EvaluateExpressions(listDef.ObjectType, listDef.Name, listDef.Grouping.Name + ".", pc);
				}
			}
			pc.ChunkManager.AddInstance(this, owner, pc.InPageSection);
		}

		public ListContentInstanceInfo()
		{
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.StartHidden, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.Label, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.CustomPropertyInstances, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.DataValueInstanceList));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.InstanceInfo, memberInfoList);
		}
	}
}

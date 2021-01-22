using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class SimpleTextBoxInstanceInfo : InstanceInfo
	{
		private string m_formattedValue;

		private object m_originalValue;

		[NonSerialized]
		private ReportItem m_reportItemDef;

		public string FormattedValue
		{
			get
			{
				return this.m_formattedValue;
			}
			set
			{
				this.m_formattedValue = value;
			}
		}

		public object OriginalValue
		{
			get
			{
				return this.m_originalValue;
			}
			set
			{
				this.m_originalValue = value;
			}
		}

		public SimpleTextBoxInstanceInfo(ReportProcessing.ProcessingContext pc, TextBox reportItemDef, TextBoxInstance owner, int index)
		{
			this.m_reportItemDef = reportItemDef;
			ReportProcessing.RuntimeRICollection.ResetSubtotalReferences(pc);
			if (pc.ChunkManager != null && !pc.DelayAddingInstanceInfo)
			{
				pc.ChunkManager.AddInstance(this, reportItemDef, owner, index, pc.InPageSection);
			}
		}

		public SimpleTextBoxInstanceInfo(TextBox reportItemDef)
		{
			this.m_reportItemDef = reportItemDef;
		}

		public SimpleTextBoxInstanceInfo(TextBox reportItemDef, TextBoxInstanceInfo instanceInfo)
		{
			this.m_reportItemDef = reportItemDef;
			this.m_originalValue = instanceInfo.OriginalValue;
			this.m_formattedValue = instanceInfo.FormattedValue;
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.FormattedValue, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.OriginalValue, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.Variant));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.InstanceInfo, memberInfoList);
		}
	}
}

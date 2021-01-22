using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ActionInstance
	{
		private ActionItemInstanceList m_actionItemsValues;

		private object[] m_styleAttributeValues;

		private int m_uniqueName;

		public ActionItemInstanceList ActionItemsValues
		{
			get
			{
				return this.m_actionItemsValues;
			}
			set
			{
				this.m_actionItemsValues = value;
			}
		}

		public object[] StyleAttributeValues
		{
			get
			{
				return this.m_styleAttributeValues;
			}
			set
			{
				this.m_styleAttributeValues = value;
			}
		}

		public int UniqueName
		{
			get
			{
				return this.m_uniqueName;
			}
			set
			{
				this.m_uniqueName = value;
			}
		}

		public ActionInstance(ReportProcessing.ProcessingContext pc)
		{
			this.m_uniqueName = pc.CreateUniqueName();
		}

		public ActionInstance(ActionItemInstance actionItemInstance)
		{
			this.m_actionItemsValues = new ActionItemInstanceList();
			this.m_actionItemsValues.Add(actionItemInstance);
		}

		public ActionInstance()
		{
		}

		public object GetStyleAttributeValue(int index)
		{
			Global.Tracer.Assert(this.m_styleAttributeValues != null && 0 <= index && index < this.m_styleAttributeValues.Length);
			return this.m_styleAttributeValues[index];
		}

		public static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.ActionItemList, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ActionItemInstanceList));
			memberInfoList.Add(new MemberInfo(MemberName.StyleAttributeValues, Token.Array, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.Variant));
			memberInfoList.Add(new MemberInfo(MemberName.UniqueName, Token.Int32));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.None, memberInfoList);
		}
	}
}

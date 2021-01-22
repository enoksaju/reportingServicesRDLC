using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class AttributeInfo
	{
		private bool m_isExpression;

		private string m_stringValue;

		private bool m_boolValue;

		private int m_intValue;

		public bool IsExpression
		{
			get
			{
				return this.m_isExpression;
			}
			set
			{
				this.m_isExpression = value;
			}
		}

		public string Value
		{
			get
			{
				return this.m_stringValue;
			}
			set
			{
				this.m_stringValue = value;
			}
		}

		public bool BoolValue
		{
			get
			{
				return this.m_boolValue;
			}
			set
			{
				this.m_boolValue = value;
			}
		}

		public int IntValue
		{
			get
			{
				return this.m_intValue;
			}
			set
			{
				this.m_intValue = value;
			}
		}

		public static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.IsExpression, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.StringValue, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.BoolValue, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.IntValue, Token.Int32));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.None, memberInfoList);
		}
	}
}

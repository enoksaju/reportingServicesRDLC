using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class DataValueInstance
	{
		private string m_name;

		private object m_value;

		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		public object Value
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

		public DataValueInstance DeepClone()
		{
			DataValueInstance dataValueInstance = new DataValueInstance();
			if (this.m_name != null)
			{
				dataValueInstance.Name = string.Copy(this.m_name);
			}
			if (this.m_value != null)
			{
				object value = default(object);
				CustomReportItem.CloneObject(this.m_value, out value);
				dataValueInstance.Value = value;
			}
			return dataValueInstance;
		}

		public static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.Name, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.Value, Token.Object));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.None, memberInfoList);
		}
	}
}

using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class RecordSetPropertyNames
	{
		private StringList m_propertyNames;

		public StringList PropertyNames
		{
			get
			{
				return this.m_propertyNames;
			}
			set
			{
				this.m_propertyNames = value;
			}
		}

		public RecordSetPropertyNames()
		{
		}

		public static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.PropertyNames, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.StringList));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.None, memberInfoList);
		}
	}
}

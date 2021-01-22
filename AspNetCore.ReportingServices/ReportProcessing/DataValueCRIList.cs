using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class DataValueCRIList : DataValueList
	{
		private int m_rdlRowIndex = -1;

		private int m_rdlColumnIndex = -1;

		public int RDLRowIndex
		{
			get
			{
				return this.m_rdlRowIndex;
			}
			set
			{
				this.m_rdlRowIndex = value;
			}
		}

		public int RDLColumnIndex
		{
			get
			{
				return this.m_rdlColumnIndex;
			}
			set
			{
				this.m_rdlColumnIndex = value;
			}
		}

		public DataValueCRIList()
		{
		}

		public DataValueCRIList(int capacity)
			: base(capacity)
		{
		}

		public new DataValueCRIList DeepClone(InitializationContext context)
		{
			int count = this.Count;
			DataValueCRIList dataValueCRIList = new DataValueCRIList(count);
			dataValueCRIList.RDLColumnIndex = this.m_rdlColumnIndex;
			dataValueCRIList.RDLRowIndex = this.m_rdlRowIndex;
			for (int i = 0; i < count; i++)
			{
				dataValueCRIList.Add(base[i].DeepClone(context));
			}
			return dataValueCRIList;
		}

		public void Initialize(string prefix, InitializationContext context)
		{
			base.Initialize(prefix, this.m_rdlRowIndex, this.m_rdlColumnIndex, false, context);
		}

		public static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.RDLRowIndex, Token.Int32));
			memberInfoList.Add(new MemberInfo(MemberName.RDLColumnIndex, Token.Int32));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.DataValue, memberInfoList);
		}
	}
}

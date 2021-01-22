using AspNetCore.ReportingServices.ReportProcessing.ExprHostObjectModel;
using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class Field
	{
		private string m_name;

		private string m_dataField;

		private ExpressionInfo m_value;

		private int m_exprHostID = -1;

		private bool m_dynamicPropertyReferences;

		private FieldPropertyHashtable m_referencedProperties;

		[NonSerialized]
		private CalcFieldExprHost m_exprHost;

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

		public string DataField
		{
			get
			{
				return this.m_dataField;
			}
			set
			{
				this.m_dataField = value;
			}
		}

		public ExpressionInfo Value
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

		public bool IsCalculatedField
		{
			get
			{
				return this.m_dataField == null;
			}
		}

		public int ExprHostID
		{
			get
			{
				return this.m_exprHostID;
			}
			set
			{
				this.m_exprHostID = value;
			}
		}

		public bool DynamicPropertyReferences
		{
			get
			{
				return this.m_dynamicPropertyReferences;
			}
			set
			{
				this.m_dynamicPropertyReferences = value;
			}
		}

		public FieldPropertyHashtable ReferencedProperties
		{
			get
			{
				return this.m_referencedProperties;
			}
			set
			{
				this.m_referencedProperties = value;
			}
		}

		public CalcFieldExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		public void Initialize(InitializationContext context)
		{
			if (this.Value != null)
			{
				context.ExprHostBuilder.CalcFieldStart(this.m_name);
				this.m_value.Initialize("Field", context);
				context.ExprHostBuilder.GenericValue(this.m_value);
				this.m_exprHostID = context.ExprHostBuilder.CalcFieldEnd();
			}
		}

		public void SetExprHost(DataSetExprHost dataSetExprHost, ObjectModelImpl reportObjectModel)
		{
			if (this.ExprHostID >= 0)
			{
				Global.Tracer.Assert(dataSetExprHost != null && reportObjectModel != null);
				this.m_exprHost = dataSetExprHost.FieldHostsRemotable[this.ExprHostID];
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
			}
		}

		public static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.Name, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.DataField, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.Value, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ExpressionInfo));
			memberInfoList.Add(new MemberInfo(MemberName.ExprHostID, Token.Int32));
			memberInfoList.Add(new MemberInfo(MemberName.DynamicPropertyReferences, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.ReferencedProperties, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.FieldPropertyHashtable));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.None, memberInfoList);
		}
	}
}

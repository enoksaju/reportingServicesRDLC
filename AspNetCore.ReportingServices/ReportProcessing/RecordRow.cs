using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class RecordRow
	{
		private RecordField[] m_recordFields;

		private bool m_isAggregateRow;

		private int m_aggregationFieldCount;

		public RecordField[] RecordFields
		{
			get
			{
				return this.m_recordFields;
			}
			set
			{
				this.m_recordFields = value;
			}
		}

		public bool IsAggregateRow
		{
			get
			{
				return this.m_isAggregateRow;
			}
			set
			{
				this.m_isAggregateRow = value;
			}
		}

		public int AggregationFieldCount
		{
			get
			{
				return this.m_aggregationFieldCount;
			}
			set
			{
				this.m_aggregationFieldCount = value;
			}
		}

		public RecordRow(FieldsImpl fields, int fieldCount)
		{
			this.m_recordFields = new RecordField[fieldCount];
			for (int i = 0; i < fieldCount; i++)
			{
				if (!fields[i].IsMissing)
				{
					this.m_recordFields[i] = new RecordField(fields[i]);
				}
			}
			this.m_isAggregateRow = fields.IsAggregateRow;
			this.m_aggregationFieldCount = fields.AggregationFieldCount;
		}

		public RecordRow()
		{
		}

		public object GetFieldValue(int aliasIndex)
		{
			RecordField recordField = this.m_recordFields[aliasIndex];
			Global.Tracer.Assert(null != recordField);
			if (recordField.FieldStatus != 0)
			{
				throw new ReportProcessingException_FieldError(recordField.FieldStatus, ReportRuntime.GetErrorName(recordField.FieldStatus, null));
			}
			return recordField.FieldValue;
		}

		public bool IsAggregationField(int aliasIndex)
		{
			return this.m_recordFields[aliasIndex].IsAggregationField;
		}

		public static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.RecordFields, Token.Array, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.RecordField));
			memberInfoList.Add(new MemberInfo(MemberName.IsAggregateRow, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.AggregationFieldCount, Token.Int32));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.None, memberInfoList);
		}
	}
}

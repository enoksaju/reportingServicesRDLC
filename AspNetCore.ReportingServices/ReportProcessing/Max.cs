using System;
using System.Globalization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	public sealed class Max : DataAggregate
	{
		private DataTypeCode m_expressionType;

		private object m_currentMax;

		private CompareInfo m_compareInfo;

		private CompareOptions m_compareOptions;

		public Max(CompareInfo compareInfo, CompareOptions compareOptions)
		{
			this.m_currentMax = null;
			this.m_compareInfo = compareInfo;
			this.m_compareOptions = compareOptions;
		}

		public override void Init()
		{
			this.m_currentMax = null;
		}

		public override void Update(object[] expressions, IErrorContext iErrorContext)
		{
			Global.Tracer.Assert(null != expressions);
			Global.Tracer.Assert(1 == expressions.Length);
			object obj = expressions[0];
			DataTypeCode typeCode = DataAggregate.GetTypeCode(obj);
			if (!DataAggregate.IsNull(typeCode))
			{
				if (!DataAggregate.IsVariant(typeCode))
				{
					iErrorContext.Register(ProcessingErrorCode.rsMinMaxOfNonSortableData, Severity.Warning);
					throw new InvalidOperationException();
				}
				if (this.m_expressionType == DataTypeCode.Null)
				{
					this.m_expressionType = typeCode;
				}
				else if (typeCode != this.m_expressionType)
				{
					iErrorContext.Register(ProcessingErrorCode.rsAggregateOfMixedDataTypes, Severity.Warning);
					throw new InvalidOperationException();
				}
				if (this.m_currentMax == null)
				{
					this.m_currentMax = obj;
				}
				else
				{
					try
					{
						int num = ReportProcessing.CompareTo(this.m_currentMax, obj, this.m_compareInfo, this.m_compareOptions);
						if (num < 0)
						{
							this.m_currentMax = obj;
						}
					}
					catch
					{
						iErrorContext.Register(ProcessingErrorCode.rsMinMaxOfNonSortableData, Severity.Warning);
					}
				}
			}
		}

		public override object Result()
		{
			return this.m_currentMax;
		}
	}
}

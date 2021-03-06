using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	public sealed class CountDistinct : DataAggregate
	{
		private Hashtable m_distinctValues = new Hashtable();

		public override void Init()
		{
			this.m_distinctValues.Clear();
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
					iErrorContext.Register(ProcessingErrorCode.rsInvalidExpressionDataType, Severity.Warning);
					throw new InvalidOperationException();
				}
				if (!this.m_distinctValues.ContainsKey(obj))
				{
					this.m_distinctValues.Add(obj, null);
				}
			}
		}

		public override object Result()
		{
			return this.m_distinctValues.Count;
		}
	}
}

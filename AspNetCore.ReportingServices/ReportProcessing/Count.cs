namespace AspNetCore.ReportingServices.ReportProcessing
{
	public sealed class Count : DataAggregate
	{
		private int m_currentTotal;

		public override void Init()
		{
			this.m_currentTotal = 0;
		}

		public override void Update(object[] expressions, IErrorContext iErrorContext)
		{
			Global.Tracer.Assert(null != expressions);
			Global.Tracer.Assert(1 == expressions.Length);
			object o = expressions[0];
			DataTypeCode typeCode = DataAggregate.GetTypeCode(o);
			if (!DataAggregate.IsNull(typeCode))
			{
				this.m_currentTotal++;
			}
		}

		public override object Result()
		{
			return this.m_currentTotal;
		}
	}
}

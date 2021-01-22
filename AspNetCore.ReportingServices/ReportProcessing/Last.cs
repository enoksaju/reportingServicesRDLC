namespace AspNetCore.ReportingServices.ReportProcessing
{
	public sealed class Last : DataAggregate
	{
		private object m_value;

		public override void Init()
		{
			this.m_value = null;
		}

		public override void Update(object[] expressions, IErrorContext iErrorContext)
		{
			Global.Tracer.Assert(null != expressions);
			Global.Tracer.Assert(1 == expressions.Length);
			this.m_value = expressions[0];
		}

		public override object Result()
		{
			return this.m_value;
		}
	}
}

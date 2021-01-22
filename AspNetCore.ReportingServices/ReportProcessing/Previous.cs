namespace AspNetCore.ReportingServices.ReportProcessing
{
	public sealed class Previous : DataAggregate
	{
		private object m_value;

		private object m_previous;

		public override void Init()
		{
			this.m_value = null;
			this.m_previous = null;
		}

		public override void Update(object[] expressions, IErrorContext iErrorContext)
		{
			Global.Tracer.Assert(null != expressions);
			Global.Tracer.Assert(1 == expressions.Length);
			this.m_previous = this.m_value;
			this.m_value = expressions[0];
		}

		public override object Result()
		{
			return this.m_previous;
		}
	}
}

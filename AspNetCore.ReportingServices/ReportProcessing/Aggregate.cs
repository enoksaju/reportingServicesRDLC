namespace AspNetCore.ReportingServices.ReportProcessing
{
	public class Aggregate : DataAggregate
	{
		private object m_value;

		public override void Init()
		{
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

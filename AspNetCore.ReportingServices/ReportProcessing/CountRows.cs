namespace AspNetCore.ReportingServices.ReportProcessing
{
	public sealed class CountRows : DataAggregate
	{
		private int m_currentTotal;

		public override void Init()
		{
			this.m_currentTotal = 0;
		}

		public override void Update(object[] expressions, IErrorContext iErrorContext)
		{
			this.m_currentTotal++;
		}

		public override object Result()
		{
			return this.m_currentTotal;
		}
	}
}

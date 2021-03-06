namespace AspNetCore.ReportingServices.Diagnostics
{
	public sealed class ReturnValue
	{
		private readonly object m_value;

		public object Value
		{
			get
			{
				return this.m_value;
			}
		}

		public ReturnValue(object value)
		{
			this.m_value = value;
		}
	}
}

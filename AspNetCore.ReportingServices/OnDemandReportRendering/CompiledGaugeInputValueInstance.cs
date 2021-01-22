namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class CompiledGaugeInputValueInstance
	{
		private object m_value;

		public object Value
		{
			get
			{
				return this.m_value;
			}
		}

		public CompiledGaugeInputValueInstance(object value)
		{
			this.m_value = value;
		}
	}
}

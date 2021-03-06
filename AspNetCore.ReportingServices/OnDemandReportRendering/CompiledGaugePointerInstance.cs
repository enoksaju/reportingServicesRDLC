namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class CompiledGaugePointerInstance
	{
		private CompiledGaugeInputValueInstance m_gaugeInputValue;

		public CompiledGaugeInputValueInstance GaugeInputValue
		{
			get
			{
				return this.m_gaugeInputValue;
			}
			internal set
			{
				this.m_gaugeInputValue = value;
			}
		}
	}
}

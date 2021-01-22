namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class NumericIndicatorInstance : GaugePanelItemInstance
	{
		private NumericIndicator m_defObject;

		public NumericIndicatorInstance(NumericIndicator defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
		}
	}
}

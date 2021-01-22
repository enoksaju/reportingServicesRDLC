using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class Field
	{
		private AspNetCore.ReportingServices.ReportIntermediateFormat.Field m_fieldDef;

		public string Name
		{
			get
			{
				return this.m_fieldDef.Name;
			}
		}

		public string DataField
		{
			get
			{
				return this.m_fieldDef.DataField;
			}
		}

		public Field(AspNetCore.ReportingServices.ReportIntermediateFormat.Field fieldDef)
		{
			this.m_fieldDef = fieldDef;
		}
	}
}

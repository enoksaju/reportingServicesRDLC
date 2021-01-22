using AspNetCore.ReportingServices.ReportProcessing;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;

namespace AspNetCore.ReportingServices.OnDemandProcessing
{
	public class LegacyReportParameterDataSetCache : ReportParameterDataSetCache
	{
		public LegacyReportParameterDataSetCache(ProcessReportParameters aParamProcessor, ParameterInfo aParameter, ParameterDef aParamDef, bool aProcessValidValues, bool aProcessDefaultValues)
			: base(aParamProcessor, aParameter, aParamDef, aProcessValidValues, aProcessDefaultValues)
		{
		}

		public override object GetFieldValue(object aRow, int col)
		{
			FieldImpl[] array = (FieldImpl[])aRow;
			if (array[col].IsMissing)
			{
				return null;
			}
			return array[col].Value;
		}
	}
}

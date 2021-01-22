using AspNetCore.ReportingServices.ReportIntermediateFormat;
using AspNetCore.ReportingServices.ReportProcessing;
using AspNetCore.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace AspNetCore.ReportingServices.OnDemandProcessing
{
	public class OnDemandReportParameterDataSetCache : ReportParameterDataSetCache
	{
		public OnDemandReportParameterDataSetCache(ProcessReportParameters aParamProcessor, ParameterInfo aParameter, AspNetCore.ReportingServices.ReportIntermediateFormat.ParameterDef aParamDef, bool aProcessValidValues, bool aProcessDefaultValues)
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

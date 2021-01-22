using AspNetCore.ReportingServices.Diagnostics.Utilities;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	public class VarP : VarBase
	{
		public override object Result()
		{
			switch (base.m_sumOfXType)
			{
			case DataTypeCode.Null:
				return null;
			case DataTypeCode.Double:
				return ((double)base.m_currentCount * (double)base.m_sumOfXSquared - (double)base.m_sumOfX * (double)base.m_sumOfX) / (double)(base.m_currentCount * base.m_currentCount);
			case DataTypeCode.Decimal:
				return ((decimal)base.m_currentCount * (decimal)base.m_sumOfXSquared - (decimal)base.m_sumOfX * (decimal)base.m_sumOfX) / (decimal)(base.m_currentCount * base.m_currentCount);
			default:
				Global.Tracer.Assert(false);
				throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
			}
		}
	}
}

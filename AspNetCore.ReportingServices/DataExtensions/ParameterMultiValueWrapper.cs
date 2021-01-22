using AspNetCore.ReportingServices.DataProcessing;
using System.Data;

namespace AspNetCore.ReportingServices.DataExtensions
{
	public class ParameterMultiValueWrapper : ParameterWrapper, IDataMultiValueParameter, AspNetCore.ReportingServices.DataProcessing.IDataParameter
	{
		private object[] m_values;

		public virtual object[] Values
		{
			get
			{
				return this.m_values;
			}
			set
			{
				this.m_values = value;
			}
		}

		public ParameterMultiValueWrapper(System.Data.IDataParameter param)
			: base(param)
		{
		}
	}
}

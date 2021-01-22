using AspNetCore.ReportingServices.DataProcessing;

namespace AspNetCore.Reporting
{
	public class DataSetProcessingParameter : IDataMultiValueParameter, IDataParameter
	{
		public string ParameterName { get; set; }

		public object Value { get; set; }

        public object[] Values { get; set; }
    }
}

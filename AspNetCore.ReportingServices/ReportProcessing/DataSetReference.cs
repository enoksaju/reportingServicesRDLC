namespace AspNetCore.ReportingServices.ReportProcessing
{
	public sealed class DataSetReference
	{
		private string m_dataSet;

		private string m_valueAlias;

		private string m_labelAlias;

		public string DataSet
		{
			get
			{
				return this.m_dataSet;
			}
		}

		public string ValueAlias
		{
			get
			{
				return this.m_valueAlias;
			}
		}

		public string LabelAlias
		{
			get
			{
				return this.m_labelAlias;
			}
		}

		public DataSetReference(string dataSet, string valueAlias, string labelAlias)
		{
			this.m_dataSet = dataSet;
			this.m_valueAlias = valueAlias;
			this.m_labelAlias = labelAlias;
		}
	}
}

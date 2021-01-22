namespace AspNetCore.ReportingServices.DataProcessing
{
	public class ScopeValueFieldName
	{
		private readonly string m_fieldName;

		private readonly object m_value;

		public object ScopeValue
		{
			get
			{
				return this.m_value;
			}
		}

		public string FieldName
		{
			get
			{
				return this.m_fieldName;
			}
		}

		public ScopeValueFieldName(string fieldName, object value)
		{
			this.m_fieldName = fieldName;
			this.m_value = value;
		}
	}
}

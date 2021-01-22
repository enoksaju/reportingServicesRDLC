namespace AspNetCore.ReportingServices.ReportProcessing
{
	public sealed class YukonDataSetInfo
	{
		private StringList m_parameterNames;

		private bool m_isComplex;

		private int m_dataSetDefIndex = -1;

		private int m_dataSourceIndex = -1;

		private int m_dataSetIndex = -1;

		private int m_calculatedFieldIndex;

		public int DataSourceIndex
		{
			get
			{
				return this.m_dataSourceIndex;
			}
			set
			{
				this.m_dataSourceIndex = value;
			}
		}

		public int DataSetIndex
		{
			get
			{
				return this.m_dataSetIndex;
			}
			set
			{
				this.m_dataSetIndex = value;
			}
		}

		public int DataSetDefIndex
		{
			get
			{
				return this.m_dataSetDefIndex;
			}
		}

		public bool IsComplex
		{
			get
			{
				return this.m_isComplex;
			}
		}

		public StringList ParameterNames
		{
			get
			{
				return this.m_parameterNames;
			}
		}

		public int CalculatedFieldIndex
		{
			get
			{
				return this.m_calculatedFieldIndex;
			}
			set
			{
				this.m_calculatedFieldIndex = value;
			}
		}

		public YukonDataSetInfo(int index, bool isComplex, StringList parameterNames)
		{
			this.m_dataSetDefIndex = index;
			this.m_isComplex = isComplex;
			this.m_parameterNames = parameterNames;
		}

		public void MergeFlagsFromDataSource(bool isComplex, StringList datasourceParameterNames)
		{
			this.m_isComplex |= isComplex;
			if (this.m_parameterNames == null)
			{
				this.m_parameterNames = datasourceParameterNames;
			}
			else if (datasourceParameterNames != null)
			{
				this.m_parameterNames.InsertRange(0, datasourceParameterNames);
			}
		}
	}
}

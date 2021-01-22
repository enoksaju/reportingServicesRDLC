namespace AspNetCore.ReportingServices.ReportPublishing
{
	public sealed class DynamicParameter
	{
		private DataSetReference m_validValueDataSet;

		private DataSetReference m_defaultDataSet;

		private int m_index;

		private bool m_isComplex;

		public DataSetReference ValidValueDataSet
		{
			get
			{
				return this.m_validValueDataSet;
			}
		}

		public DataSetReference DefaultDataSet
		{
			get
			{
				return this.m_defaultDataSet;
			}
		}

		public int Index
		{
			get
			{
				return this.m_index;
			}
		}

		public bool IsComplex
		{
			get
			{
				return this.m_isComplex;
			}
		}

		public DynamicParameter(DataSetReference validValueDataSet, DataSetReference defaultDataSet, int index, bool isComplex)
		{
			this.m_validValueDataSet = validValueDataSet;
			this.m_defaultDataSet = defaultDataSet;
			this.m_index = index;
			this.m_isComplex = isComplex;
		}
	}
}

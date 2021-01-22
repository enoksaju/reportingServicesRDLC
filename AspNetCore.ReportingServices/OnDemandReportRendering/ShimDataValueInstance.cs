namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class ShimDataValueInstance : DataValueInstance
	{
		private string m_name;

		private object m_value;

		public override string Name
		{
			get
			{
				return this.m_name;
			}
		}

		public override object Value
		{
			get
			{
				return this.m_value;
			}
		}

		public ShimDataValueInstance(string name, object value)
			: base(null)
		{
			this.m_name = name;
			this.m_value = value;
		}

		public void Update(string name, object value)
		{
			this.m_name = name;
			this.m_value = value;
		}

		protected override void ResetInstanceCache()
		{
		}
	}
}

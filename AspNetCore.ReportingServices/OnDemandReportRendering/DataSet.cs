using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class DataSet
	{
		private FieldCollection m_fields;

		private DataSetInstance m_instance;

		private AspNetCore.ReportingServices.ReportIntermediateFormat.DataSet m_dataSetDef;

		private RenderingContext m_renderingContext;

		public string Name
		{
			get
			{
				return this.m_dataSetDef.Name;
			}
		}

		public DataSetInstance Instance
		{
			get
			{
				if (this.m_renderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new DataSetInstance(this);
				}
				return this.m_instance;
			}
		}

		public FieldCollection NonCalculatedFields
		{
			get
			{
				if (this.m_fields == null)
				{
					this.m_fields = new FieldCollection(this.m_dataSetDef);
				}
				return this.m_fields;
			}
		}

		public AspNetCore.ReportingServices.ReportIntermediateFormat.DataSet DataSetDef
		{
			get
			{
				return this.m_dataSetDef;
			}
		}

		public RenderingContext RenderingContext
		{
			get
			{
				return this.m_renderingContext;
			}
		}

		public DataSet(AspNetCore.ReportingServices.ReportIntermediateFormat.DataSet dataSetDef, RenderingContext renderingContext)
		{
			this.m_dataSetDef = dataSetDef;
			this.m_renderingContext = renderingContext;
		}

		public void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}
	}
}

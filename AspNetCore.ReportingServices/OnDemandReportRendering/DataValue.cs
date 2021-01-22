using AspNetCore.ReportingServices.ReportIntermediateFormat;
using AspNetCore.ReportingServices.ReportRendering;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class DataValue
	{
		private bool m_isChartValue;

		private AspNetCore.ReportingServices.ReportIntermediateFormat.DataValue m_dataValue;

		private ReportStringProperty m_name;

		private ReportVariantProperty m_value;

		private DataValueInstance m_instance;

		private IInstancePath m_instancePath;

		private RenderingContext m_renderingContext;

		private string m_objectName;

		public ReportStringProperty Name
		{
			get
			{
				return this.m_name;
			}
		}

		public ReportVariantProperty Value
		{
			get
			{
				return this.m_value;
			}
		}

		public AspNetCore.ReportingServices.ReportIntermediateFormat.DataValue DataValueDef
		{
			get
			{
				return this.m_dataValue;
			}
		}

		public RenderingContext RenderingContext
		{
			get
			{
				return this.m_renderingContext;
			}
		}

		public bool IsChart
		{
			get
			{
				return this.m_isChartValue;
			}
		}

		public IInstancePath InstancePath
		{
			get
			{
				return this.m_instancePath;
			}
		}

		public string ObjectName
		{
			get
			{
				return this.m_objectName;
			}
		}

		public DataValueInstance Instance
		{
			get
			{
				if (this.m_renderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				return this.m_instance;
			}
		}

		public DataValue(RenderingContext renderingContext, object chartDataValue)
		{
			this.m_isChartValue = true;
			this.m_name = new ReportStringProperty();
			this.m_value = new ReportVariantProperty(true);
			this.m_instance = new ShimDataValueInstance(null, chartDataValue);
			this.m_renderingContext = renderingContext;
		}

		public DataValue(RenderingContext renderingContext, AspNetCore.ReportingServices.ReportRendering.DataValue dataValue)
		{
			this.m_isChartValue = false;
			string name = (dataValue != null) ? dataValue.Name : null;
			object value = (dataValue != null) ? dataValue.Value : null;
			this.m_name = new ReportStringProperty(true, null, null);
			this.m_value = new ReportVariantProperty(true);
			this.m_instance = new ShimDataValueInstance(name, value);
			this.m_renderingContext = renderingContext;
		}

		public DataValue(IReportScope reportScope, RenderingContext renderingContext, AspNetCore.ReportingServices.ReportIntermediateFormat.DataValue dataValue, bool isChart, IInstancePath instancePath, string objectName)
		{
			this.m_isChartValue = isChart;
			this.m_instancePath = instancePath;
			this.m_dataValue = dataValue;
			this.m_name = new ReportStringProperty(dataValue.Name);
			this.m_value = new ReportVariantProperty(dataValue.Value);
			this.m_instance = new InternalDataValueInstance(reportScope, this);
			this.m_renderingContext = renderingContext;
			this.m_objectName = objectName;
		}

		public void UpdateChartDataValue(object dataValue)
		{
			if (this.m_dataValue == null && this.m_isChartValue)
			{
				((ShimDataValueInstance)this.m_instance).Update(null, dataValue);
			}
		}

		public void UpdateDataCellValue(AspNetCore.ReportingServices.ReportRendering.DataValue dataValue)
		{
			if (this.m_dataValue == null && !this.m_isChartValue)
			{
				string name = (dataValue != null) ? dataValue.Name : null;
				object value = (dataValue != null) ? dataValue.Value : null;
				((ShimDataValueInstance)this.m_instance).Update(name, value);
			}
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

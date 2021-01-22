using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class ChartDerivedSeries : ChartObjectCollectionItem<BaseInstance>
	{
		private Chart m_chart;

		private AspNetCore.ReportingServices.ReportIntermediateFormat.ChartDerivedSeries m_chartDerivedSeriesDef;

		private ChartSeries m_series;

		private ChartFormulaParameterCollection m_chartFormulaParameters;

		private InternalChartSeries m_sourceSeries;

		public ChartSeries Series
		{
			get
			{
				if (this.m_series == null && !this.m_chart.IsOldSnapshot && this.m_chartDerivedSeriesDef.Series != null)
				{
					this.m_series = new InternalChartSeries(this);
				}
				return this.m_series;
			}
		}

		public ChartFormulaParameterCollection FormulaParameters
		{
			get
			{
				if (this.m_chartFormulaParameters == null && !this.m_chart.IsOldSnapshot && this.ChartDerivedSeriesDef.FormulaParameters != null)
				{
					this.m_chartFormulaParameters = new ChartFormulaParameterCollection(this, this.m_chart);
				}
				return this.m_chartFormulaParameters;
			}
		}

		public string SourceChartSeriesName
		{
			get
			{
				if (this.m_chart.IsOldSnapshot)
				{
					return null;
				}
				return this.m_chartDerivedSeriesDef.SourceChartSeriesName;
			}
		}

		public InternalChartSeries SourceSeries
		{
			get
			{
				if (this.m_sourceSeries == null)
				{
					this.m_sourceSeries = ((InternalChartSeriesCollection)this.m_chart.ChartData.SeriesCollection).GetByName(this.SourceChartSeriesName);
				}
				return this.m_sourceSeries;
			}
		}

		public ChartSeriesFormula DerivedSeriesFormula
		{
			get
			{
				return this.m_chartDerivedSeriesDef.DerivedSeriesFormula;
			}
		}

		public IReportScope ReportScope
		{
			get
			{
				return this.SourceSeries.ReportScope;
			}
		}

		public Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		public AspNetCore.ReportingServices.ReportIntermediateFormat.ChartDerivedSeries ChartDerivedSeriesDef
		{
			get
			{
				return this.m_chartDerivedSeriesDef;
			}
		}

		public ChartDerivedSeries(AspNetCore.ReportingServices.ReportIntermediateFormat.ChartDerivedSeries chartDerivedSeriesDef, Chart chart)
		{
			this.m_chartDerivedSeriesDef = chartDerivedSeriesDef;
			this.m_chart = chart;
		}

		public override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_series != null)
			{
				this.m_series.SetNewContext();
			}
			if (this.m_chartFormulaParameters != null)
			{
				this.m_chartFormulaParameters.SetNewContext();
			}
		}
	}
}

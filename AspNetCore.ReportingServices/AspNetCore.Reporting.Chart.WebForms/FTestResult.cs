namespace AspNetCore.Reporting.Chart.WebForms
{
	public class FTestResult
	{
		public double firstSeriesMean;

		public double secondSeriesMean;

		public double firstSeriesVariance;

		public double secondSeriesVariance;

		public double fValue;

		public double probabilityFOneTail;

		public double fCriticalValueOneTail;

		public double FirstSeriesMean
		{
			get
			{
				return this.firstSeriesMean;
			}
		}

		public double SecondSeriesMean
		{
			get
			{
				return this.secondSeriesMean;
			}
		}

		public double FirstSeriesVariance
		{
			get
			{
				return this.firstSeriesVariance;
			}
		}

		public double SecondSeriesVariance
		{
			get
			{
				return this.secondSeriesVariance;
			}
		}

		public double FValue
		{
			get
			{
				return this.fValue;
			}
		}

		public double ProbabilityFOneTail
		{
			get
			{
				return this.probabilityFOneTail;
			}
		}

		public double FCriticalValueOneTail
		{
			get
			{
				return this.fCriticalValueOneTail;
			}
		}
	}
}

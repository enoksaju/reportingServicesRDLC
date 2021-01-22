namespace AspNetCore.Reporting.Chart.WebForms
{
	public class ZTestResult
	{
		public double firstSeriesMean;

		public double secondSeriesMean;

		public double firstSeriesVariance;

		public double secondSeriesVariance;

		public double zValue;

		public double probabilityZOneTail;

		public double zCriticalValueOneTail;

		public double probabilityZTwoTail;

		public double zCriticalValueTwoTail;

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

		public double ZValue
		{
			get
			{
				return this.zValue;
			}
		}

		public double ProbabilityZOneTail
		{
			get
			{
				return this.probabilityZOneTail;
			}
		}

		public double ZCriticalValueOneTail
		{
			get
			{
				return this.zCriticalValueOneTail;
			}
		}

		public double ProbabilityZTwoTail
		{
			get
			{
				return this.probabilityZTwoTail;
			}
		}

		public double ZCriticalValueTwoTail
		{
			get
			{
				return this.zCriticalValueTwoTail;
			}
		}
	}
}

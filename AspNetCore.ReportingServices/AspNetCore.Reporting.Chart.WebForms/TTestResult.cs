namespace AspNetCore.Reporting.Chart.WebForms
{
	public class TTestResult
	{
		public double firstSeriesMean;

		public double secondSeriesMean;

		public double firstSeriesVariance;

		public double secondSeriesVariance;

		public double tValue;

		public double degreeOfFreedom;

		public double probabilityTOneTail;

		public double tCriticalValueOneTail;

		public double probabilityTTwoTail;

		public double tCriticalValueTwoTail;

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

		public double TValue
		{
			get
			{
				return this.tValue;
			}
		}

		public double DegreeOfFreedom
		{
			get
			{
				return this.degreeOfFreedom;
			}
		}

		public double ProbabilityTOneTail
		{
			get
			{
				return this.probabilityTOneTail;
			}
		}

		public double TCriticalValueOneTail
		{
			get
			{
				return this.tCriticalValueOneTail;
			}
		}

		public double ProbabilityTTwoTail
		{
			get
			{
				return this.probabilityTTwoTail;
			}
		}

		public double TCriticalValueTwoTail
		{
			get
			{
				return this.tCriticalValueTwoTail;
			}
		}
	}
}

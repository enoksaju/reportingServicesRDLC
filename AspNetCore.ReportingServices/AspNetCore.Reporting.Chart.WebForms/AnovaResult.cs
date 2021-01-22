namespace AspNetCore.Reporting.Chart.WebForms
{
	public class AnovaResult
	{
		public double sumOfSquaresBetweenGroups;

		public double sumOfSquaresWithinGroups;

		public double sumOfSquaresTotal;

		public double degreeOfFreedomBetweenGroups;

		public double degreeOfFreedomWithinGroups;

		public double degreeOfFreedomTotal;

		public double meanSquareVarianceBetweenGroups;

		public double meanSquareVarianceWithinGroups;

		public double fRatio;

		public double fCriticalValue;

		public double SumOfSquaresBetweenGroups
		{
			get
			{
				return this.sumOfSquaresBetweenGroups;
			}
		}

		public double SumOfSquaresWithinGroups
		{
			get
			{
				return this.sumOfSquaresWithinGroups;
			}
		}

		public double SumOfSquaresTotal
		{
			get
			{
				return this.sumOfSquaresTotal;
			}
		}

		public double DegreeOfFreedomBetweenGroups
		{
			get
			{
				return this.degreeOfFreedomBetweenGroups;
			}
		}

		public double DegreeOfFreedomWithinGroups
		{
			get
			{
				return this.degreeOfFreedomWithinGroups;
			}
		}

		public double DegreeOfFreedomTotal
		{
			get
			{
				return this.degreeOfFreedomTotal;
			}
		}

		public double MeanSquareVarianceBetweenGroups
		{
			get
			{
				return this.meanSquareVarianceBetweenGroups;
			}
		}

		public double MeanSquareVarianceWithinGroups
		{
			get
			{
				return this.meanSquareVarianceWithinGroups;
			}
		}

		public double FRatio
		{
			get
			{
				return this.fRatio;
			}
		}

		public double FCriticalValue
		{
			get
			{
				return this.fCriticalValue;
			}
		}
	}
}

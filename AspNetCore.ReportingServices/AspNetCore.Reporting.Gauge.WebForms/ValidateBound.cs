using System;

namespace AspNetCore.Reporting.Gauge.WebForms
{
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class ValidateBound : Attribute
	{
		private double minimum;

		private double maximum;

		private bool required = true;

		public double Minimum
		{
			get
			{
				return this.minimum;
			}
		}

		public double Maximum
		{
			get
			{
				return this.maximum;
			}
		}

		public bool Required
		{
			get
			{
				return this.required;
			}
		}

		public ValidateBound(double minimum, double maximum)
		{
			this.minimum = minimum;
			this.maximum = maximum;
		}

		public ValidateBound(double minimum, double maximum, bool required)
		{
			this.minimum = minimum;
			this.maximum = maximum;
			this.required = required;
		}
	}
}

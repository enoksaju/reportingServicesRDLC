using System;

namespace AspNetCore.Reporting.Gauge.WebForms
{
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class DoubleConverterHint : Attribute
	{
		private double bound;

		public double Bound
		{
			get
			{
				return this.bound;
			}
		}

		public DoubleConverterHint(double bound)
		{
			this.bound = bound;
		}
	}
}

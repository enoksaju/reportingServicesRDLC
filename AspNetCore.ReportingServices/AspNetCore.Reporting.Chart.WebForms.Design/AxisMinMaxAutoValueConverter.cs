using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace AspNetCore.Reporting.Chart.WebForms.Design
{
	public class AxisMinMaxAutoValueConverter : AxisMinMaxValueConverter
	{
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(double.NaN);
			return new StandardValuesCollection(arrayList);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			double d = (double)value;
			if (destinationType == typeof(string) && double.IsNaN(d))
			{
				return "Auto";
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string strA = (string)value;
				if (string.Compare(strA, "Auto", StringComparison.OrdinalIgnoreCase) == 0)
				{
					return double.NaN;
				}
			}
			return base.ConvertFrom(context, culture, value);
		}
	}
}

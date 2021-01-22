using System;
using System.ComponentModel;
using System.Globalization;

namespace AspNetCore.Reporting.Chart.WebForms.Design
{
	public class AxesArrayConverter : TypeConverter
	{
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				return "(Collection)";
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}

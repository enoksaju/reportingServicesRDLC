using System;
using System.ComponentModel;
using System.Globalization;

namespace AspNetCore.Reporting.Map.WebForms
{
	public class NoNameExpandableObjectConverter : ExpandableObjectConverter
	{
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (context != null && context.Instance != null && destinationType == typeof(string))
			{
				return "";
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}

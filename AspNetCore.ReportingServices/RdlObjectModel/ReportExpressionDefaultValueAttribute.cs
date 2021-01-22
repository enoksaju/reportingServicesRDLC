using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class ReportExpressionDefaultValueAttribute : DefaultValueAttribute
	{
		public ReportExpressionDefaultValueAttribute()
			: base(default(ReportExpression))
		{
		}

		public ReportExpressionDefaultValueAttribute(string value)
			: base(new ReportExpression(value))
		{
		}

		public ReportExpressionDefaultValueAttribute(Type type)
			: base(Activator.CreateInstance(ReportExpressionDefaultValueAttribute.ConstructGenericType(type)))
		{
		}

		public ReportExpressionDefaultValueAttribute(Type type, object value)
			: base(ReportExpressionDefaultValueAttribute.CreateInstance(type, value))
		{
		}

		public static Type ConstructGenericType(Type type)
		{
			return typeof(ReportExpression<>).MakeGenericType(type);
		}

		public static object CreateInstance(Type type, object value)
		{
			type = ReportExpressionDefaultValueAttribute.ConstructGenericType(type);
			if (value is string)
			{
				ConstructorInfo constructor = type.GetConstructor(new Type[2]
				{
					typeof(string),
					typeof(IFormatProvider)
				});
				return constructor.Invoke(new object[2]
				{
					value,
					CultureInfo.InvariantCulture
				});
			}
			return Activator.CreateInstance(type, value);
		}
	}
}

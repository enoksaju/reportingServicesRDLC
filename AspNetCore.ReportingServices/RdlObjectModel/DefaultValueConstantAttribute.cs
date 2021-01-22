using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public sealed class DefaultValueConstantAttribute : DefaultValueAttribute
	{
		public DefaultValueConstantAttribute(string field)
			: base(DefaultValueConstantAttribute.GetConstant(field))
		{
		}

		public static object GetConstant(string field)
		{
			return typeof(Constants).InvokeMember(field, BindingFlags.GetField, null, null, null, CultureInfo.InvariantCulture);
		}
	}
}

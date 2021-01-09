using System.ComponentModel;

namespace AspNetCore.Reporting.Gauge.WebForms
{
	internal class DataMemberConverter : StringConverter
	{
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}
	}
}

using System.Collections;
using System.Globalization;

namespace AspNetCore.Reporting.Gauge.WebForms
{
	public static class SmartClientSerializerHelper
	{
		private static CaseInsensitiveHashCodeProvider hashCodeProvider = new CaseInsensitiveHashCodeProvider(CultureInfo.InvariantCulture);
	}
}

using System.Collections.Generic;
using System.Security.Principal;

namespace AspNetCore.ReportingServices.Interfaces
{
	public interface IRSRequestContext
	{
		IDictionary<string, string> Cookies
		{
			get;
		}

		IDictionary<string, string[]> Headers
		{
			get;
		}

		IIdentity User
		{
			get;
		}
	}
}

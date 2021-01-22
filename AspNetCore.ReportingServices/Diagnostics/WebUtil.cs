using System;
using System.Net;

namespace AspNetCore.ReportingServices.Diagnostics
{
	public static class WebUtil
	{
		public const string NormalizedLocalServerName = "localhost";

		public static bool IsWellKnownLocalServer(string server)
		{
			if (string.Compare(server, "localhost", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			if (string.Compare(server, "(local)", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			if (string.Compare(server, ".", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			if (string.Compare(server, Environment.MachineName, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			if (string.Compare(server, "127.0.0.1", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			if (string.Compare(server, "::1", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			if (string.Compare(server, "[::1]", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			return false;
		}

		public static bool IsSameServer(IPAddress[] server1IPAddresses, IPAddress[] server2IPAddresses)
		{
			foreach (IPAddress iPAddress in server1IPAddresses)
			{
				foreach (IPAddress obj in server2IPAddresses)
				{
					if (iPAddress.Equals(obj))
					{
						return true;
					}
				}
			}
			return false;
		}

		public static string NormalizeWellKnownLocalServerName(string server)
		{
			if (!WebUtil.IsWellKnownLocalServer(server))
			{
				return server;
			}
			return "localhost";
		}
	}
}

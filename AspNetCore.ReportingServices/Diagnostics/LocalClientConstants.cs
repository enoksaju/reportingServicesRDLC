namespace AspNetCore.ReportingServices.Diagnostics
{
	public static class LocalClientConstants
	{
		private static readonly string m_clientNotLocalHeaderName = "RSClientNotLocalHeader";

		public static string ClientNotLocalHeaderName
		{
			get
			{
				return LocalClientConstants.m_clientNotLocalHeaderName;
			}
		}
	}
}

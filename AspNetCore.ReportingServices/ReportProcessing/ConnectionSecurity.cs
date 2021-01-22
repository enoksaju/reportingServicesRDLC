namespace AspNetCore.ReportingServices.ReportProcessing
{
	public enum ConnectionSecurity
	{
		UseIntegratedSecurity,
		ImpersonateWindowsUser,
		UseDataSourceCredentials,
		None,
		ImpersonateServiceAccount
	}
}

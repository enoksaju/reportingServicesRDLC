namespace AspNetCore.ReportingServices.Diagnostics
{
	public interface IDataProtection
	{
		byte[] ProtectData(string unprotectedData, string tag);

		string UnprotectDataToString(byte[] protectedData, string tag);
	}
}

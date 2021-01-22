namespace AspNetCore.ReportingServices.DataProcessing
{
	public interface IModelDataExtension
	{
		string GetModelMetadata(string perspectiveName, string supportedVersion);

		void CancelModelMetadataRetrieval();
	}
}

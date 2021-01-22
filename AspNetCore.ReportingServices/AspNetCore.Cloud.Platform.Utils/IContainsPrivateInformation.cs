namespace Microsoft.Cloud.Platform.Utils
{
	public interface IContainsPrivateInformation
	{
		string ToPrivateString();

		string ToInternalString();

		string ToOriginalString();
	}
}

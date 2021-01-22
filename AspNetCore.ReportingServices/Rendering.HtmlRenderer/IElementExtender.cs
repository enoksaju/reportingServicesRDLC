namespace AspNetCore.ReportingServices.Rendering.HtmlRenderer
{
	public interface IElementExtender
	{
		bool HasSetupRequirements();

		string SetupRequirements();

		bool ShouldApplyToElement(bool isTopLevel);

		string ApplyToElement();
	}
}

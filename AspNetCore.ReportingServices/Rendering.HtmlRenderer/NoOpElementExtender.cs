using System;

namespace AspNetCore.ReportingServices.Rendering.HtmlRenderer
{
	public sealed class NoOpElementExtender : IElementExtender
	{
		public bool HasSetupRequirements()
		{
			return false;
		}

		public string SetupRequirements()
		{
			throw new InvalidOperationException();
		}

		public bool ShouldApplyToElement(bool isTopLevel)
		{
			return false;
		}

		public string ApplyToElement()
		{
			throw new InvalidOperationException();
		}
	}
}

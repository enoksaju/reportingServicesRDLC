using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class RichTextHelpers
	{
		public static MarkupType TranslateMarkupType(string value)
		{
			if (AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.CompareWithInvariantCulture(value, "None", false) == 0)
			{
				return MarkupType.None;
			}
			if (AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.CompareWithInvariantCulture(value, "HTML", false) == 0)
			{
				return MarkupType.HTML;
			}
			return MarkupType.None;
		}

		public static ListStyle TranslateListStyle(string value)
		{
			if (AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.CompareWithInvariantCulture(value, "None", false) == 0)
			{
				return ListStyle.None;
			}
			if (AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.CompareWithInvariantCulture(value, "Numbered", false) == 0)
			{
				return ListStyle.Numbered;
			}
			if (AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.CompareWithInvariantCulture(value, "Bulleted", false) == 0)
			{
				return ListStyle.Bulleted;
			}
			return ListStyle.None;
		}
	}
}

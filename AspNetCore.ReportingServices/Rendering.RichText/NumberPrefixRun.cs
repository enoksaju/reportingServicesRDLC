namespace AspNetCore.ReportingServices.Rendering.RichText
{
	public class NumberPrefixRun : PrefixRun
	{
		private const string PrefixNumberFontFamily = "Arial";

		public override string FontName
		{
			get
			{
				return "Arial";
			}
		}
	}
}

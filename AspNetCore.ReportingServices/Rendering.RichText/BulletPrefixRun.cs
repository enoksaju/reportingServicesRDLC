namespace AspNetCore.ReportingServices.Rendering.RichText
{
	public class BulletPrefixRun : PrefixRun
	{
		private const string PrefixBulletFontFamily = "Courier New";

		public override string FontName
		{
			get
			{
				return "Courier New";
			}
		}
	}
}

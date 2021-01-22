namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class ColorProperty : PropertyDefinition<ReportColor>
	{
		public ColorProperty(string name, ReportColor? defaultValue)
			: base(name, defaultValue)
		{
		}
	}
}

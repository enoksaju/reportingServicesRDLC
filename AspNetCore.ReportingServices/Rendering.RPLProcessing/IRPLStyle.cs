namespace AspNetCore.ReportingServices.Rendering.RPLProcessing
{
	public interface IRPLStyle
	{
		object this[byte styleName]
		{
			get;
		}
	}
}

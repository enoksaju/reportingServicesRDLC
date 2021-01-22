namespace AspNetCore.ReportingServices.Rendering.ExcelOpenXmlRenderer
{
	public interface IDeepCloneable<T>
	{
		T DeepClone();
	}
}

namespace AspNetCore.ReportingServices.Rendering.ExcelOpenXmlRenderer.Model
{
	public interface IShapeModel
	{
		string Hyperlink
		{
			set;
		}

		void UpdateColumnOffset(double sizeInPoints, bool start);

		void UpdateRowOffset(double sizeInPoints, bool start);
	}
}

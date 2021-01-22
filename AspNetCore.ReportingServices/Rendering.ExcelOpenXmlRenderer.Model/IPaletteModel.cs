namespace AspNetCore.ReportingServices.Rendering.ExcelOpenXmlRenderer.Model
{
	public interface IPaletteModel
	{
		Palette Interface
		{
			get;
		}

		ColorModel getColor(int red, int green, int blue);

		void setColorAt(int index, int red, int green, int blue);

		int GetColorIndex(int red, int green, int blue);

		ColorModel GetColorAt(int index);
	}
}

using System.IO;

namespace AspNetCore.ReportingServices.Rendering.ExcelOpenXmlRenderer.Model
{
	public interface IPictureShapesModel
	{
		Pictures Interface
		{
			get;
		}

		IPictureShapeModel CreatePicture(string uniqueId, string extension, Stream pictureStream, AnchorModel startPosition, AnchorModel endPosition);

		new string ToString();
	}
}

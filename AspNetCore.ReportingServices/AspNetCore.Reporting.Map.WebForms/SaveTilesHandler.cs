using System.Drawing;

namespace AspNetCore.Reporting.Map.WebForms
{
	public delegate void SaveTilesHandler(Layer layer, string[,] tileUrls, Image[,] tileImages);
}

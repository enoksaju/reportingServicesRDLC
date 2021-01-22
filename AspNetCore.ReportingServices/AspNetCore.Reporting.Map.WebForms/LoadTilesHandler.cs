using System.Drawing;

namespace AspNetCore.Reporting.Map.WebForms
{
	public delegate Image[,] LoadTilesHandler(Layer layer, string[,] tileUrls);
}

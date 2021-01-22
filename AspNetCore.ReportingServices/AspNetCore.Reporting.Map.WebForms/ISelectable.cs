using System.Drawing;

namespace AspNetCore.Reporting.Map.WebForms
{
	public interface ISelectable
	{
		void DrawSelection(MapGraphics g, RectangleF clipRect, bool designTimeSelection);

		bool IsSelected();

		bool IsVisible();

		RectangleF GetSelectionRectangle(MapGraphics g, RectangleF clipRect);
	}
}

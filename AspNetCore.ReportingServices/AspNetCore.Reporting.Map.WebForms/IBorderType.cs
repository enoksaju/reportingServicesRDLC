using System.Drawing;

namespace AspNetCore.Reporting.Map.WebForms
{
	public interface IBorderType
	{
		string Name
		{
			get;
		}

		void DrawBorder(MapGraphics graph, Frame borderSkin, RectangleF rect, Color backColor, MapHatchStyle backHatchStyle, string backImage, MapImageWrapMode backImageMode, Color backImageTranspColor, MapImageAlign backImageAlign, GradientType backGradientType, Color backSecondaryColor, Color borderColor, int borderWidth, MapDashStyle borderStyle);

		void AdjustAreasPosition(MapGraphics graph, ref RectangleF areasRect);

		RectangleF GetTitlePositionInBorder();

		bool IsVisible(MapGraphics g);
	}
}

using AspNetCore.Reporting.Chart.WebForms.Utilities;
using System.ComponentModel;
using System.Drawing;

namespace AspNetCore.Reporting.Chart.WebForms
{
	[SRDescription("DescriptionAttributeVerticalLineAnnotation_VerticalLineAnnotation")]
	public class VerticalLineAnnotation : LineAnnotation
	{
		[SRCategory("CategoryAttributeMisc")]
		[Bindable(true)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SerializationVisibility(SerializationVisibility.Hidden)]
		[SRDescription("DescriptionAttributeAnnotationType4")]
		public override string AnnotationType
		{
			get
			{
				return "VerticalLine";
			}
		}

		public override void AdjustLineCoordinates(ref PointF point1, ref PointF point2, ref RectangleF selectionRect)
		{
			point2.X = point1.X;
			selectionRect.Width = 0f;
			base.AdjustLineCoordinates(ref point1, ref point2, ref selectionRect);
		}

		public override RectangleF GetContentPosition()
		{
			return new RectangleF(float.NaN, float.NaN, 0f, float.NaN);
		}
	}
}

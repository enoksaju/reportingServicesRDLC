using AspNetCore.Reporting.Map.WebForms;
using System.Drawing;
using System.Globalization;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public class CoreShapeManager : CoreSpatialElementManager
	{
		public override AspNetCore.Reporting.Map.WebForms.FieldCollection FieldDefinitions
		{
			get
			{
				return base.m_coreMap.ShapeFields;
			}
		}

		protected override NamedCollection SpatialElements
		{
			get
			{
				return base.m_coreMap.Shapes;
			}
		}

		public CoreShapeManager(MapControl mapControl, MapVectorLayer mapVectorLayer)
			: base(mapControl, mapVectorLayer)
		{
		}

		public override void AddSpatialElement(ISpatialElement spatialElement)
		{
			((NamedElement)spatialElement).Name = base.m_coreMap.Shapes.Count.ToString(CultureInfo.InvariantCulture);
			base.m_coreMap.Shapes.Add((Shape)spatialElement);
		}

		public override void RemoveSpatialElement(ISpatialElement spatialElement)
		{
			base.m_coreMap.Shapes.Remove((Shape)spatialElement);
		}

		public override ISpatialElement CreateSpatialElement()
		{
			Shape shape = new Shape();
			shape.BorderColor = Color.Black;
			shape.Text = "";
			return shape;
		}
	}
}

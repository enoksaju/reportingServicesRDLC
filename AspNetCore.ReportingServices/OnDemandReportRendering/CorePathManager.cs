using AspNetCore.Reporting.Map.WebForms;
using System.Drawing;
using System.Globalization;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public class CorePathManager : CoreSpatialElementManager
	{
		protected override NamedCollection SpatialElements
		{
			get
			{
				return base.m_coreMap.Paths;
			}
		}

		public override AspNetCore.Reporting.Map.WebForms.FieldCollection FieldDefinitions
		{
			get
			{
				return base.m_coreMap.PathFields;
			}
		}

		public CorePathManager(MapControl mapControl, MapVectorLayer mapVectorLayer)
			: base(mapControl, mapVectorLayer)
		{
		}

		public override void AddSpatialElement(ISpatialElement spatialElement)
		{
			((NamedElement)spatialElement).Name = base.m_coreMap.Paths.Count.ToString(CultureInfo.InvariantCulture);
			base.m_coreMap.Paths.Add((Path)spatialElement);
		}

		public override void RemoveSpatialElement(ISpatialElement spatialElement)
		{
			base.m_coreMap.Paths.Remove((Path)spatialElement);
		}

		public override ISpatialElement CreateSpatialElement()
		{
			Path path = new Path();
			path.BorderColor = Color.Black;
			path.Text = "";
			return new Path();
		}
	}
}

using AspNetCore.Reporting.Map.WebForms;
using System.Drawing;
using System.Globalization;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public class CoreSymbolManager : CoreSpatialElementManager
	{
		protected override NamedCollection SpatialElements
		{
			get
			{
				return base.m_coreMap.Symbols;
			}
		}

		public override AspNetCore.Reporting.Map.WebForms.FieldCollection FieldDefinitions
		{
			get
			{
				return base.m_coreMap.SymbolFields;
			}
		}

		public CoreSymbolManager(MapControl mapControl, MapVectorLayer mapVectorLayer)
			: base(mapControl, mapVectorLayer)
		{
		}

		public override void AddSpatialElement(ISpatialElement spatialElement)
		{
			((NamedElement)spatialElement).Name = base.m_coreMap.Symbols.Count.ToString(CultureInfo.InvariantCulture);
			base.m_coreMap.Symbols.Add((Symbol)spatialElement);
		}

		public override void RemoveSpatialElement(ISpatialElement spatialElement)
		{
			base.m_coreMap.Symbols.Remove((Symbol)spatialElement);
		}

		public override ISpatialElement CreateSpatialElement()
		{
			Symbol symbol = new Symbol();
			symbol.BorderColor = Color.Black;
			symbol.Text = "";
			return symbol;
		}
	}
}

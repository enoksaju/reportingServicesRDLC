using AspNetCore.ReportingServices.RdlObjectModel.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	[XmlElementClass("MapCustomView", typeof(MapCustomView))]
	[XmlElementClass("MapDataBoundView", typeof(MapDataBoundView))]
	[XmlElementClass("MapElementView", typeof(MapElementView))]
	public abstract class MapView : ReportObject
	{
		public class Definition : DefinitionStore<MapView, Definition.Properties>
		{
			public enum Properties
			{
				Zoom
			}

			private Definition()
			{
			}
		}

		public ReportExpression<double> Zoom
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		public MapView()
		{
		}

		public MapView(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
		}
	}
}

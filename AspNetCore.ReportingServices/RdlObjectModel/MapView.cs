using AspNetCore.ReportingServices.RdlObjectModel.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	[XmlElementClass("MapCustomView", typeof(MapCustomView))]
	[XmlElementClass("MapDataBoundView", typeof(MapDataBoundView))]
	[XmlElementClass("MapElementView", typeof(MapElementView))]
	internal abstract class MapView : ReportObject
	{
		internal class Definition : DefinitionStore<MapView, Definition.Properties>
		{
			internal enum Properties
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

		internal MapView(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
		}
	}
}

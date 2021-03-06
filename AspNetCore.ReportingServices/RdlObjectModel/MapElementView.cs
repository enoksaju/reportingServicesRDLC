using System.Collections.Generic;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class MapElementView : MapView
	{
		public new class Definition : DefinitionStore<MapElementView, Definition.Properties>
		{
			public enum Properties
			{
				Zoom,
				LayerName,
				MapBindingFieldPairs,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		public ReportExpression LayerName
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		[XmlElement(typeof(RdlCollection<MapBindingFieldPair>))]
		public IList<MapBindingFieldPair> MapBindingFieldPairs
		{
			get
			{
				return (IList<MapBindingFieldPair>)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		public MapElementView()
		{
		}

		public MapElementView(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.MapBindingFieldPairs = new RdlCollection<MapBindingFieldPair>();
		}
	}
}

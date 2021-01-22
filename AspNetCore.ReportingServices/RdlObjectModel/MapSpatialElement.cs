using System.Collections.Generic;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public abstract class MapSpatialElement : ReportObject
	{
		public class Definition : DefinitionStore<MapSpatialElement, Definition.Properties>
		{
			public enum Properties
			{
				VectorData,
				MapFields
			}

			private Definition()
			{
			}
		}

		public VectorData VectorData
		{
			get
			{
				return base.PropertyStore.GetObject<VectorData>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		[XmlElement(typeof(RdlCollection<MapField>))]
		public IList<MapField> MapFields
		{
			get
			{
				return (IList<MapField>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		public MapSpatialElement()
		{
		}

		public MapSpatialElement(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.MapFields = new RdlCollection<MapField>();
		}
	}
}

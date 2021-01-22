using System.Collections.Generic;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class MapShapefile : MapSpatialData
	{
		public class Definition : DefinitionStore<MapShapefile, Definition.Properties>
		{
			public enum Properties
			{
				Source,
				MapFieldNames,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		public ReportExpression Source
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		[XmlElement(typeof(RdlCollection<ReportExpression>))]
		[XmlArrayItem("MapFieldName", typeof(ReportExpression))]
		public IList<ReportExpression> MapFieldNames
		{
			get
			{
				return (IList<ReportExpression>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		public MapShapefile()
		{
		}

		public MapShapefile(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.MapFieldNames = new RdlCollection<ReportExpression>();
		}
	}
}

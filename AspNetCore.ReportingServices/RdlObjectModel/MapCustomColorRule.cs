using System.Collections.Generic;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class MapCustomColorRule : MapColorRule
	{
		public new class Definition : DefinitionStore<MapCustomColorRule, Definition.Properties>
		{
			public enum Properties
			{
				DataValue,
				DistributionType,
				BucketCount,
				StartValue,
				EndValue,
				MapBuckets,
				LegendName,
				LegendText,
				DataElementName,
				DataElementOutput,
				ShowInColorScale,
				MapCustomColors,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		[XmlElement(typeof(RdlCollection<ReportExpression<ReportColor>>))]
		[XmlArrayItem("MapCustomColor", typeof(ReportExpression<ReportColor>))]
		public IList<ReportExpression<ReportColor>> MapCustomColors
		{
			get
			{
				return (IList<ReportExpression<ReportColor>>)base.PropertyStore.GetObject(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		public MapCustomColorRule()
		{
		}

		public MapCustomColorRule(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.MapCustomColors = new RdlCollection<ReportExpression<ReportColor>>();
		}
	}
}

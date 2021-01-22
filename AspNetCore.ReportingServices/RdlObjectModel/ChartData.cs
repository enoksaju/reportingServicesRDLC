using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class ChartData : DataRegionBody
	{
		public class Definition : DefinitionStore<ChartData, Definition.Properties>
		{
			public enum Properties
			{
				ChartSeriesCollection,
				ChartDerivedSeriesCollection,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		[XmlElement(typeof(RdlCollection<ChartSeries>))]
		public IList<ChartSeries> ChartSeriesCollection
		{
			get
			{
				return (IList<ChartSeries>)base.PropertyStore.GetObject(0);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.PropertyStore.SetObject(0, value);
			}
		}

		[XmlElement(typeof(RdlCollection<ChartDerivedSeries>))]
		public IList<ChartDerivedSeries> ChartDerivedSeriesCollection
		{
			get
			{
				return (IList<ChartDerivedSeries>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		public ChartData()
		{
		}

		public ChartData(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.ChartSeriesCollection = new RdlCollection<ChartSeries>();
			this.ChartDerivedSeriesCollection = new RdlCollection<ChartDerivedSeries>();
		}
	}
}

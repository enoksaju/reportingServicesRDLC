using System.Collections.Generic;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class GridLayoutDefinition : ReportObject
	{
		public class Definition : DefinitionStore<GridLayoutDefinition, Definition.Properties>
		{
			public enum Properties
			{
				NumberOfColumns,
				NumberOfRows,
				CellDefinitions
			}

			private Definition()
			{
			}
		}

		public const int MaxNumberOfRows = 10000;

		public const int MaxNumberOfColumns = 8;

		public const int MaxNumberOfConsecutiveEmptyRows = 20;

		public int NumberOfColumns
		{
			get
			{
				return base.PropertyStore.GetInteger(0);
			}
			set
			{
				base.PropertyStore.SetInteger(0, value);
			}
		}

		public int NumberOfRows
		{
			get
			{
				return base.PropertyStore.GetInteger(1);
			}
			set
			{
				base.PropertyStore.SetInteger(1, value);
			}
		}

		[XmlElement(typeof(RdlCollection<CellDefinition>))]
		public IList<CellDefinition> CellDefinitions
		{
			get
			{
				return base.PropertyStore.GetObject<RdlCollection<CellDefinition>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		public GridLayoutDefinition()
		{
			this.CellDefinitions = new RdlCollection<CellDefinition>();
			this.NumberOfColumns = 4;
			this.NumberOfRows = 2;
		}

		public GridLayoutDefinition(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}

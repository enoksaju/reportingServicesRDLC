using System.Collections.Generic;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	internal class DataSet : DataSetBase
	{
		internal new class Definition : DefinitionStore<DataSet, Definition.Properties>
		{
			internal enum Properties
			{
				Name,
				CaseSensitivity,
				Collation,
				AccentSensitivity,
				KanatypeSensitivity,
				WidthSensitivity,
				InterpretSubtotalsAsDetails,
				Query,
				Fields,
				Filters,
				SharedDataSet
			}
		}

		public Query Query
		{
			get
			{
				return (Query)base.PropertyStore.GetObject(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
				if (value != null)
				{
					base.PropertyStore.SetObject(10, null);
				}
			}
		}

		public SharedDataSet SharedDataSet
		{
			get
			{
				return (SharedDataSet)base.PropertyStore.GetObject(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
				if (value != null)
				{
					base.PropertyStore.SetObject(7, null);
				}
			}
		}

		[XmlElement(typeof(RdlCollection<Field>))]
		public IList<Field> Fields
		{
			get
			{
				return (IList<Field>)base.PropertyStore.GetObject(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		[XmlElement(typeof(RdlCollection<Filter>))]
		public IList<Filter> Filters
		{
			get
			{
				return (IList<Filter>)base.PropertyStore.GetObject(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		public DataSet()
		{
		}

		internal DataSet(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.Fields = new RdlCollection<Field>();
			this.Filters = new RdlCollection<Filter>();
		}

		private bool FieldExistsByName(IList<Field> list, string name)
		{
			foreach (Field item in list)
			{
				if (item.Name == name)
				{
					return true;
				}
			}
			return false;
		}

		public bool IsSharedDataSourceReference()
		{
			if (this.SharedDataSet != null)
			{
				return true;
			}
			DataSource dataSource = this.Query.DataSource;
			if (dataSource != null)
			{
				return !string.IsNullOrEmpty(dataSource.DataSourceReference);
			}
			return false;
		}

		public override QueryBase GetQuery()
		{
			return this.Query;
		}

		public IList<QueryParameter> GetQueryParameters()
		{
			if (this.Query != null)
			{
				return this.Query.QueryParameters;
			}
			if (this.SharedDataSet != null)
			{
				return this.SharedDataSet.QueryParameters;
			}
			return null;
		}
	}
}

using System.Collections.Generic;
using System.ComponentModel;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class DataSetReference : ReportObject
	{
		public class Definition : DefinitionStore<DataSetReference, Definition.Properties>
		{
			public enum Properties
			{
				DataSetName,
				ValueField,
				LabelField
			}

			private Definition()
			{
			}
		}

		public string DataSetName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		public string ValueField
		{
			get
			{
				return (string)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		[DefaultValue("")]
		public string LabelField
		{
			get
			{
				return (string)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		public DataSetReference()
		{
		}

		public DataSetReference(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		protected override void GetDependenciesCore(IList<ReportObject> dependencies)
		{
			base.GetDependenciesCore(dependencies);
			Report ancestor = base.GetAncestor<Report>();
			if (ancestor != null)
			{
				DataSet dataSetByName = ancestor.GetDataSetByName(this.DataSetName);
				if (dataSetByName != null && !dependencies.Contains(dataSetByName))
				{
					dependencies.Add(dataSetByName);
				}
			}
		}
	}
}

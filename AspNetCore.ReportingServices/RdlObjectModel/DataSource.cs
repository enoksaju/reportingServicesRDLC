using System.ComponentModel;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class DataSource : ReportObject, INamedObject
	{
		public class Definition : DefinitionStore<DataSource, Definition.Properties>
		{
			public enum Properties
			{
				Name,
				Transaction,
				ConnectionProperties,
				DataSourceReference
			}
		}

		[XmlAttribute]
		public string Name
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

		[DefaultValue(false)]
		public bool Transaction
		{
			get
			{
				return base.PropertyStore.GetBoolean(1);
			}
			set
			{
				base.PropertyStore.SetBoolean(1, value);
			}
		}

		public ConnectionProperties ConnectionProperties
		{
			get
			{
				return (ConnectionProperties)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		[DefaultValue("")]
		public string DataSourceReference
		{
			get
			{
				return (string)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		public DataSource()
		{
		}

		public DataSource(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
		}
	}
}

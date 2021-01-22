namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class Class : ReportObject
	{
		public class Definition : DefinitionStore<Class, Definition.Properties>
		{
			public enum Properties
			{
				ClassName,
				InstanceName
			}

			private Definition()
			{
			}
		}

		public string ClassName
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

		public string InstanceName
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

		public Class()
		{
		}

		public Class(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}

using AspNetCore.ReportingServices.RdlObjectModel.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class ReportParametersLayout : ReportObject, IShouldSerialize
	{
		public class Definition : DefinitionStore<GridLayoutDefinition, Definition.Properties>
		{
			public enum Properties
			{
				GridLayoutDefinition
			}

			private Definition()
			{
			}
		}

		public GridLayoutDefinition GridLayoutDefinition
		{
			get
			{
				return base.PropertyStore.GetObject<GridLayoutDefinition>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		public ReportParametersLayout()
		{
			this.GridLayoutDefinition = new GridLayoutDefinition();
		}

		public ReportParametersLayout(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		bool IShouldSerialize.ShouldSerializeThis()
		{
			return true;
		}

		SerializationMethod IShouldSerialize.ShouldSerializeProperty(string name)
		{
			return SerializationMethod.Auto;
		}
	}
}

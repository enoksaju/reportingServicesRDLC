namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class MapBindingFieldPair : ReportObject
	{
		public class Definition : DefinitionStore<MapBindingFieldPair, Definition.Properties>
		{
			public enum Properties
			{
				FieldName,
				BindingExpression,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		public ReportExpression FieldName
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

		public ReportExpression BindingExpression
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		public MapBindingFieldPair()
		{
		}

		public MapBindingFieldPair(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
		}
	}
}

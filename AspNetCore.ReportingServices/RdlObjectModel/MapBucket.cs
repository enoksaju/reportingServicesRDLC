namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class MapBucket : ReportObject
	{
		public class Definition : DefinitionStore<MapBucket, Definition.Properties>
		{
			public enum Properties
			{
				StartValue,
				EndValue,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		public ReportExpression StartValue
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

		public ReportExpression EndValue
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

		public MapBucket()
		{
		}

		public MapBucket(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
		}
	}
}

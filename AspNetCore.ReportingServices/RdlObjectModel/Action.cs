namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class Action : ReportObject
	{
		public class Definition : DefinitionStore<Action, Definition.Properties>
		{
			public enum Properties
			{
				Hyperlink,
				Drillthrough,
				BookmarkLink
			}

			private Definition()
			{
			}
		}

		[ReportExpressionDefaultValue]
		public ReportExpression Hyperlink
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

		public Drillthrough Drillthrough
		{
			get
			{
				return (Drillthrough)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		[ReportExpressionDefaultValue]
		public ReportExpression BookmarkLink
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		public Action()
		{
		}

		public Action(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}

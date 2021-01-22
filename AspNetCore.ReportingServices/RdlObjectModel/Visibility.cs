using System.ComponentModel;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class Visibility : ReportObject
	{
		public class Definition : DefinitionStore<Visibility, Definition.Properties>
		{
			public enum Properties
			{
				Hidden,
				ToggleItem
			}

			private Definition()
			{
			}
		}

		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Hidden
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		[DefaultValue("")]
		public string ToggleItem
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

		public Visibility()
		{
		}

		public Visibility(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}

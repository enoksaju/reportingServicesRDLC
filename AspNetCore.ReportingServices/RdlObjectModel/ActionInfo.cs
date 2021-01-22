using System.Collections.Generic;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class ActionInfo : ReportObject
	{
		public class Definition : DefinitionStore<ActionInfo, Definition.Properties>
		{
			public enum Properties
			{
				Actions,
				LayoutDirection,
				Style
			}

			private Definition()
			{
			}
		}

		[XmlElement(typeof(RdlCollection<Action>))]
		public IList<Action> Actions
		{
			get
			{
				return (IList<Action>)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		public ActionInfo()
		{
		}

		public ActionInfo(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.Actions = new RdlCollection<Action>();
		}
	}
}

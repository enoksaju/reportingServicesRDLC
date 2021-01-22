using AspNetCore.ReportingServices.RdlObjectModel;
using System;

namespace AspNetCore.ReportingServices.RdlObjectModel2005
{
	public class ValueAxis2005 : ReportObject
	{
		public class Definition : DefinitionStore<ValueAxis2005, Definition.Properties>
		{
			public enum Properties
			{
				Axis
			}

			private Definition()
			{
			}
		}

		public Axis2005 Axis
		{
			get
			{
				return (Axis2005)base.PropertyStore.GetObject(0);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.PropertyStore.SetObject(0, value);
			}
		}

		public ValueAxis2005()
		{
		}

		public ValueAxis2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			this.Axis = new Axis2005();
		}
	}
}

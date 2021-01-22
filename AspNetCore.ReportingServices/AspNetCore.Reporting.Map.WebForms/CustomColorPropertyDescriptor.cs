using System;
using System.ComponentModel;

namespace AspNetCore.Reporting.Map.WebForms
{
	public class CustomColorPropertyDescriptor : PropertyDescriptor
	{
		private Field field;

		public override Type ComponentType
		{
			get
			{
				return typeof(CustomColor);
			}
		}

		public override bool IsBrowsable
		{
			get
			{
				return true;
			}
		}

		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public override Type PropertyType
		{
			get
			{
				return this.field.Type;
			}
		}

		public CustomColorPropertyDescriptor(Field field, string name, Attribute[] attrs)
			: base(name, attrs)
		{
			this.field = field;
		}

		public override bool CanResetValue(object component)
		{
			return false;
		}

		public override object GetValue(object component)
		{
			CustomColor customColor = (CustomColor)component;
			if (this.Name == "FromValue")
			{
				return this.field.Parse(customColor.FromValue);
			}
			return this.field.Parse(customColor.ToValue);
		}

		public override void SetValue(object component, object value)
		{
			CustomColor customColor = (CustomColor)component;
			if (this.Name == "FromValue")
			{
				customColor.FromValue = Field.ToStringInvariant(value);
			}
			else
			{
				customColor.ToValue = Field.ToStringInvariant(value);
			}
		}

		public override void ResetValue(object component)
		{
		}

		public override bool ShouldSerializeValue(object component)
		{
			return true;
		}
	}
}

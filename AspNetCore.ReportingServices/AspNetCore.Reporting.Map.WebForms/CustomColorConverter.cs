using System;
using System.ComponentModel;

namespace AspNetCore.Reporting.Map.WebForms
{
	internal class CustomColorConverter : CollectionItemTypeConverter
	{
		public CustomColorConverter()
		{
			base.simpleType = typeof(CustomColor);
		}

		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		private Field GetField(CustomColor customColor)
		{
			if (customColor == null)
			{
				return null;
			}
			RuleBase rule = customColor.GetRule();
			if (rule == null)
			{
				return null;
			}
			return rule.GetField();
		}

		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value, false);
			Field field = this.GetField((CustomColor)value);
			PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
			for (int i = 0; i < properties.Count; i++)
			{
				if (properties[i].IsBrowsable)
				{
					if (field != null && (properties[i].Name == "FromValue" || properties[i].Name == "ToValue"))
					{
						Attribute[] array = new Attribute[properties[i].Attributes.Count];
						properties[i].Attributes.CopyTo(array, 0);
						CustomColorPropertyDescriptor value2 = new CustomColorPropertyDescriptor(field, properties[i].Name, array);
						propertyDescriptorCollection.Add(value2);
					}
					else
					{
						propertyDescriptorCollection.Add(properties[i]);
					}
				}
			}
			return propertyDescriptorCollection;
		}
	}
}

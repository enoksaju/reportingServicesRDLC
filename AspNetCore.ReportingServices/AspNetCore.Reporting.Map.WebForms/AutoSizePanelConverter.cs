using System;
using System.ComponentModel;

namespace AspNetCore.Reporting.Map.WebForms
{
	public class AutoSizePanelConverter : DockablePanelConverter
	{
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			if (context != null && value is AutoSizePanel)
			{
				AutoSizePanel autoSizePanel = (AutoSizePanel)value;
				if (autoSizePanel.AutoSize)
				{
					PropertyDescriptorCollection properties = base.GetProperties(context, value, attributes);
					PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
					{
						foreach (PropertyDescriptor item in properties)
						{
							if (item.Name == "Size")
							{
								propertyDescriptorCollection.Add(TypeDescriptor.CreateProperty(value.GetType(), item, new ReadOnlyAttribute(true)));
							}
							else
							{
								propertyDescriptorCollection.Add(item);
							}
						}
						return propertyDescriptorCollection;
					}
				}
			}
			return base.GetProperties(context, value, attributes);
		}
	}
}

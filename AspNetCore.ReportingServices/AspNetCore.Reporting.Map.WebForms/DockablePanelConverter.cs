using System;
using System.ComponentModel;

namespace AspNetCore.Reporting.Map.WebForms
{
	public class DockablePanelConverter : NoNameExpandableObjectConverter
	{
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			if (context != null)
			{
				if (value is Viewport)
				{
					Viewport viewport = (Viewport)value;
					PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value, new Attribute[1]
					{
						new BrowsableAttribute(true)
					});
					PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
					{
						foreach (PropertyDescriptor item in properties)
						{
							if (viewport.AutoSize && (item.Name == "Size" || item.Name == "Location"))
							{
								propertyDescriptorCollection.Add(TypeDescriptor.CreateProperty(value.GetType(), item, new ReadOnlyAttribute(true)));
							}
							else if (viewport.ContentSize != 0 && item.Name == "ContentAutoFitMargin")
							{
								propertyDescriptorCollection.Add(TypeDescriptor.CreateProperty(value.GetType(), item, new ReadOnlyAttribute(true)));
							}
							else if (!viewport.EnablePanning && item.Name == "OptimizeForPanning")
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
				if (value is DockablePanel && ((DockablePanel)value).Dock != 0)
				{
					PropertyDescriptorCollection properties2 = TypeDescriptor.GetProperties(value, new Attribute[1]
					{
						new BrowsableAttribute(true)
					});
					PropertyDescriptorCollection propertyDescriptorCollection2 = new PropertyDescriptorCollection(null);
					{
						foreach (PropertyDescriptor item2 in properties2)
						{
							if (item2.Name == "Location")
							{
								propertyDescriptorCollection2.Add(TypeDescriptor.CreateProperty(value.GetType(), item2, new ReadOnlyAttribute(true)));
							}
							else
							{
								propertyDescriptorCollection2.Add(item2);
							}
						}
						return propertyDescriptorCollection2;
					}
				}
			}
			return base.GetProperties(context, value, attributes);
		}
	}
}

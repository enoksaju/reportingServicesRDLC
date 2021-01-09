using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace AspNetCore.Reporting.Map.WebForms
{
	internal class FrameAttributesConverter : NoNameExpandableObjectConverter
	{
		private class NullUIEditor : UITypeEditor
		{
			public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
			{
				return UITypeEditorEditStyle.None;
			}
		}

		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			if (context != null)
			{
				Frame frame = value as Frame;
				if (frame != null && frame.ShouldRenderReadOnly())
				{
					PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value, new Attribute[1]
					{
						new BrowsableAttribute(true)
					});
					PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
					{
						foreach (PropertyDescriptor item in properties)
						{
							if (item.Name != "FrameStyle" && (item.Name != "PageColor" || frame.FrameStyle == FrameStyle.None))
							{
								if (item.Name != "BackImage")
								{
									propertyDescriptorCollection.Add(TypeDescriptor.CreateProperty(value.GetType(), item, new ReadOnlyAttribute(true)));
								}
								else
								{
									propertyDescriptorCollection.Add(TypeDescriptor.CreateProperty(value.GetType(), item, new ReadOnlyAttribute(true), new EditorAttribute(typeof(NullUIEditor), typeof(UITypeEditor))));
								}
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

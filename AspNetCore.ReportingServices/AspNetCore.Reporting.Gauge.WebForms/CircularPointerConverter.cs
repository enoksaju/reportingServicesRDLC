using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace AspNetCore.Reporting.Gauge.WebForms
{
	internal class CircularPointerConverter : CollectionItemTypeConverter
	{
		private class NullUIEditor : UITypeEditor
		{
			public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
			{
				return UITypeEditorEditStyle.None;
			}
		}

		public CircularPointerConverter()
		{
			base.simpleType = typeof(CircularPointer);
		}

		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value, false);
			CircularPointer circularPointer = (CircularPointer)value;
			PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
			for (int i = 0; i < properties.Count; i++)
			{
				if (properties[i].IsBrowsable)
				{
					if (circularPointer.CapStyle != 0 && (properties[i].Name == "CapFillGradientType" || properties[i].Name == "CapFillGradientEndColor" || properties[i].Name == "CapFillHatchStyle"))
					{
						propertyDescriptorCollection.Add(TypeDescriptor.CreateProperty(value.GetType(), properties[i], new ReadOnlyAttribute(true)));
					}
					else if (circularPointer.Type == CircularPointerType.Needle && (properties[i].Name == "MarkerLength" || properties[i].Name == "MarkerStyle" || properties[i].Name == "BarStart"))
					{
						propertyDescriptorCollection.Add(TypeDescriptor.CreateProperty(value.GetType(), properties[i], new ReadOnlyAttribute(true)));
					}
					else if (circularPointer.Type == CircularPointerType.Marker && (properties[i].Name == "BarStart" || properties[i].Name == "NeedleStyle" || properties[i].Name.StartsWith("Cap", StringComparison.Ordinal)))
					{
						propertyDescriptorCollection.Add(TypeDescriptor.CreateProperty(value.GetType(), properties[i], new ReadOnlyAttribute(true), new EditorAttribute(typeof(NullUIEditor), typeof(UITypeEditor))));
					}
					else if (circularPointer.Type == CircularPointerType.Bar && (properties[i].Name == "MarkerLength" || properties[i].Name == "MarkerStyle" || properties[i].Name == "NeedleStyle" || properties[i].Name.StartsWith("Cap", StringComparison.Ordinal)))
					{
						propertyDescriptorCollection.Add(TypeDescriptor.CreateProperty(value.GetType(), properties[i], new ReadOnlyAttribute(true), new EditorAttribute(typeof(NullUIEditor), typeof(UITypeEditor))));
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

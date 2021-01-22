using AspNetCore.ReportingServices.Rendering.WordRenderer.WordOpenXmlRenderer.Parser.dc.elements.x1_1;
using AspNetCore.ReportingServices.Rendering.WordRenderer.WordOpenXmlRenderer.Parser.package.x2006.metadata.core_properties;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer.WordOpenXmlRenderer.Models
{
	public sealed class OpenXmlDocumentPropertiesModel
	{
		private CorePropertiesPart _propertyPart;

		public CorePropertiesPart PropertiesPart
		{
			get
			{
				return this._propertyPart;
			}
		}

		public OpenXmlDocumentPropertiesModel(string author, string title, string description)
		{
			this._propertyPart = new CorePropertiesPart();
			CT_CoreProperties cT_CoreProperties = (CT_CoreProperties)this._propertyPart.Root;
			cT_CoreProperties.Creator = new SimpleLiteral
			{
				Content = WordOpenXmlUtils.Escape(author)
			};
			cT_CoreProperties.Title = new SimpleLiteral
			{
				Content = WordOpenXmlUtils.Escape(title)
			};
			cT_CoreProperties.Description = new SimpleLiteral
			{
				Content = WordOpenXmlUtils.Escape(description)
			};
		}
	}
}

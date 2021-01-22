using System.Collections.Generic;

namespace AspNetCore.ReportingServices.Rendering.HtmlRenderer
{
	public sealed class FitImageElement : ComboElement
	{
		public FitImageElement(string url, string role = null, string altText = null, string ariaLabel = null, Dictionary<string, string> customAttributes = null)
			: base(new DivElement
			{
				Role = role,
				AriaLabel = ariaLabel,
				ChildElement = new ImgElement
				{
					AltText = altText,
					Image = url,
					Size = new SizeAttribute
					{
						Width = new AutoScaleTo100Percent(),
						Height = new AutoScaleTo100Percent()
					},
					CustomAttributes = customAttributes
				},
				Size = new SizeAttribute
				{
					Width = new AutoScaleTo100Percent(),
					Height = new AutoScaleTo100Percent()
				}
			})
		{
		}
	}
}

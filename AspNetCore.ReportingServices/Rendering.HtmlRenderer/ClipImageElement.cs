using System.Collections.Generic;

namespace AspNetCore.ReportingServices.Rendering.HtmlRenderer
{
	internal sealed class ClipImageElement : ComboElement
	{
		public ClipImageElement(string url, string role = null, string altText = null, string ariaLabel = null, Dictionary<string, string> customAttributes = null)
			: base(new DivElement
			{
				Role = role,
				AriaLabel = ariaLabel,
				Overflow = HTMLElements.m_hiddenString,
				ChildElement = new ImgElement
				{
					AltText = altText,
					Image = url,
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

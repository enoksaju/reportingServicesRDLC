using System.Drawing;

namespace AspNetCore.ReportingServices.Rendering.ImageRenderer
{
	public sealed class PDFUriAction
	{
		public string Uri;

		public RectangleF Rectangle;

		public PDFUriAction(string uri, RectangleF rectangle)
		{
			this.Uri = uri;
			this.Rectangle = rectangle;
		}
	}
}

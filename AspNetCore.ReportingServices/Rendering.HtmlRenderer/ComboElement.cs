namespace AspNetCore.ReportingServices.Rendering.HtmlRenderer
{
	public abstract class ComboElement : HtmlElement
	{
		private HtmlElement Element
		{
			get;
			set;
		}

		protected ComboElement(HtmlElement element)
		{
			this.Element = element;
		}

		public void Render(IOutputStream outputStream)
		{
			this.Element.Render(outputStream);
		}
	}
}

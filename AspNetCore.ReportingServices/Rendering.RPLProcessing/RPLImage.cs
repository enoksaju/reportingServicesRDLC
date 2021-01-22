namespace AspNetCore.ReportingServices.Rendering.RPLProcessing
{
	public sealed class RPLImage : RPLItem
	{
		public RPLImage()
		{
			base.m_rplElementProps = new RPLImageProps();
			base.m_rplElementProps.Definition = new RPLImagePropsDef();
		}

		public RPLImage(long startOffset, RPLContext context)
			: base(startOffset, context)
		{
		}
	}
}

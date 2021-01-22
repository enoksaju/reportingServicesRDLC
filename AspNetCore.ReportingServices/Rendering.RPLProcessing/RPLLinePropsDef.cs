namespace AspNetCore.ReportingServices.Rendering.RPLProcessing
{
	public sealed class RPLLinePropsDef : RPLItemPropsDef
	{
		private bool m_slant;

		public bool Slant
		{
			get
			{
				return this.m_slant;
			}
			set
			{
				this.m_slant = value;
			}
		}

		public RPLLinePropsDef()
		{
		}
	}
}

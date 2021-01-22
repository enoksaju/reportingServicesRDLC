namespace AspNetCore.ReportingServices.Rendering.RPLProcessing
{
	public sealed class RPLRectanglePropsDef : RPLItemPropsDef
	{
		private string m_linkToChildId;

		public string LinkToChildId
		{
			get
			{
				return this.m_linkToChildId;
			}
			set
			{
				this.m_linkToChildId = value;
			}
		}

		public RPLRectanglePropsDef()
		{
		}
	}
}

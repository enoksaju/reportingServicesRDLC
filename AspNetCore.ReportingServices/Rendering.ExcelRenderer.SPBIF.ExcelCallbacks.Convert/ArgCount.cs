namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.SPBIF.ExcelCallbacks.Convert
{
	public sealed class ArgCount
	{
		private short m_count;

		public short Count
		{
			get
			{
				return this.m_count;
			}
			set
			{
				this.m_count = value;
			}
		}
	}
}

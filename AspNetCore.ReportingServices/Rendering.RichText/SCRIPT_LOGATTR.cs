namespace AspNetCore.ReportingServices.Rendering.RichText
{
	public struct SCRIPT_LOGATTR
	{
		private byte m_value;

		public bool IsWhiteSpace
		{
			get
			{
				return (this.m_value >> 1 & 1) > 0;
			}
		}

		public bool IsSoftBreak
		{
			get
			{
				return (this.m_value & 1 & 1) > 0;
			}
		}
	}
}

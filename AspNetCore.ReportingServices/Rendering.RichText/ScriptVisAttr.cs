namespace AspNetCore.ReportingServices.Rendering.RichText
{
	public sealed class ScriptVisAttr
	{
		private ushort m_value;

		public int uJustification;

		public int fClusterStart;

		public int fDiacritic;

		public int fZeroWidth;

		public ScriptVisAttr(ushort value)
		{
			this.m_value = value;
			this.uJustification = (this.m_value & 0xF);
			this.fClusterStart = (this.m_value >> 4 & 1);
			this.fDiacritic = (this.m_value >> 5 & 1);
			this.fZeroWidth = (this.m_value >> 6 & 1);
		}
	}
}

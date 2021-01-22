namespace AspNetCore.ReportingServices.Rendering.WordRenderer
{
	public abstract class Format
	{
		protected SprmBuffer m_grpprl;

		public Format(int initialSize, int initialOffset)
		{
			this.m_grpprl = new SprmBuffer(initialSize, initialOffset);
		}

		public void AddSprm(ushort sprmCode, int param, byte[] varParam)
		{
			this.m_grpprl.AddSprm(sprmCode, param, varParam);
		}
	}
}

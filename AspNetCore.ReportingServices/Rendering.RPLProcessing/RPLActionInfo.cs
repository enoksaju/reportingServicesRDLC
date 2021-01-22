namespace AspNetCore.ReportingServices.Rendering.RPLProcessing
{
	public class RPLActionInfo
	{
		private RPLAction[] m_actions;

		public RPLAction[] Actions
		{
			get
			{
				return this.m_actions;
			}
			set
			{
				this.m_actions = value;
			}
		}

		public RPLActionInfo()
		{
		}

		public RPLActionInfo(int count)
		{
			this.m_actions = new RPLAction[count];
		}
	}
}

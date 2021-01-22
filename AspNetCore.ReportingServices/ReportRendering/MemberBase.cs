namespace AspNetCore.ReportingServices.ReportRendering
{
	public class MemberBase
	{
		private bool m_customControl;

		public bool IsCustomControl
		{
			get
			{
				return this.m_customControl;
			}
		}

		public MemberBase(bool isCustomControl)
		{
			this.m_customControl = isCustomControl;
		}
	}
}

namespace AspNetCore.ReportingServices.ReportRendering
{
	public sealed class ActionProcessing : MemberBase
	{
		public string m_label;

		public string m_action;

		public ActionProcessing()
			: base(true)
		{
		}

		public ActionProcessing DeepClone()
		{
			ActionProcessing actionProcessing = new ActionProcessing();
			if (this.m_label != null)
			{
				actionProcessing.m_label = string.Copy(this.m_label);
			}
			if (this.m_action != null)
			{
				actionProcessing.m_action = string.Copy(this.m_action);
			}
			return actionProcessing;
		}
	}
}

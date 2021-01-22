namespace AspNetCore.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	public abstract class CustomCodeProxyBase
	{
		private IReportObjectModelProxyForCustomCode m_reportObjectModel;

		public IReportObjectModelProxyForCustomCode Report
		{
			get
			{
				return this.m_reportObjectModel;
			}
		}

        public CustomCodeProxyBase(IReportObjectModelProxyForCustomCode reportObjectModel)
		{
			this.m_reportObjectModel = reportObjectModel;
		}

		protected virtual void OnInit()
		{
		}

		public void CallOnInit()
		{
			this.OnInit();
		}
	}
}

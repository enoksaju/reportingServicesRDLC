using AspNetCore.ReportingServices.Common;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class InternalDynamicMemberLogic
	{
		protected int m_currentContext = -1;

		protected bool m_isNewContext = true;

		public bool IsNewContext
		{
			get
			{
				return this.m_isNewContext;
			}
			set
			{
				this.m_isNewContext = value;
			}
		}

		public abstract bool MoveNext();

		public int GetInstanceIndex()
		{
			return this.m_currentContext;
		}

		public abstract bool SetInstanceIndex(int index);

		public abstract ScopeID GetScopeID();

		public abstract ScopeID GetLastScopeID();

		public abstract void SetScopeID(ScopeID scopeID);

		public abstract void ResetContext();
	}
}

using System.Collections.Generic;

namespace AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing
{
	public class AggregateUpdateQueue : Queue<AggregateUpdateCollection>
	{
		private AggregateUpdateCollection m_originalState;

		public AggregateUpdateCollection OriginalState
		{
			get
			{
				return this.m_originalState;
			}
		}

		public AggregateUpdateQueue(AggregateUpdateCollection originalState)
		{
			this.m_originalState = originalState;
		}
	}
}

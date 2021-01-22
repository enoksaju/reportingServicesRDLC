using AspNetCore.ReportingServices.OnDemandProcessing.Scalability;
using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing
{
	public class CreateInstancesTraversalContext : ITraversalContext
	{
		private ScopeInstance m_parentInstance;

		private IReference<RuntimeMemberObj>[] m_innerMembers;

		private IReference<RuntimeDataTablixGroupLeafObj> m_innerGroupLeafRef;

		public ScopeInstance ParentInstance
		{
			get
			{
				return this.m_parentInstance;
			}
		}

		public IReference<RuntimeMemberObj>[] InnerMembers
		{
			get
			{
				return this.m_innerMembers;
			}
		}

		public IReference<RuntimeDataTablixGroupLeafObj> InnerGroupLeafRef
		{
			get
			{
				return this.m_innerGroupLeafRef;
			}
		}

		public CreateInstancesTraversalContext(ScopeInstance parentInstance, IReference<RuntimeMemberObj>[] innerMembers, IReference<RuntimeDataTablixGroupLeafObj> innerGroupLeafRef)
		{
			this.m_parentInstance = parentInstance;
			this.m_innerMembers = innerMembers;
			this.m_innerGroupLeafRef = innerGroupLeafRef;
		}
	}
}

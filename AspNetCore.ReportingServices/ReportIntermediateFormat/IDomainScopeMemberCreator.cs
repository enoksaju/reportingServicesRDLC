using AspNetCore.ReportingServices.ReportPublishing;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	public interface IDomainScopeMemberCreator
	{
		void CreateDomainScopeMember(ReportHierarchyNode parentNode, Grouping grouping, AutomaticSubtotalContext context);
	}
}

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	public sealed class GaugeMemberList : HierarchyNodeList
	{
		public new GaugeMember this[int index]
		{
			get
			{
				return (GaugeMember)base[index];
			}
		}
	}
}

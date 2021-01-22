namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	public sealed class MapMemberList : HierarchyNodeList
	{
		public new MapMember this[int index]
		{
			get
			{
				return (MapMember)base[index];
			}
		}
	}
}

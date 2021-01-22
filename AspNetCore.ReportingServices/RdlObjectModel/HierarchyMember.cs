namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public abstract class HierarchyMember : ReportObject
	{
		public abstract Group Group
		{
			get;
			set;
		}

		public HierarchyMember()
		{
		}

		public HierarchyMember(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}

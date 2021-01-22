namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class DataMemberCollection : DataRegionMemberCollection<DataMember>
	{
		public override string DefinitionPath
		{
			get
			{
				if (base.m_parentDefinitionPath is DataMember)
				{
					return base.m_parentDefinitionPath.DefinitionPath + "xM";
				}
				return base.m_parentDefinitionPath.DefinitionPath;
			}
		}

		public CustomReportItem OwnerCri
		{
			get
			{
				return base.m_owner as CustomReportItem;
			}
		}

		public DataMemberCollection(IDefinitionPath parentDefinitionPath, CustomReportItem owner)
			: base(parentDefinitionPath, (ReportItem)owner)
		{
		}
	}
}

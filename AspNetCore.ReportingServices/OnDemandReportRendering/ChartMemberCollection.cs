namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class ChartMemberCollection : DataRegionMemberCollection<ChartMember>
	{
		public override string DefinitionPath
		{
			get
			{
				if (base.m_parentDefinitionPath is ChartMember)
				{
					return base.m_parentDefinitionPath.DefinitionPath + "xM";
				}
				return base.m_parentDefinitionPath.DefinitionPath;
			}
		}

		public Chart OwnerChart
		{
			get
			{
				return base.m_owner as Chart;
			}
		}

		public ChartMemberCollection(IDefinitionPath parentDefinitionPath, Chart owner)
			: base(parentDefinitionPath, (ReportItem)owner)
		{
		}
	}
}

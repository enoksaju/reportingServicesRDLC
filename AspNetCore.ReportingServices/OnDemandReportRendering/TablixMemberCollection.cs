namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class TablixMemberCollection : DataRegionMemberCollection<TablixMember>
	{
		public override string DefinitionPath
		{
			get
			{
				if (base.m_parentDefinitionPath is TablixMember)
				{
					return base.m_parentDefinitionPath.DefinitionPath + "xM";
				}
				return base.m_parentDefinitionPath.DefinitionPath;
			}
		}

		public Tablix OwnerTablix
		{
			get
			{
				return base.m_owner as Tablix;
			}
		}

		public virtual double SizeDelta
		{
			get
			{
				return 0.0;
			}
		}

		public TablixMemberCollection(IDefinitionPath parentDefinitionPath, Tablix owner)
			: base(parentDefinitionPath, (ReportItem)owner)
		{
		}
	}
}

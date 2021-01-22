using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class ShimTablixMember : TablixMember, IShimDataRegionMember
	{
		protected bool m_isColumn;

		protected PageBreakLocation m_propagatedPageBreak;

		public override string UniqueName
		{
			get
			{
				return this.ID;
			}
		}

		public override string ID
		{
			get
			{
				if (base.m_group != null && base.m_group.RenderGroups != null)
				{
					return base.m_group.CurrentShimRenderGroup.ID;
				}
				return base.DefinitionPath;
			}
		}

		public override string DataElementName
		{
			get
			{
				if (base.m_group != null && base.m_group.CurrentShimRenderGroup != null)
				{
					return base.m_group.CurrentShimRenderGroup.DataCollectionName;
				}
				return null;
			}
		}

		public override DataElementOutputTypes DataElementOutput
		{
			get
			{
				if (this.IsStatic)
				{
					if (this.TablixHeader != null)
					{
						return DataElementOutputTypes.Output;
					}
					return DataElementOutputTypes.ContentsOnly;
				}
				return DataElementOutputTypes.Output;
			}
		}

		public override CustomPropertyCollection CustomProperties
		{
			get
			{
				if (base.m_customPropertyCollection == null)
				{
					if (base.m_group != null && base.m_group.CustomProperties != null)
					{
						base.m_customPropertyCollection = base.m_group.CustomProperties;
					}
					else
					{
						base.m_customPropertyCollection = new CustomPropertyCollection();
					}
				}
				return base.m_customPropertyCollection;
			}
		}

		public override TablixHeader TablixHeader
		{
			get
			{
				return null;
			}
		}

		public override bool IsColumn
		{
			get
			{
				return this.m_isColumn;
			}
		}

		public override bool HideIfNoRows
		{
			get
			{
				return false;
			}
		}

		public override AspNetCore.ReportingServices.ReportIntermediateFormat.TablixMember MemberDefinition
		{
			get
			{
				return null;
			}
		}

		public override bool FixedData
		{
			get
			{
				return false;
			}
		}

		public override KeepWithGroup KeepWithGroup
		{
			get
			{
				return KeepWithGroup.None;
			}
		}

		public override bool RepeatOnNewPage
		{
			get
			{
				return false;
			}
		}

		public override IRIFReportScope RIFReportScope
		{
			get
			{
				return null;
			}
		}

		public override IReportScopeInstance ReportScopeInstance
		{
			get
			{
				return null;
			}
		}

		public override IReportScope ReportScope
		{
			get
			{
				return null;
			}
		}

		public ShimTablixMember(IDefinitionPath parentDefinitionPath, Tablix owner, TablixMember parent, int parentCollectionIndex, bool isColumn)
			: base(parentDefinitionPath, owner, parent, parentCollectionIndex)
		{
			this.m_isColumn = isColumn;
		}

		public override void ResetContext()
		{
			base.ResetContext();
		}

		public virtual void SetPropagatedPageBreak(PageBreakLocation pageBreakLocation)
		{
			this.m_propagatedPageBreak = pageBreakLocation;
		}

		public abstract bool SetNewContext(int index);

		bool IShimDataRegionMember.SetNewContext(int index)
		{
			return this.SetNewContext(index);
		}

		void IShimDataRegionMember.ResetContext()
		{
			this.ResetContext();
		}
	}
}

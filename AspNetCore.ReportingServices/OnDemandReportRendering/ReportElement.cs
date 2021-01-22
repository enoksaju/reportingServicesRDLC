using AspNetCore.ReportingServices.ReportIntermediateFormat;
using AspNetCore.ReportingServices.ReportProcessing;
using AspNetCore.ReportingServices.ReportRendering;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class ReportElement : IDefinitionPath, IROMStyleDefinitionContainer
	{
		public enum CriGenerationPhases
		{
			None,
			Definition,
			Instance
		}

		protected bool m_isOldSnapshot;

		public AspNetCore.ReportingServices.ReportIntermediateFormat.ReportItem m_reportItemDef;

		public RenderingContext m_renderingContext;

		public AspNetCore.ReportingServices.ReportRendering.ReportItem m_renderReportItem;

		protected IDefinitionPath m_parentDefinitionPath;

		protected Style m_style;

		private IReportScope m_reportScope;

		private CustomReportItem __criOwner;

		private CriGenerationPhases __criGenerationPhase;

		public abstract string DefinitionPath
		{
			get;
		}

		public IDefinitionPath ParentDefinitionPath
		{
			get
			{
				return this.m_parentDefinitionPath;
			}
		}

		public abstract string InstanceUniqueName
		{
			get;
		}

		public ReportElementInstance Instance
		{
			get
			{
				return this.ReportElementInstance;
			}
		}

		public abstract ReportElementInstance ReportElementInstance
		{
			get;
		}

		public virtual Style Style
		{
			get
			{
				if (this.m_style == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_style = new Style(this.RenderReportItem, this.m_renderingContext, this.UseRenderStyle);
					}
					else
					{
						this.m_style = new Style(this, this.ReportScope, this.StyleContainer, this.m_renderingContext);
					}
				}
				return this.m_style;
			}
		}

		public abstract string ID
		{
			get;
		}

		public virtual bool UseRenderStyle
		{
			get
			{
				return true;
			}
		}

		public virtual IStyleContainer StyleContainer
		{
			get
			{
				return this.m_reportItemDef;
			}
		}

		public AspNetCore.ReportingServices.ReportIntermediateFormat.ReportItem ReportItemDef
		{
			get
			{
				return this.m_reportItemDef;
			}
		}

		public RenderingContext RenderingContext
		{
			get
			{
				return this.m_renderingContext;
			}
		}

		public bool IsOldSnapshot
		{
			get
			{
				return this.m_isOldSnapshot;
			}
		}

		public virtual AspNetCore.ReportingServices.ReportRendering.ReportItem RenderReportItem
		{
			get
			{
				if (!this.m_isOldSnapshot)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				return this.m_renderReportItem;
			}
		}

		public virtual IReportScope ReportScope
		{
			get
			{
				return this.m_reportScope;
			}
		}

		public CustomReportItem CriOwner
		{
			get
			{
				Global.Tracer.Assert(!this.m_isOldSnapshot || this.__criOwner == null, "(!m_isOldSnapshot || __criOwner == null)");
				return this.__criOwner;
			}
			set
			{
				Global.Tracer.Assert(!this.m_isOldSnapshot, "(!m_isOldSnapshot)");
				this.__criOwner = value;
			}
		}

		public CriGenerationPhases CriGenerationPhase
		{
			get
			{
				Global.Tracer.Assert(!this.m_isOldSnapshot || this.__criGenerationPhase == CriGenerationPhases.None, "(!m_isOldSnapshot || __criGenerationPhase == CriGenerationPhases.None)");
				return this.__criGenerationPhase;
			}
			set
			{
				Global.Tracer.Assert(!this.m_isOldSnapshot, "(!m_isOldSnapshot)");
				this.__criGenerationPhase = value;
			}
		}

		public ReportElement(IDefinitionPath parentDefinitionPath)
		{
			this.m_parentDefinitionPath = parentDefinitionPath;
		}

		public ReportElement(IReportScope reportScope, IDefinitionPath parentDefinitionPath, AspNetCore.ReportingServices.ReportIntermediateFormat.ReportItem reportItemDef, RenderingContext renderingContext)
		{
			this.m_reportScope = reportScope;
			this.m_parentDefinitionPath = parentDefinitionPath;
			this.m_reportItemDef = reportItemDef;
			this.m_renderingContext = renderingContext;
			this.m_isOldSnapshot = false;
		}

		public ReportElement(IDefinitionPath parentDefinitionPath, AspNetCore.ReportingServices.ReportRendering.ReportItem renderReportItem, RenderingContext renderingContext)
		{
			this.m_parentDefinitionPath = parentDefinitionPath;
			this.m_renderReportItem = renderReportItem;
			this.m_renderingContext = renderingContext;
			this.m_isOldSnapshot = true;
		}

		public ReportElement(IDefinitionPath parentDefinitionPath, RenderingContext renderingContext)
		{
			this.m_parentDefinitionPath = parentDefinitionPath;
			this.m_renderingContext = renderingContext;
			this.m_isOldSnapshot = true;
		}

		public virtual void SetNewContext()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.SetNewContextChildren();
		}

		public abstract void SetNewContextChildren();

		public void ConstructReportElementDefinitionImpl()
		{
			Global.Tracer.Assert(this.CriGenerationPhase == CriGenerationPhases.Definition, "(CriGenerationPhase == CriGenerationPhases.Definition)");
			this.Style.ConstructStyleDefinition();
		}
	}
}

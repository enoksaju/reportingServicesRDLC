using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class Tag
	{
		private readonly Image m_image;

		private readonly ExpressionInfo m_expression;

		private ReportVariantProperty m_value;

		private TagInstance m_instance;

		public ReportVariantProperty Value
		{
			get
			{
				if (this.m_value == null)
				{
					this.m_value = new ReportVariantProperty(this.m_expression);
				}
				return this.m_value;
			}
		}

		public TagInstance Instance
		{
			get
			{
				if (this.m_instance == null)
				{
					this.m_instance = new TagInstance(this);
				}
				return this.m_instance;
			}
		}

		public Image Image
		{
			get
			{
				return this.m_image;
			}
		}

		public ExpressionInfo Expression
		{
			get
			{
				return this.m_expression;
			}
		}

		public Tag(Image image, ExpressionInfo expression)
		{
			this.m_image = image;
			this.m_expression = expression;
		}

		public void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.ResetInstanceCache();
			}
		}
	}
}

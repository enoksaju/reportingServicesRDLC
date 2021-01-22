namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class ReportEnumProperty<EnumType> : ReportProperty where EnumType : struct
	{
		private EnumType m_value;

		public EnumType Value
		{
			get
			{
				return this.m_value;
			}
		}

		public ReportEnumProperty()
		{
			this.m_value = default(EnumType);
		}

		public ReportEnumProperty(EnumType value)
		{
			this.m_value = value;
		}

		public ReportEnumProperty(bool isExpression, string expressionString, EnumType value)
			: this(isExpression, expressionString, value, default(EnumType))
		{
		}

		public ReportEnumProperty(bool isExpression, string expressionString, EnumType value, EnumType defaultValue)
			: base(isExpression, expressionString)
		{
			if (!isExpression)
			{
				this.m_value = value;
			}
			else
			{
				this.m_value = defaultValue;
			}
		}
	}
}

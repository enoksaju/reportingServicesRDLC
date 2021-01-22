namespace AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel
{
	public sealed class TextBoxImpl : ReportItemImpl
	{
		private TextBox m_textBox;

		private VariantResult m_result;

		private bool m_isValueReady;

		private bool m_isVisited;

		public override object Value
		{
			get
			{
				this.GetResult();
				return this.m_result.Value;
			}
		}

		public TextBoxImpl(TextBox itemDef, ReportRuntime reportRT, IErrorContext iErrorContext)
			: base(itemDef, reportRT, iErrorContext)
		{
			this.m_textBox = itemDef;
		}

		public void SetResult(VariantResult result)
		{
			this.m_result = result;
			this.m_isValueReady = true;
		}

		public VariantResult GetResult()
		{
			if (!this.m_isValueReady)
			{
				if (this.m_isVisited)
				{
					base.m_iErrorContext.Register(ProcessingErrorCode.rsCyclicExpression, Severity.Warning, this.m_textBox.ObjectType, this.m_textBox.Name, "Value");
					throw new ReportProcessingException_InvalidOperationException();
				}
				this.m_isVisited = true;
				ObjectType objectType = base.m_reportRT.ObjectType;
				string objectName = base.m_reportRT.ObjectName;
				string propertyName = base.m_reportRT.PropertyName;
				ReportProcessing.IScope currentScope = base.m_reportRT.CurrentScope;
				base.m_reportRT.CurrentScope = base.m_scope;
				this.m_result = base.m_reportRT.EvaluateTextBoxValueExpression(this.m_textBox);
				base.m_reportRT.CurrentScope = currentScope;
				base.m_reportRT.ObjectType = objectType;
				base.m_reportRT.ObjectName = objectName;
				base.m_reportRT.PropertyName = propertyName;
				this.m_isVisited = false;
				this.m_isValueReady = true;
			}
			return this.m_result;
		}

		public void Reset()
		{
			this.m_isValueReady = false;
		}
	}
}

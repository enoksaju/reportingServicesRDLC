namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.SPBIF.ExcelCallbacks.Convert
{
	public sealed class Conditional : Operator
	{
		private string m_gotoLabel;

		private string m_label;

		public string Label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
			}
		}

		public string GotoLabel
		{
			get
			{
				return this.m_gotoLabel;
			}
			set
			{
				this.m_gotoLabel = value;
			}
		}

		public Conditional(string op, int precedence, OperatorType ot, ushort biffCode)
			: base(op, precedence, ot, biffCode)
		{
		}

		public Conditional(string op, int precedence, OperatorType ot, ushort biffCode, uint functionCode, short numOfArgs)
			: base(op, precedence, ot, biffCode, functionCode, numOfArgs)
		{
		}

		public Conditional(Conditional conditionalOp)
			: base(conditionalOp.Name, conditionalOp.Precedence, conditionalOp.Type, conditionalOp.BCode, conditionalOp.FCode, conditionalOp.ArgumentCount)
		{
			this.m_label = conditionalOp.Label;
		}
	}
}

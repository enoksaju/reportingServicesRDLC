using System.Collections;

namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.SPBIF.ExcelCallbacks.Convert
{
	public sealed class OptimizedChoose : Operator
	{
		private ArrayList m_gotoLabels;

		public ArrayList GotoLabelList
		{
			get
			{
				return this.m_gotoLabels;
			}
			set
			{
				this.m_gotoLabels = value;
			}
		}

		public OptimizedChoose(string op, int precedence, OperatorType ot, ushort biffCode)
			: base(op, precedence, ot, biffCode)
		{
		}

		public OptimizedChoose(OptimizedChoose oc)
			: base(oc.Name, oc.Precedence, oc.Type, oc.BCode)
		{
		}
	}
}

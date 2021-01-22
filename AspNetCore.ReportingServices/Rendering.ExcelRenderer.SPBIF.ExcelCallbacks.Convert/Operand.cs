namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.SPBIF.ExcelCallbacks.Convert
{
	public sealed class Operand
	{
		public enum OperandType
		{
			USHORT,
			DOUBLE,
			BOOLEAN,
			STRING,
			NAME
		}

		private object m_operandValue;

		private OperandType m_type;

		public object OperandValue
		{
			get
			{
				return this.m_operandValue;
			}
		}

		public OperandType Type
		{
			get
			{
				return this.m_type;
			}
		}

		public Operand(object operandValue, OperandType type)
		{
			this.m_operandValue = operandValue;
			this.m_type = type;
		}
	}
}

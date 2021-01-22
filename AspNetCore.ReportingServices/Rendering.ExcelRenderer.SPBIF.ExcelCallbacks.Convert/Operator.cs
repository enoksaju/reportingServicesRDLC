using System;

namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.SPBIF.ExcelCallbacks.Convert
{
	public class Operator
	{
		public enum OperatorType
		{
			ARITHMETIC,
			LOGICAL,
			FUNCTION,
			DELIMITER
		}

		private string m_operator;

		private int m_precedence;

		private OperatorType m_type;

		private ushort m_biffCode;

		private uint m_functionCode;

		private short m_numOfArgs;

		private bool m_variableArgs;

		public string Name
		{
			get
			{
				return this.m_operator;
			}
		}

		public int Precedence
		{
			get
			{
				return this.m_precedence;
			}
		}

		public OperatorType Type
		{
			get
			{
				return this.m_type;
			}
		}

		public ushort BCode
		{
			get
			{
				return this.m_biffCode;
			}
		}

		public uint FCode
		{
			get
			{
				return this.m_functionCode;
			}
		}

		public short ArgumentCount
		{
			get
			{
				return this.m_numOfArgs;
			}
			set
			{
				this.m_numOfArgs = value;
			}
		}

		public byte[] BiffOperator
		{
			get
			{
				return BitConverter.GetBytes(this.m_biffCode);
			}
		}

		public byte[] FunctionCode
		{
			get
			{
				return BitConverter.GetBytes(this.m_functionCode);
			}
		}

		public byte[] NumberOfArguments
		{
			get
			{
				return BitConverter.GetBytes(this.m_numOfArgs);
			}
		}

		public Operator(string op, int precedence, OperatorType ot, ushort biffCode)
		{
			this.m_operator = op;
			this.m_precedence = precedence;
			this.m_type = ot;
			this.m_biffCode = biffCode;
		}

		public Operator(string op, int precedence, OperatorType ot, ushort biffCode, uint functionCode)
		{
			this.m_operator = op;
			this.m_precedence = precedence;
			this.m_type = ot;
			this.m_biffCode = biffCode;
			this.m_functionCode = functionCode;
		}

		public Operator(string op, int precedence, OperatorType ot, ushort biffCode, uint functionCode, short numOfArgs)
		{
			this.m_operator = op;
			this.m_precedence = precedence;
			this.m_type = ot;
			this.m_biffCode = biffCode;
			this.m_functionCode = functionCode;
			if (biffCode == 66 && numOfArgs == -1)
			{
				this.m_numOfArgs = 0;
				this.m_variableArgs = true;
			}
			else
			{
				this.m_numOfArgs = numOfArgs;
			}
		}

		public Operator(Operator op)
		{
			this.m_operator = op.m_operator;
			this.m_precedence = op.m_precedence;
			this.m_type = op.m_type;
			this.m_biffCode = op.m_biffCode;
			this.m_functionCode = op.m_functionCode;
			this.m_numOfArgs = op.m_numOfArgs;
		}

		public bool HasVariableArguments()
		{
			return this.m_variableArgs;
		}
	}
}

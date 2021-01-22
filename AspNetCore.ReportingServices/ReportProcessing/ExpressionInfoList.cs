using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ExpressionInfoList : ArrayList
	{
		public new ExpressionInfo this[int index]
		{
			get
			{
				return (ExpressionInfo)base[index];
			}
		}

		public ExpressionInfoList()
		{
		}

		public ExpressionInfoList(int capacity)
			: base(capacity)
		{
		}
	}
}

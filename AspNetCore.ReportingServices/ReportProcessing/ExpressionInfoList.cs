using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	internal sealed class ExpressionInfoList : ArrayList
	{
		internal new ExpressionInfo this[int index]
		{
			get
			{
				return (ExpressionInfo)base[index];
			}
		}

		internal ExpressionInfoList()
		{
		}

		internal ExpressionInfoList(int capacity)
			: base(capacity)
		{
		}
	}
}

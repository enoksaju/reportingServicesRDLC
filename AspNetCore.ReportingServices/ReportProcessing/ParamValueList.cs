using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	public sealed class ParamValueList : ArrayList
	{
		public new ParamValue this[int index]
		{
			get
			{
				return (ParamValue)base[index];
			}
		}

		public ParamValueList()
		{
		}

		public ParamValueList(int capacity)
			: base(capacity)
		{
		}
	}
}

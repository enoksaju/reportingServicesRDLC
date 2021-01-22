using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	public sealed class ToggleItemInfoList : ArrayList
	{
		public new ToggleItemInfo this[int index]
		{
			get
			{
				return (ToggleItemInfo)base[index];
			}
		}
	}
}

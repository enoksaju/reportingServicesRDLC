using System;
using System.Collections;

namespace AspNetCore.ReportingServices.Rendering.HtmlRenderer
{
	[Serializable]
	public sealed class PageTableCellList : ArrayList
	{
		public new PageTableCell this[int index]
		{
			get
			{
				return (PageTableCell)base[index];
			}
		}
	}
}

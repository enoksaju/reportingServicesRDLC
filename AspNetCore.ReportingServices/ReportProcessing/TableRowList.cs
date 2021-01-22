using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class TableRowList : ArrayList
	{
		public new TableRow this[int index]
		{
			get
			{
				return (TableRow)base[index];
			}
		}

		public TableRowList()
		{
		}

		public TableRowList(int capacity)
			: base(capacity)
		{
		}

		public void Register(InitializationContext context)
		{
			for (int i = 0; i < this.Count; i++)
			{
				Global.Tracer.Assert(null != this[i]);
				context.RegisterReportItems(this[i].ReportItems);
			}
		}

		public void UnRegister(InitializationContext context)
		{
			for (int i = 0; i < this.Count; i++)
			{
				Global.Tracer.Assert(null != this[i]);
				context.UnRegisterReportItems(this[i].ReportItems);
			}
		}

		public double GetHeightValue()
		{
			double num = 0.0;
			for (int i = 0; i < this.Count; i++)
			{
				if (!this[i].StartHidden)
				{
					num += this[i].HeightValue;
				}
			}
			return num;
		}
	}
}

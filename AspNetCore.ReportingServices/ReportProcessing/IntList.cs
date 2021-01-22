using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class IntList : ArrayList
	{
		public new int this[int index]
		{
			get
			{
				return (int)base[index];
			}
			set
			{
				base[index] = value;
			}
		}

		public IntList()
		{
		}

		public IntList(int capacity)
			: base(capacity)
		{
		}

		public void CopyTo(IntList target)
		{
			if (target != null)
			{
				target.Clear();
				for (int i = 0; i < this.Count; i++)
				{
					target.Add(this[i]);
				}
			}
		}
	}
}

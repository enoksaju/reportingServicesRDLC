using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	internal sealed class DataValueInstanceList : ArrayList
	{
		internal new DataValueInstance this[int index]
		{
			get
			{
				return (DataValueInstance)base[index];
			}
		}

		internal DataValueInstanceList()
		{
		}

		internal DataValueInstanceList(int capacity)
			: base(capacity)
		{
		}
	}
}

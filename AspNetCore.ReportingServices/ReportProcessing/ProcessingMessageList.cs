using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	internal sealed class ProcessingMessageList : ArrayList
	{
		public new ProcessingMessage this[int index]
		{
			get
			{
				return (ProcessingMessage)base[index];
			}
		}

		public ProcessingMessageList()
		{
		}

		internal ProcessingMessageList(int capacity)
			: base(capacity)
		{
		}
	}
}

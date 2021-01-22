using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ProcessingMessageList : ArrayList
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

		public ProcessingMessageList(int capacity)
			: base(capacity)
		{
		}
	}
}

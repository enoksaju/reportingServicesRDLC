using System;

namespace AspNetCore.ReportingServices.OnDemandProcessing
{
	public sealed class ProcessingAbortEventArgs : EventArgs
	{
		private string m_uniqueName;

		public string UniqueName
		{
			get
			{
				return this.m_uniqueName;
			}
		}

		public ProcessingAbortEventArgs(string uniqueName)
		{
			this.m_uniqueName = uniqueName;
		}
	}
}

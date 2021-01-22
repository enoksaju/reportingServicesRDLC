using System;
using System.Diagnostics;

namespace AspNetCore.ReportingServices.Rendering.RichText
{
	public class PerfCheck : IDisposable
	{
		private string m_Name;

		private Stopwatch m_sw = new Stopwatch();

		public PerfCheck(string Name)
		{
			this.m_Name = Name;
		}

		[Conditional("PERF_CHECK")]
		private void Start()
		{
			this.m_sw.Start();
		}

		[Conditional("PERF_CHECK")]
		private void Stop()
		{
			this.m_sw.Stop();
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
	}
}

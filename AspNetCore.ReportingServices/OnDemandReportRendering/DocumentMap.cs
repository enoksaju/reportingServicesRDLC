using System;
using System.Collections;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class DocumentMap : IEnumerator<DocumentMapNode>, IDisposable, IEnumerator
	{
		protected bool m_isClosed;

		public bool IsClosed
		{
			get
			{
				return this.m_isClosed;
			}
		}

		public abstract DocumentMapNode Current
		{
			get;
		}

		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		public abstract void Close();

		public abstract void Dispose();

		public abstract bool MoveNext();

		public abstract void Reset();
	}
}

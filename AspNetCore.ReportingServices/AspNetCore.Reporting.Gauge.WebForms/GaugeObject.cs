using System;

namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class GaugeObject : IDisposable
	{
		public bool initialized = true;

		private object parent;

		private CommonElements common;

		private bool disposed;

		public virtual object Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		public virtual CommonElements Common
		{
			get
			{
				if (this.common == null)
				{
					object obj = this.Parent;
					if (obj is GaugeObject)
					{
						this.common = ((GaugeObject)obj).Common;
					}
					else if (obj is NamedElement)
					{
						this.common = ((NamedElement)obj).Common;
					}
					else if (obj is NamedCollection)
					{
						this.common = ((NamedCollection)obj).Common;
					}
					else if (obj is GaugeCore)
					{
						this.common = ((GaugeCore)obj).Common;
					}
				}
				return this.common;
			}
			set
			{
				this.common = value;
			}
		}

		public GaugeObject(object parent)
		{
			this.parent = parent;
		}

		public virtual void Invalidate()
		{
			if (this.Common != null)
			{
				this.Common.GaugeCore.Invalidate();
			}
		}

		public virtual void Refresh()
		{
			if (this.Common != null)
			{
				this.Common.GaugeCore.Refresh();
			}
		}

		public virtual void BeginInit()
		{
			this.initialized = false;
		}

		public virtual void EndInit()
		{
			this.initialized = true;
		}

		public virtual void ReconnectData(bool exact)
		{
		}

		public virtual void Notify(MessageType msg, NamedElement element, object param)
		{
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				this.OnDispose();
			}
			this.disposed = true;
		}

		protected virtual void OnDispose()
		{
		}
	}
}

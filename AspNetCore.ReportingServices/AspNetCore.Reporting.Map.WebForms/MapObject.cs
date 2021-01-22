using System;
using System.Drawing;

namespace AspNetCore.Reporting.Map.WebForms
{
	public class MapObject : IDisposable
	{
		public bool initialized = true;

		private object parent;

		private CommonElements common;

		protected bool disposed;

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
					if (obj is MapObject)
					{
						this.common = ((MapObject)obj).Common;
					}
					else if (obj is NamedElement)
					{
						this.common = ((NamedElement)obj).Common;
					}
					else if (obj is NamedCollection)
					{
						this.common = ((NamedCollection)obj).Common;
					}
					else if (obj is MapCore)
					{
						this.common = ((MapCore)obj).Common;
					}
				}
				return this.common;
			}
			set
			{
				this.common = value;
			}
		}

		public MapObject(object parent)
		{
			this.parent = parent;
		}

		public virtual void Invalidate()
		{
			if (this.Common != null)
			{
				this.Common.MapCore.Invalidate();
			}
		}

		public virtual void Invalidate(RectangleF rect)
		{
			if (this.Common != null)
			{
				this.Common.MapCore.Invalidate(rect);
			}
		}

		public virtual void InvalidateViewport(bool invalidateGridSections)
		{
			if (this.Common != null)
			{
				this.Common.MapCore.InvalidateViewport(invalidateGridSections);
			}
		}

		public virtual void InvalidateDistanceScalePanel()
		{
			if (this.Common != null)
			{
				this.Common.MapCore.InvalidateDistanceScalePanel();
			}
		}

		public virtual void InvalidateViewport()
		{
			if (this.Common != null)
			{
				this.Common.MapCore.InvalidateViewport(true);
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

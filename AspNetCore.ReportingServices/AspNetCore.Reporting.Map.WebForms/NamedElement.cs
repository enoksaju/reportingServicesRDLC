using System;
using System.ComponentModel;
using System.Drawing;

namespace AspNetCore.Reporting.Map.WebForms
{
	public abstract class NamedElement : IDisposable, ICloneable
	{
		private string name = string.Empty;

		public CommonElements common;

		public NamedCollection collection;

		public bool initialized = true;

		private object tag;

		public bool disposed;

		[SerializationVisibility(SerializationVisibility.Hidden)]
		[Browsable(false)]
		[Description("Indicates that map area is custom.")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual CommonElements Common
		{
			get
			{
				return this.common;
			}
			set
			{
				if (this.common != value)
				{
					if (value == null)
					{
						this.OnRemove();
						this.common = value;
					}
					else
					{
						this.common = value;
						this.OnAdded();
					}
				}
			}
		}

		[SerializationVisibility(SerializationVisibility.Hidden)]
		[SRDescription("DescriptionAttributeNamedElement_Collection")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		[Browsable(false)]
		public virtual NamedCollection Collection
		{
			get
			{
				return this.collection;
			}
			set
			{
				this.collection = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SerializationVisibility(SerializationVisibility.Hidden)]
		[SRDescription("DescriptionAttributeNamedElement_ParentElement")]
		[DefaultValue("")]
		public virtual NamedElement ParentElement
		{
			get
			{
				if (this.collection != null)
				{
					return this.collection.ParentElement;
				}
				return null;
			}
		}

		public virtual string DefaultName
		{
			get
			{
				return string.Empty;
			}
		}

		[SRDescription("DescriptionAttributeNamedElement_Name")]
		public virtual string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (this.collection != null)
				{
					this.collection.IsValidNameCheck(value, this);
				}
				if (this.Common != null)
				{
					this.Common.MapCore.Notify(MessageType.NamedElementRename, this, value);
				}
				this.name = value;
				this.OnNameChanged();
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SerializationVisibility(SerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		public NamedElement()
			: this(null)
		{
		}

		public NamedElement(CommonElements common)
		{
			this.Common = common;
		}

		public virtual void OnRemove()
		{
			if (this.Common != null)
			{
				this.Common.MapCore.Notify(MessageType.NamedElementRemove, this, null);
			}
		}

		public virtual void OnAdded()
		{
			if (this.Common != null)
			{
				this.Common.MapCore.Notify(MessageType.NamedElementAdded, this, null);
			}
		}

		public virtual void OnNameChanged()
		{
		}

		public virtual void BeginInit()
		{
			this.initialized = false;
		}

		public virtual void EndInit()
		{
			this.initialized = true;
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

		public virtual void InvalidateViewport()
		{
			if (this.Common != null)
			{
				this.Common.MapCore.InvalidateViewport(true);
			}
		}

		public virtual void InvalidateDistanceScalePanel()
		{
			if (this.Common != null)
			{
				this.Common.MapCore.InvalidateDistanceScalePanel();
			}
		}

		public virtual void InvalidateAndLayout()
		{
			if (this.Common != null)
			{
				this.Common.MapCore.InvalidateAndLayout();
			}
		}

		public virtual void ReconnectData(bool exact)
		{
		}

		public virtual void Notify(MessageType msg, NamedElement element, object param)
		{
		}

		public string GetNameAsParent()
		{
			return this.GetNameAsParent(this.Name);
		}

		public string GetNameAsParent(string newName)
		{
			if (this.Collection != null)
			{
				return this.Collection.GetCollectionName() + "." + newName;
			}
			return newName;
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

		public virtual object Clone()
		{
			return this.CloneInternals(this.InitiateCopy());
		}

		public virtual object InitiateCopy()
		{
			return base.MemberwiseClone();
		}

		public virtual object CloneInternals(object copy)
		{
			return copy;
		}
	}
}

using System;
using System.ComponentModel;

namespace AspNetCore.Reporting.Gauge.WebForms
{
	[Serializable]
	public class NamedElement : IDisposable, ICloneable
	{
		private string name = string.Empty;

		[NonSerialized]
		public CommonElements common;

		[NonSerialized]
		public NamedCollection collection;

		public bool initialized = true;

		private object tag;

		public bool disposed;

		[Browsable(false)]
		[SerializationVisibility(SerializationVisibility.Hidden)]
		[SRDescription("DescriptionAttributeCommon")]
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

		[DefaultValue("")]
		[SerializationVisibility(SerializationVisibility.Hidden)]
		[Browsable(false)]
		[SRDescription("DescriptionAttributeCollection")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		[SerializationVisibility(SerializationVisibility.Hidden)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[SRDescription("DescriptionAttributeParentElement")]
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

		[SRDescription("DescriptionAttributeName10")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
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
					this.Common.GaugeCore.Notify(MessageType.NamedElementRename, this, value);
				}
				this.name = value;
				this.OnNameChanged();
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[SerializationVisibility(SerializationVisibility.Hidden)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
		{
		}

		protected NamedElement(string name)
		{
			this.name = name;
		}

		public virtual void OnRemove()
		{
			if (this.Common != null)
			{
				this.Common.GaugeCore.Notify(MessageType.NamedElementRemove, this, null);
			}
		}

		public virtual void OnAdded()
		{
			if (this.Common != null)
			{
				this.Common.GaugeCore.Notify(MessageType.NamedElementAdded, this, null);
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

using System;

namespace AspNetCore.Reporting.Map.WebForms
{
	internal class LayerCollection : NamedCollection
	{
		private Layer this[int index]
		{
			get
			{
				return (Layer)base.List[index];
			}
			set
			{
				base.List.Insert(index, value);
			}
		}

		private Layer this[string name]
		{
			get
			{
				return (Layer)base.GetByNameCheck(name);
			}
			set
			{
				base.SetByNameCheck(name, value);
			}
		}

		public Layer this[object obj]
		{
			get
			{
				if (obj is string)
				{
					return this[(string)obj];
				}
				if (obj is int)
				{
					return this[(int)obj];
				}
				throw new ArgumentException(SR.ExceptionInvalidIndexerArgument);
			}
			set
			{
				if (obj is string)
				{
					this[(string)obj] = value;
					return;
				}
				if (obj is int)
				{
					this[(int)obj] = value;
					return;
				}
				throw new ArgumentException(SR.ExceptionInvalidIndexerArgument);
			}
		}

		internal LayerCollection(NamedElement parent, CommonElements common)
			: base(parent, common)
		{
			base.elementType = typeof(Layer);
		}

		public Layer Add(string name)
		{
			Layer layer = new Layer();
			layer.Name = name;
			this.Add(layer);
			return layer;
		}

		public int Add(Layer value)
		{
			return base.List.Add(value);
		}

		public void Remove(Layer value)
		{
			base.List.Remove(value);
		}

		internal override string GetDefaultElementName(NamedElement el)
		{
			return "Layer1";
		}

		internal override string GetElementNameFormat(NamedElement el)
		{
			return "Layer{0}";
		}

		internal override void Invalidate()
		{
			if (base.Common != null)
			{
				base.Common.MapCore.InvalidateDataBinding();
			}
			base.Invalidate();
		}

		internal bool HasVisibleLayer()
		{
			foreach (Layer item in this)
			{
				if (item.Visible)
				{
					return true;
				}
			}
			return false;
		}
	}
}

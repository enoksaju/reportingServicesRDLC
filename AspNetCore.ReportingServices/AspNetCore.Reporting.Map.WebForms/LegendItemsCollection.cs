using System;
using System.ComponentModel;
using System.Drawing;

namespace AspNetCore.Reporting.Map.WebForms
{
	[Description("Legend items collection.")]
	public class LegendItemsCollection : NamedCollection
	{
		public Legend Legend;

		private LegendItem this[int index]
		{
			get
			{
				return (LegendItem)base.List[index];
			}
			set
			{
				base.List.Insert(index, value);
			}
		}

		private LegendItem this[string name]
		{
			get
			{
				return (LegendItem)base.GetByNameCheck(name);
			}
			set
			{
				base.SetByNameCheck(name, value);
			}
		}

		public LegendItem this[object obj]
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

		public LegendItemsCollection(NamedElement parent, CommonElements common)
			: base(parent, common)
		{
			base.elementType = typeof(LegendItem);
		}

		public void Insert(int index, Color color, string text)
		{
			LegendItem value = new LegendItem(text, color, "");
			base.List.Insert(index, value);
		}

		public void Insert(int index, string image, string text)
		{
			LegendItem value = new LegendItem(text, Color.Empty, image);
			base.List.Insert(index, value);
		}

		public LegendItem Add(string name)
		{
			LegendItem legendItem = new LegendItem();
			legendItem.Name = name;
			this.Add(legendItem);
			return legendItem;
		}

		public int Add(LegendItem item)
		{
			return base.List.Add(item);
		}

		public override string GetDefaultElementName(NamedElement el)
		{
			return "LegendItem1";
		}

		public override string GetElementNameFormat(NamedElement el)
		{
			return "LegendItem{0}";
		}

		public override void Invalidate()
		{
			if (this.Legend != null)
			{
				this.Legend.Invalidate();
			}
		}

		protected override void OnInsertComplete(int index, object value)
		{
			((LegendItem)value).Legend = this.Legend;
			base.OnInsertComplete(index, value);
			this.Invalidate();
		}

		protected override void OnClearComplete()
		{
			base.OnClearComplete();
			this.Invalidate();
		}

		protected override void OnSetComplete(int index, object oldValue, object newValue)
		{
			((LegendItem)newValue).Legend = this.Legend;
			base.OnSetComplete(index, oldValue, newValue);
			this.Invalidate();
		}
	}
}

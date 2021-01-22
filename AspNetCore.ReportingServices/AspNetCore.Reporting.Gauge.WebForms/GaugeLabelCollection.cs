using System;

namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class GaugeLabelCollection : NamedCollection
	{
		private GaugeLabel this[int index]
		{
			get
			{
				if (index == 0 && base.List.Count == 0)
				{
					this.Add(new GaugeLabel());
				}
				return (GaugeLabel)base.List[index];
			}
			set
			{
				base.List.Insert(index, value);
			}
		}

		private GaugeLabel this[string name]
		{
			get
			{
				return (GaugeLabel)base.GetByNameCheck(name);
			}
			set
			{
				base.SetByNameCheck(name, value);
			}
		}

		public GaugeLabel this[object obj]
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
				throw new ArgumentException(Utils.SRGetStr("ExceptionInvalidIndexer_error"));
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
				throw new ArgumentException(Utils.SRGetStr("ExceptionInvalidIndexer_error"));
			}
		}

		public GaugeLabelCollection(NamedElement parent, CommonElements common)
			: base(parent, common)
		{
			base.elementType = typeof(GaugeLabel);
		}

		public GaugeLabel Add(string name)
		{
			GaugeLabel gaugeLabel = new GaugeLabel();
			gaugeLabel.Name = name;
			this.Add(gaugeLabel);
			return gaugeLabel;
		}

		public int Add(GaugeLabel value)
		{
			return base.List.Add(value);
		}

		public void Remove(GaugeLabel value)
		{
			base.List.Remove(value);
		}

		public bool Contains(GaugeLabel value)
		{
			return base.List.Contains(value);
		}

		public void Insert(int index, GaugeLabel value)
		{
			base.List.Insert(index, value);
		}

		public int IndexOf(GaugeLabel value)
		{
			return base.List.IndexOf(value);
		}

		public override string GetDefaultElementName(NamedElement el)
		{
			return "Label1";
		}

		public override string GetElementNameFormat(NamedElement el)
		{
			return "Label{0}";
		}

		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete(index, value);
			GaugeLabel gaugeLabel = (GaugeLabel)value;
			if (gaugeLabel.Position.DefaultValues && index != 0)
			{
				GaugeLabel gaugeLabel2 = this[index - 1];
				gaugeLabel.Location.X = (float)(gaugeLabel2.Location.X + 3.0);
				gaugeLabel.Location.Y = (float)(gaugeLabel2.Location.Y + 3.0);
			}
			if (gaugeLabel.DefaultParent && gaugeLabel.Parent.Length == 0 && base.Common != null)
			{
				if (base.Common.GaugeContainer.CircularGauges.Count > 0)
				{
					gaugeLabel.Parent = "CircularGauges." + base.Common.GaugeContainer.CircularGauges[0].Name;
				}
				else if (base.Common.GaugeContainer.LinearGauges.Count > 0)
				{
					gaugeLabel.Parent = "LinearGauges." + base.Common.GaugeContainer.LinearGauges[0].Name;
				}
			}
		}
	}
}

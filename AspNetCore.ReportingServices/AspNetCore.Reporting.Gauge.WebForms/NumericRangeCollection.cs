using System;

namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class NumericRangeCollection : NamedCollection
	{
		private NumericRange this[int index]
		{
			get
			{
				if (index == 0 && base.List.Count == 0)
				{
					this.Add(new NumericRange());
				}
				return (NumericRange)base.List[index];
			}
			set
			{
				base.List.Insert(index, value);
			}
		}

		private NumericRange this[string name]
		{
			get
			{
				return (NumericRange)base.GetByNameCheck(name);
			}
			set
			{
				base.SetByNameCheck(name, value);
			}
		}

		public NumericRange this[object obj]
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

		public NumericRangeCollection(NamedElement parent, CommonElements common)
			: base(parent, common)
		{
			base.elementType = typeof(NumericRange);
		}

		public NumericRange Add(string name)
		{
			NumericRange numericRange = new NumericRange();
			numericRange.Name = name;
			this.Add(numericRange);
			return numericRange;
		}

		public int Add(NumericRange value)
		{
			return base.List.Add(value);
		}

		public void Remove(NumericRange value)
		{
			base.List.Remove(value);
		}

		public bool Contains(NumericRange value)
		{
			return base.List.Contains(value);
		}

		public void Insert(int index, NumericRange value)
		{
			base.List.Insert(index, value);
		}

		public int IndexOf(NumericRange value)
		{
			return base.List.IndexOf(value);
		}

		public override string GetElementNameFormat(NamedElement el)
		{
			return "Range{0}";
		}
	}
}

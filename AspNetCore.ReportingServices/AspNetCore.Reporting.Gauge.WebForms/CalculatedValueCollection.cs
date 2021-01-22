using System;
using System.Globalization;

namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class CalculatedValueCollection : NamedCollection
	{
		public CalculatedValue this[object obj]
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
				throw new ArgumentException(Utils.SRGetStr("ExceptionInvalidIndexer"));
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
				throw new ArgumentException(Utils.SRGetStr("ExceptionInvalidIndexer"));
			}
		}

		private CalculatedValue this[string name]
		{
			get
			{
				return (CalculatedValue)base.GetByNameCheck(name);
			}
			set
			{
				base.SetByNameCheck(name, value);
			}
		}

		private CalculatedValue this[int index]
		{
			get
			{
				return (CalculatedValue)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		public CalculatedValueCollection(NamedElement parent, CommonElements common)
			: base(parent, common)
		{
			base.elementType = typeof(CalculatedValue);
		}

		public CalculatedValue Add(string name)
		{
			CalculatedValue calculatedValue = new CalculatedValue();
			calculatedValue.Name = name;
			this.Add(calculatedValue);
			return calculatedValue;
		}

		public int Add(CalculatedValue value)
		{
			return base.List.Add(value);
		}

		public void Insert(int index, CalculatedValue value)
		{
			base.List.Insert(index, value);
		}

		public void Remove(CalculatedValue value)
		{
			base.List.Remove(value);
		}

		public bool Contains(CalculatedValue value)
		{
			return base.List.Contains(value);
		}

		public int IndexOf(CalculatedValue value)
		{
			return base.List.IndexOf(value);
		}

		public override string GetElementNameFormat(NamedElement el)
		{
			return base.GetElementNameFormat(el).Replace("CalculatedValue", "");
		}

		public override string GetDefaultElementName(NamedElement el)
		{
			return string.Format(CultureInfo.InvariantCulture, this.GetElementNameFormat(el), 1);
		}

		public override bool IsCorrectType(object value)
		{
			return value is CalculatedValue;
		}

		public override void IsValidNameCheck(string name, NamedElement element)
		{
			base.IsValidNameCheck(name, element);
			if (!(base.parent is ValueBase))
			{
				return;
			}
			if (!((ValueBase)base.parent).Name.Equals(name, StringComparison.Ordinal))
			{
				return;
			}
			throw new NotSupportedException(Utils.SRGetStr("ExceptionListUniqueName", base.GetType().Name));
		}

		public override bool IsUniqueName(string name)
		{
			bool flag = true;
			if (base.parent is ValueBase)
			{
				flag = ((ValueBase)base.parent).Name.Equals(name, StringComparison.Ordinal);
			}
			if (flag)
			{
				return base.IsUniqueName(name);
			}
			return false;
		}
	}
}

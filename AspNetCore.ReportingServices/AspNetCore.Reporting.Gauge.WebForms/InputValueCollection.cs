using System;

namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class InputValueCollection : NamedCollection
	{
		private InputValue this[string name]
		{
			get
			{
				return (InputValue)base.GetByNameCheck(name);
			}
			set
			{
				base.SetByNameCheck(name, value);
			}
		}

		private InputValue this[int index]
		{
			get
			{
				return (InputValue)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		public InputValue this[object obj]
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

		public InputValueCollection(NamedElement parent, CommonElements common)
			: base(parent, common)
		{
			base.elementType = typeof(InputValue);
		}

		public InputValue Add(string name)
		{
			InputValue inputValue = new InputValue();
			inputValue.Name = name;
			this.Add(inputValue);
			return inputValue;
		}

		public int Add(InputValue value)
		{
			return base.List.Add(value);
		}

		public void Insert(int index, InputValue value)
		{
			base.List.Insert(index, value);
		}

		public void Remove(InputValue value)
		{
			base.List.Remove(value);
		}

		public bool Contains(InputValue value)
		{
			return base.List.Contains(value);
		}

		public int IndexOf(InputValue value)
		{
			return base.List.IndexOf(value);
		}
	}
}

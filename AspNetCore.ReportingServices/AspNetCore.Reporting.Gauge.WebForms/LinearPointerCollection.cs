using System;

namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class LinearPointerCollection : NamedCollection
	{
		private LinearPointer this[int index]
		{
			get
			{
				if (index == 0 && base.List.Count == 0)
				{
					this.Add(new LinearPointer());
				}
				return (LinearPointer)base.List[index];
			}
			set
			{
				base.List.Insert(index, value);
			}
		}

		private LinearPointer this[string name]
		{
			get
			{
				return (LinearPointer)base.GetByNameCheck(name);
			}
			set
			{
				base.SetByNameCheck(name, value);
			}
		}

		public LinearPointer this[object obj]
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

		public LinearPointerCollection(NamedElement parent, CommonElements common)
			: base(parent, common)
		{
			base.elementType = typeof(LinearPointer);
		}

		public LinearPointer Add(string name)
		{
			LinearPointer linearPointer = new LinearPointer();
			linearPointer.Name = name;
			this.Add(linearPointer);
			return linearPointer;
		}

		public int Add(LinearPointer value)
		{
			return base.List.Add(value);
		}

		public void Remove(LinearPointer value)
		{
			base.List.Remove(value);
		}

		public bool Contains(LinearPointer value)
		{
			return base.List.Contains(value);
		}

		public void Insert(int index, LinearPointer value)
		{
			base.List.Insert(index, value);
		}

		public int IndexOf(LinearPointer value)
		{
			return base.List.IndexOf(value);
		}

		public override string GetElementNameFormat(NamedElement el)
		{
			return "Pointer{0}";
		}
	}
}

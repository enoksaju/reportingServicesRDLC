using System;

namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class CustomLabelCollection : NamedCollection
	{
		private CustomLabel this[int index]
		{
			get
			{
				return (CustomLabel)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		private CustomLabel this[string name]
		{
			get
			{
				return (CustomLabel)base.GetByNameCheck(name);
			}
			set
			{
				base.SetByNameCheck(name, value);
			}
		}

		public CustomLabel this[object obj]
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

		public CustomLabelCollection(NamedElement parent, CommonElements common)
			: base(parent, common)
		{
			base.elementType = typeof(CustomLabel);
		}

		public CustomLabel Add(string name)
		{
			CustomLabel customLabel = new CustomLabel();
			customLabel.Name = name;
			this.Add(customLabel);
			return customLabel;
		}

		public int Add(CustomLabel value)
		{
			return base.List.Add(value);
		}

		public void Remove(CustomLabel value)
		{
			base.List.Remove(value);
		}

		public bool Contains(CustomLabel value)
		{
			return base.List.Contains(value);
		}

		public void Insert(int index, CustomLabel value)
		{
			base.List.Insert(index, value);
		}

		public int IndexOf(CustomLabel value)
		{
			return base.List.IndexOf(value);
		}

		public override string GetDefaultElementName(NamedElement el)
		{
			return "CustomLabel1";
		}
	}
}

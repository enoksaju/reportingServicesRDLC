using System;

namespace AspNetCore.Reporting.Map.WebForms
{
	public class CustomColorCollection : NamedCollection
	{
		private CustomColor this[int index]
		{
			get
			{
				return (CustomColor)base.List[index];
			}
			set
			{
				base.List.Insert(index, value);
			}
		}

		private CustomColor this[string name]
		{
			get
			{
				return (CustomColor)base.GetByNameCheck(name);
			}
			set
			{
				base.SetByNameCheck(name, value);
			}
		}

		public CustomColor this[object obj]
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

		public CustomColorCollection(NamedElement parent, CommonElements common)
			: base(parent, common)
		{
			base.elementType = typeof(CustomColor);
		}

		public CustomColor Add(string name)
		{
			CustomColor customColor = new CustomColor();
			customColor.Name = name;
			this.Add(customColor);
			return customColor;
		}

		public int Add(CustomColor value)
		{
			return base.List.Add(value);
		}

		public void Remove(CustomColor value)
		{
			base.List.Remove(value);
		}

		public override string GetDefaultElementName(NamedElement el)
		{
			return "CustomColor1";
		}

		public override string GetElementNameFormat(NamedElement el)
		{
			return "CustomColor{0}";
		}

		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete(index, value);
			CustomColor customColor = (CustomColor)value;
		}
	}
}

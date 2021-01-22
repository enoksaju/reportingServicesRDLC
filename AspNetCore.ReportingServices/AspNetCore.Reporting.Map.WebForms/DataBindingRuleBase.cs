using System.Collections.Specialized;
using System.ComponentModel;

namespace AspNetCore.Reporting.Map.WebForms
{
	public class DataBindingRuleBase : NamedElement
	{
		private string dataMember = string.Empty;

		private string bindingField = string.Empty;

		[TypeConverter(typeof(DataMemberConverter))]
		[DefaultValue("")]
		[SRCategory("CategoryAttribute_Data")]
		[SRDescription("DescriptionAttributeDataBindingRuleBase_DataMember")]
		public string DataMember
		{
			get
			{
				return this.dataMember;
			}
			set
			{
				if (this.dataMember != value)
				{
					this.dataMember = value;
					if (this.Common != null)
					{
						this.Common.MapCore.InvalidateDataBinding();
						this.Common.MapCore.Invalidate();
					}
				}
			}
		}

		[DefaultValue("")]
		[TypeConverter(typeof(BindingFieldRuleConverter))]
		public virtual string BindingField
		{
			get
			{
				return this.bindingField;
			}
			set
			{
				if (this.bindingField != value)
				{
					this.bindingField = value;
					if (this.Common != null)
					{
						this.Common.MapCore.InvalidateDataBinding();
						this.Common.MapCore.Invalidate();
					}
				}
			}
		}

		public object DataSource
		{
			get
			{
				if (this.Common != null)
				{
					return this.Common.MapCore.DataSource;
				}
				return null;
			}
		}

		public DataBindingRuleBase()
			: this(null)
		{
		}

		public DataBindingRuleBase(CommonElements common)
			: base(common)
		{
		}

		public virtual void DataBind()
		{
		}

		public virtual void Reset()
		{
			if (this.DataSource != null)
			{
				this.DataMember = DataBindingHelper.GetDataSourceDefaultDataMember(this.DataSource);
			}
			else
			{
				this.DataMember = "";
			}
			this.BindingField = "";
		}

		public virtual void UpdateDataMember(StringCollection dataMembers)
		{
			if (!string.IsNullOrEmpty(this.DataMember) && !dataMembers.Contains(this.DataMember))
			{
				this.Reset();
			}
		}

		public virtual void UpdateDataFields(string dataMember, int dataMemberIndex, StringCollection dataFields)
		{
			if (!(this.DataMember == dataMember))
			{
				if (!string.IsNullOrEmpty(this.DataMember))
				{
					return;
				}
				if (dataMemberIndex != 0)
				{
					return;
				}
			}
			if (!dataFields.Contains(this.BindingField))
			{
				this.BindingField = "";
			}
		}
	}
}

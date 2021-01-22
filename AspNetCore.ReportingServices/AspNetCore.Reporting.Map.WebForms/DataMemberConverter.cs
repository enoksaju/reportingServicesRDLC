using System.Collections.Specialized;
using System.ComponentModel;

namespace AspNetCore.Reporting.Map.WebForms
{
	public class DataMemberConverter : StringConverter
	{
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			StringCollection values = null;
			if (context != null && context.Instance != null && context.Instance is DataBindingRuleBase)
			{
				try
				{
					object obj = null;
					DataBindingRuleBase dataBindingRuleBase = (DataBindingRuleBase)context.Instance;
					obj = dataBindingRuleBase.DataSource;
					if (obj != null)
					{
						values = DataBindingHelper.GetDataSourceDataMembers(obj);
					}
				}
				catch
				{
				}
			}
			return new StandardValuesCollection(values);
		}
	}
}

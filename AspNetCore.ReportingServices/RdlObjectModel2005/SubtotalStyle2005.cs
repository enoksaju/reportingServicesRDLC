using AspNetCore.ReportingServices.RdlObjectModel;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.RdlObjectModel2005
{
	public class SubtotalStyle2005 : Style2005
	{
		private bool m_initialize;

		private Dictionary<string, bool> m_definedPropertiesOnInitialize = new Dictionary<string, bool>();

		public SubtotalStyle2005()
		{
		}

		public SubtotalStyle2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			this.m_initialize = true;
			base.Initialize();
			this.m_initialize = false;
		}

		public override void OnSetObject(int propertyIndex)
		{
			base.OnSetObject(propertyIndex);
			string key = ((Style.Definition.Properties)propertyIndex).ToString();
			if (this.m_initialize)
			{
				this.m_definedPropertiesOnInitialize[key] = true;
			}
			else if (this.m_definedPropertiesOnInitialize.ContainsKey(key))
			{
				this.m_definedPropertiesOnInitialize.Remove(key);
			}
		}

		public bool IsPropertyDefinedOnInitialize(string propertyName)
		{
			return this.m_definedPropertiesOnInitialize.ContainsKey(propertyName);
		}
	}
}

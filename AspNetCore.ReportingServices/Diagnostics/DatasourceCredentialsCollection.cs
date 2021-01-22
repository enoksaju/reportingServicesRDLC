using System;
using System.Collections;
using System.Collections.Specialized;

namespace AspNetCore.ReportingServices.Diagnostics
{
	[Serializable]
	public sealed class DatasourceCredentialsCollection : CollectionBase
	{
		public DatasourceCredentials this[int index]
		{
			get
			{
				return (DatasourceCredentials)base.InnerList[index];
			}
		}

		public DatasourceCredentialsCollection()
		{
		}

		public DatasourceCredentialsCollection(NameValueCollection userNameParams, NameValueCollection userPwdParams)
		{
			for (int i = 0; i < userNameParams.Count; i++)
			{
				string key = userNameParams.GetKey(i);
				string text = userNameParams.Get(i);
				if (text != null && text.Trim().Length != 0)
				{
					string password = userPwdParams[key];
					DatasourceCredentials datasourceCred = new DatasourceCredentials(key, text, password);
					this.Add(datasourceCred);
				}
			}
		}

		public int Add(DatasourceCredentials datasourceCred)
		{
			return base.InnerList.Add(datasourceCred);
		}
	}
}

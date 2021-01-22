using AspNetCore.ReportingServices.Diagnostics;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	public sealed class ConnectionContext
	{
		public string DataSourceType
		{
			get;
			set;
		}

		public ConnectionSecurity ConnectionSecurity
		{
			get;
			set;
		}

		public string ConnectionString
		{
			get;
			set;
		}

		public string DomainName
		{
			get;
			set;
		}

		public string UserName
		{
			get;
			set;
		}

		public bool ImpersonateUser
		{
			get;
			set;
		}

		public string ImpersonateUserName
		{
			get;
			set;
		}

		public SecureStringWrapper Password
		{
			get;
			set;
		}

		public string DecryptedPassword
		{
			get
			{
				if (this.Password != null)
				{
					return this.Password.ToString();
				}
				return string.Empty;
			}
		}

		public ConnectionContext()
		{
			this.ConnectionSecurity = ConnectionSecurity.None;
		}

		public ConnectionKey CreateConnectionKey()
		{
			return new ConnectionKey(this.DataSourceType, this.ConnectionString, this.ConnectionSecurity, this.DomainName, this.UserName, this.ImpersonateUser, this.ImpersonateUserName);
		}
	}
}

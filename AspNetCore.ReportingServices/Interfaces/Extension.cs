using System.Security.Permissions;

namespace AspNetCore.ReportingServices.Interfaces
{ 
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class Extension
	{
		private string m_name;

		private string m_localizedName;

		private bool m_visible;

		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		public string LocalizedName
		{
			get
			{
				return this.m_localizedName;
			}
		}

		public bool Visible
		{
			get
			{
				return this.m_visible;
			}
		}

		public Extension(string name, string localizedName, bool visible)
		{
			this.m_name = name;
			this.m_localizedName = localizedName;
			this.m_visible = visible;
		}
	}
}

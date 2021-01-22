using System.Web;

namespace AspNetCore.Reporting.Common
{
	public sealed class ResourceItem
	{
		private readonly string m_name;

		private readonly string m_debugName;

		private readonly string m_mimeType;

		public string EffectiveName
		{
			get
			{
				
				return this.m_debugName;
			}
		}



		public string MimeType
		{
			get
			{
				return this.m_mimeType;
			}
		}

		public ResourceItem(string name, string debugName, string mimeType)
		{
			this.m_name = name;
			this.m_debugName = debugName;
			this.m_mimeType = mimeType;
		}

		public ResourceItem(string name, string mimeType)
			: this(name, name, mimeType)
		{
		}
	}
}

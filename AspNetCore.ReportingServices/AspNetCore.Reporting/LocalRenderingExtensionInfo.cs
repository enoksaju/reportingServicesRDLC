using AspNetCore.ReportingServices.OnDemandReportRendering;
using System;

namespace AspNetCore.Reporting
{
	[Serializable]
	public sealed class LocalRenderingExtensionInfo
	{
		private string m_name;

		private string m_localizedName;

		private bool m_isVisible;

		private Type m_type;

		private bool m_isExposedExternally = true;

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

		public bool IsVisible
		{
			get
			{
				return this.m_isVisible;
			}
		}

		public bool IsExposedExternally
		{
			get
			{
				return this.m_isExposedExternally;
			}
		}

		public LocalRenderingExtensionInfo(string name, string localizedName, bool isVisible)
		{
			this.m_name = name;
			this.m_localizedName = localizedName;
			this.m_isVisible = isVisible;
		}

		public LocalRenderingExtensionInfo(string name, string localizedName, bool isVisible, Type type, bool isExposedExternally)
			: this(name, localizedName, isVisible)
		{
			this.m_type = type;
			this.m_isExposedExternally = isExposedExternally;
		}

		public IRenderingExtension Instantiate()
		{
			if (this.m_type == null)
			{
				throw new Exception("public Error: Direct instantiation is only available during standalone local mode");
			}
			return (IRenderingExtension)Activator.CreateInstance(this.m_type);
		}
	}
}

using AspNetCore.ReportingServices.Diagnostics.Utilities;
using System;
using System.Collections.Specialized;
using System.Diagnostics;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer
{
	public class DeviceInfo
	{
		private AutoFit m_autoFit = AutoFit.Default;

		private bool m_expandToggles;

		private bool m_fixedPageWidth;

		private bool m_omitHyperlinks;

		private bool m_omitDrillthroughs;

		private NameValueCollection m_rawDeviceInfo;

		public AutoFit AutoFit
		{
			get
			{
				return this.m_autoFit;
			}
			set
			{
				this.m_autoFit = value;
			}
		}

		public bool ExpandToggles
		{
			get
			{
				return this.m_expandToggles;
			}
		}

		public bool FixedPageWidth
		{
			get
			{
				return this.m_fixedPageWidth;
			}
		}

		public bool OmitHyperlinks
		{
			get
			{
				return this.m_omitHyperlinks;
			}
		}

		public bool OmitDrillthroughs
		{
			get
			{
				return this.m_omitDrillthroughs;
			}
		}

		public NameValueCollection RawDeviceInfo
		{
			get
			{
				return this.m_rawDeviceInfo;
			}
		}

		public DeviceInfo(NameValueCollection deviceInfo)
		{
			this.m_rawDeviceInfo = deviceInfo;
			string value = deviceInfo["AutoFit"];
			if (!string.IsNullOrEmpty(value))
			{
				try
				{
					this.AutoFit = (AutoFit)Enum.Parse(((object)this.AutoFit).GetType(), value, true);
				}
				catch (Exception)
				{
					RSTrace.RenderingTracer.Trace(TraceLevel.Verbose, "AutoFit value is not valid");
				}
			}
			this.m_expandToggles = DeviceInfo.ParseBool(deviceInfo["ExpandToggles"], this.m_expandToggles);
			this.m_fixedPageWidth = DeviceInfo.ParseBool(deviceInfo["FixedPageWidth"], this.m_fixedPageWidth);
			this.m_omitHyperlinks = DeviceInfo.ParseBool(deviceInfo["OmitHyperlinks"], this.m_omitHyperlinks);
			this.m_omitDrillthroughs = DeviceInfo.ParseBool(deviceInfo["OmitDrillthroughs"], this.m_omitDrillthroughs);
		}

		private static bool ParseBool(string boolValue, bool defaultValue)
		{
			if (!string.IsNullOrEmpty(boolValue))
			{
				bool result = false;
				if (bool.TryParse(boolValue, out result))
				{
					return result;
				}
			}
			return defaultValue;
		}
	}
}

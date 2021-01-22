using AspNetCore.ReportingServices.Diagnostics.Utilities;
using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace AspNetCore.ReportingServices.Rendering.HtmlRenderer
{
	public abstract class DeviceInfo
	{
		public string ActionScript;

		public bool AllowScript = true;

		public string BookmarkId;

		public bool ExpandContent;

		public bool HasActionScript;

		public bool HTMLFragment;

		public bool OnlyVisibleStyles = true;

		public bool EnablePowerBIFeatures;

		public string FindString;

		public string HtmlPrefixId = "";

		public string JavascriptPrefixId = "";

		public string LinkTarget;

		public string ReplacementRoot;

		public string ResourceStreamRoot;

		public int Section;

		public string StylePrefixId = "a";

		public bool StyleStream;

		public bool OutlookCompat;

		public int Zoom = 100;

		public bool AccessibleTablix;

		public DataVisualizationFitSizing DataVisualizationFitSizing;

		public bool IsBrowserIE = true;

		public bool IsBrowserSafari;

		public bool IsBrowserGeckoEngine;

		public bool IsBrowserIE6Or7StandardsMode;

		public bool IsBrowserIE6;

		public bool IsBrowserIE7;

		public BrowserMode BrowserMode;

		public readonly string BrowserMode_Quirks = "quirks";

		public readonly string BrowserMode_Standards = "standards";

		public string NavigationId;

		public bool ImageConsolidation = true;

		private static readonly Regex m_safeForJavascriptRegex = new Regex("^([a-zA-Z0-9_\\.]+)$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		private NameValueCollection m_rawDeviceInfo;

		public NameValueCollection RawDeviceInfo
		{
			get
			{
				return this.m_rawDeviceInfo;
			}
		}

		public void ParseDeviceInfo(NameValueCollection deviceInfo, NameValueCollection browserCaps)
		{
			this.m_rawDeviceInfo = deviceInfo;
			string text = deviceInfo["HTMLFragment"];
			if (string.IsNullOrEmpty(text))
			{
				text = deviceInfo["MHTMLFragment"];
			}
			if (!string.IsNullOrEmpty(text))
			{
				this.HTMLFragment = DeviceInfo.ParseBool(text, false);
			}
            /*
			object obj = browserCaps[BrowserDetectionUtility.TypeKey];
			if (obj != null && ((string)obj).StartsWith("Netscape", StringComparison.OrdinalIgnoreCase))
			{
				this.IsBrowserIE = false;
			}
            */
			if (this.HTMLFragment)
			{
				string text2 = deviceInfo["PrefixId"];
				if (!string.IsNullOrEmpty(text2))
				{
					this.VerifySafeForJavascript(text2);
					this.HtmlPrefixId = text2;
					this.StylePrefixId = (this.JavascriptPrefixId = "A" + Guid.NewGuid().ToString().Replace("-", ""));
				}
			}
			text = deviceInfo["BookmarkId"];
			if (!string.IsNullOrEmpty(text))
			{
				this.BookmarkId = text;
			}
			text = deviceInfo["JavaScript"];
			if (!string.IsNullOrEmpty(text))
			{
				this.AllowScript = DeviceInfo.ParseBool(text, true);
			}
            /*
			if (this.AllowScript)
			{
				text = browserCaps[BrowserDetectionUtility.JavaScriptKey];
				if (!string.IsNullOrEmpty(text))
				{
					this.AllowScript = DeviceInfo.ParseBool(text, true);
				}
				if (this.AllowScript)
				{
					text = deviceInfo["ActionScript"];
					if (!string.IsNullOrEmpty(text))
					{
						this.VerifySafeForJavascript(text);
						this.ActionScript = text;
						this.HasActionScript = true;
					}
				}
			}
            */
			string text3 = deviceInfo["UserAgent"];
			if (text3 == null && browserCaps != null)
			{
				text3 = browserCaps["UserAgent"];
			}
			if (text3 != null && text3.Contains("MSIE 6.0"))
			{
				this.IsBrowserIE6 = true;
			}
			if (text3 != null && text3.Contains("MSIE 7.0"))
			{
				this.IsBrowserIE7 = true;
			}
            /*
			this.IsBrowserGeckoEngine = BrowserDetectionUtility.IsGeckoBrowserEngine(text3);
			if (this.IsBrowserGeckoEngine)
			{
				this.IsBrowserIE = false;
			}
			else if (BrowserDetectionUtility.IsSafari(text3) || BrowserDetectionUtility.IsChrome(text3))
			{
				this.IsBrowserSafari = true;
				this.IsBrowserIE = false;
			}
            */
			text = deviceInfo["ExpandContent"];
			if (!string.IsNullOrEmpty(text))
			{
				this.ExpandContent = DeviceInfo.ParseBool(text, false);
			}
			text = deviceInfo["EnablePowerBIFeatures"];
			if (!string.IsNullOrEmpty(text))
			{
				this.EnablePowerBIFeatures = DeviceInfo.ParseBool(text, false);
			}
			text = deviceInfo["Section"];
			if (!string.IsNullOrEmpty(text))
			{
				this.Section = DeviceInfo.ParseInt(text, 0);
			}
			text = deviceInfo["FindString"];
			if (!string.IsNullOrEmpty(text) && text.LastIndexOfAny(HTML4Renderer.m_standardLineBreak.ToCharArray()) < 0)
			{
				this.FindString = text.ToUpperInvariant();
			}
			text = deviceInfo["LinkTarget"];
			if (!string.IsNullOrEmpty(text))
			{
				this.VerifySafeForJavascript(text);
				this.LinkTarget = text;
			}
			text = deviceInfo["OutlookCompat"];
			if (!string.IsNullOrEmpty(text))
			{
				this.OutlookCompat = DeviceInfo.ParseBool(text, false);
			}
			text = deviceInfo["AccessibleTablix"];
			if (!string.IsNullOrEmpty(text))
			{
				this.AccessibleTablix = DeviceInfo.ParseBool(text, false);
			}
			text = deviceInfo["StyleStream"];
			if (!string.IsNullOrEmpty(text))
			{
				this.StyleStream = DeviceInfo.ParseBool(text, false);
			}
			this.OnlyVisibleStyles = !this.StyleStream;
			text = deviceInfo["OnlyVisibleStyles"];
			if (!string.IsNullOrEmpty(text))
			{
				this.OnlyVisibleStyles = DeviceInfo.ParseBool(text, this.OnlyVisibleStyles);
			}
			text = deviceInfo["ResourceStreamRoot"];
			if (!string.IsNullOrEmpty(text))
			{
				this.VerifySafeForRoots(text);
				this.ResourceStreamRoot = text;
			}
			text = deviceInfo["StreamRoot"];
			if (!string.IsNullOrEmpty(text))
			{
				this.VerifySafeForRoots(text);
			}
			if (this.IsBrowserIE)
			{
				text = deviceInfo["Zoom"];
				if (!string.IsNullOrEmpty(text))
				{
					this.Zoom = DeviceInfo.ParseInt(text, 100);
				}
			}
			text = deviceInfo["ReplacementRoot"];
			if (!string.IsNullOrEmpty(text))
			{
				this.VerifySafeForRoots(text);
				this.ReplacementRoot = text;
			}
			text = deviceInfo["ImageConsolidation"];
			if (!string.IsNullOrEmpty(text))
			{
				this.ImageConsolidation = DeviceInfo.ParseBool(text, this.ImageConsolidation);
			}
			text = deviceInfo["BrowserMode"];
			if (!string.IsNullOrEmpty(text))
			{
				if (string.Compare(text, this.BrowserMode_Quirks, StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.BrowserMode = BrowserMode.Quirks;
				}
				else if (string.Compare(text, this.BrowserMode_Standards, StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.BrowserMode = BrowserMode.Standards;
					if (this.IsBrowserIE && text3 != null && (this.IsBrowserIE7 || this.IsBrowserIE6))
					{
						this.IsBrowserIE6Or7StandardsMode = true;
					}
				}
			}
			if (this.IsBrowserIE && this.ImageConsolidation && string.IsNullOrEmpty(deviceInfo["ImageConsolidation"]) && this.IsBrowserIE6)
			{
				this.ImageConsolidation = false;
			}
			if (!this.AllowScript)
			{
				this.ImageConsolidation = false;
			}
			text = deviceInfo["DataVisualizationFitSizing"];
			if (!string.IsNullOrEmpty(text) && string.Compare(text, "Approximate", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.DataVisualizationFitSizing = DataVisualizationFitSizing.Approximate;
			}
		}

		public abstract bool IsSupported(string value, bool isTrue, out bool isRelative);

		public virtual void VerifySafeForJavascript(string value)
		{
			if (value != null)
			{
				Match match = DeviceInfo.m_safeForJavascriptRegex.Match(value.Trim());
				if (match.Success)
				{
					return;
				}
				throw new ArgumentOutOfRangeException("value");
			}
		}

		public void VerifySafeForRoots(string value)
		{
			bool flag = default(bool);
			if (this.IsSupported(value, true, out flag) && !flag)
			{
				return;
			}
			int num = value.IndexOf(':');
			int num2 = value.IndexOf('?');
			int num3 = value.IndexOf('&');
			if (num == -1 && num3 == -1)
			{
				return;
			}
			if (num2 == -1 && (num != -1 || num3 != -1))
			{
				throw new ArgumentOutOfRangeException("value");
			}
			if (num != -1 && num < num2)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			if (num3 == -1)
			{
				return;
			}
			if (num3 >= num2)
			{
				return;
			}
			throw new ArgumentOutOfRangeException("value");
		}

		private static bool ParseBool(string boolValue, bool defaultValue)
		{
			bool result = default(bool);
			if (bool.TryParse(boolValue, out result))
			{
				return result;
			}
			return defaultValue;
		}

		private static int ParseInt(string intValue, int defaultValue)
		{
			int result = default(int);
			if (int.TryParse(intValue, out result))
			{
				return result;
			}
			return defaultValue;
		}
	}
}

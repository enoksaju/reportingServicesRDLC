using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace AspNetCore.ReportingServices.Rendering.RPLProcessing
{
	[CompilerGenerated]
	public class RPLRes
	{
		[CompilerGenerated]
		public class Keys
		{
			public const string InvalidRPLToken = "InvalidRPLToken";

			public const string UnsupportedRPLVersion = "UnsupportedRPLVersion";

			public const string IncompleteRPLVersion = "IncompleteRPLVersion";

			public const string MismatchRPLVersion = "MismatchRPLVersion";

			private static ResourceManager resourceManager = new ResourceManager(typeof(RPLRes).FullName, typeof(RPLRes).Module.Assembly);

			private static CultureInfo _culture = null;

			public static CultureInfo Culture
			{
				get
				{
					return Keys._culture;
				}
				set
				{
					Keys._culture = value;
				}
			}

			private Keys()
			{
			}

			public static string GetString(string key)
			{
				return Keys.resourceManager.GetString(key, Keys._culture);
			}

			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, Keys.resourceManager.GetString(key, Keys._culture), arg0);
			}

			public static string GetString(string key, object arg0, object arg1)
			{
				return string.Format(CultureInfo.CurrentCulture, Keys.resourceManager.GetString(key, Keys._culture), arg0, arg1);
			}
		}

		public static CultureInfo Culture
		{
			get
			{
				return Keys.Culture;
			}
			set
			{
				Keys.Culture = value;
			}
		}

		protected RPLRes()
		{
		}

		public static string InvalidRPLToken(string rplItemType, string hexToken)
		{
			return Keys.GetString("InvalidRPLToken", rplItemType, hexToken);
		}

		public static string UnsupportedRPLVersion(string actualVersion, string expectedVersion)
		{
			return Keys.GetString("UnsupportedRPLVersion", actualVersion, expectedVersion);
		}

		public static string IncompleteRPLVersion(string length)
		{
			return Keys.GetString("IncompleteRPLVersion", length);
		}

		public static string MismatchRPLVersion(string version1, string version2)
		{
			return Keys.GetString("MismatchRPLVersion", version1, version2);
		}
	}
}

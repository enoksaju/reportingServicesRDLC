using AspNetCore.ReportingServices.Diagnostics.Utilities;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace AspNetCore.ReportingServices.Diagnostics
{
	internal static class DataProtectionLocal
	{
		private sealed class DataProtectionLocalInstance : IDataProtection
		{
			public byte[] ProtectData(string unprotectedData, string tag)
			{
				if (unprotectedData == null)
				{
					return null;
				}
				byte[] bytes = Encoding.Unicode.GetBytes(unprotectedData);
				return DataProtectionLocal.LocalProtectData(bytes);
			}

			public string UnprotectDataToString(byte[] protectedData, string tag)
			{
				if (protectedData == null)
				{
					return null;
				}
				byte[] array = DataProtectionLocal.LocalUnprotectData(protectedData);
				if (array == null)
				{
					return null;
				}
				if (protectedData.Length == 0)
				{
					return string.Empty;
				}
				return Encoding.Unicode.GetString(array);
			}
		}

		private static int m_dwProtectionFlags = 4;

		private static IDataProtection m_dpInstance;

		public static ProtectionMode GlobalProtectionMode
		{
			set
			{
				switch (value)
				{
				case ProtectionMode.LocalSystemEncryption:
					DataProtectionLocal.m_dwProtectionFlags = 4;
					break;
				case ProtectionMode.CurrentUserEncryption:
					DataProtectionLocal.m_dwProtectionFlags = 0;
					break;
				}
			}
		}

		public static IDataProtection Instance
		{
			[DebuggerStepThrough]
			get
			{
				if (DataProtectionLocal.m_dpInstance == null)
				{
					DataProtectionLocal.m_dpInstance = new DataProtectionLocalInstance();
				}
				return DataProtectionLocal.m_dpInstance;
			}
		}

		public static string LocalProtectData(string data)
		{
			if (data == null)
			{
				return null;
			}
			byte[] bytes = Encoding.Unicode.GetBytes(data);
			byte[] array = DataProtectionLocal.LocalProtectData(bytes);
			string result = null;
			if (array != null)
			{
				result = Convert.ToBase64String(array);
			}
			return result;
		}

		public static string LocalUnprotectData(string data)
		{
			if (data == null)
			{
				return null;
			}
			byte[] data2 = Convert.FromBase64String(data);
			byte[] array = DataProtectionLocal.LocalUnprotectData(data2);
			string text = null;
			if (array != null)
			{
				text = Encoding.Unicode.GetString(array);
				if (text != null && text.Length > 0 && text[text.Length - 1] == '\0')
				{
					text = text.Remove(text.Length - 1);
				}
			}
			return text;
		}

		public static byte[] LocalProtectData(byte[] data)
		{
			if (data == null)
			{
				return null;
			}
			return DataProtectionLocal.ProtectData(data, 1 | DataProtectionLocal.m_dwProtectionFlags);
		}

		public static byte[] LocalUnprotectData(byte[] data)
		{
			if (data == null)
			{
				return null;
			}
			return DataProtectionLocal.UnprotectData(data, 1);
		}

		private static byte[] UnprotectData(byte[] data, int dwFlags)
		{
			byte[] array = null;
			SafeCryptoBlobIn safeCryptoBlobIn = null;
			SafeCryptoBlobOut safeCryptoBlobOut = null;
			try
			{
				safeCryptoBlobIn = new SafeCryptoBlobIn(data);
				safeCryptoBlobOut = new SafeCryptoBlobOut();
				if (NativeMethods.CryptUnprotectData(safeCryptoBlobIn, null, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, dwFlags, safeCryptoBlobOut))
				{
					NativeMethods.DATA_BLOB blob = safeCryptoBlobOut.Blob;
					array = new byte[blob.cbData];
					Marshal.Copy(blob.pbData, array, 0, blob.cbData);
					safeCryptoBlobOut.ZeroBuffer();
					return array;
				}
				int lastWin32Error = Marshal.GetLastWin32Error();
				string additionalTraceMessage = string.Format(CultureInfo.InvariantCulture, "CryptUnprotectData: Win32 error:{0}", lastWin32Error);
				throw new ServerConfigurationErrorException(additionalTraceMessage);
			}
			finally
			{
				if (safeCryptoBlobIn != null)
				{
					safeCryptoBlobIn.Close();
				}
				if (safeCryptoBlobOut != null)
				{
					safeCryptoBlobOut.Close();
				}
			}
		}

		private static byte[] ProtectData(byte[] data, int dwFlags)
		{
			string szDataDescr = "Default";
			byte[] array = null;
			SafeCryptoBlobIn safeCryptoBlobIn = null;
			SafeCryptoBlobOut safeCryptoBlobOut = null;
			try
			{
				safeCryptoBlobIn = new SafeCryptoBlobIn(data);
				safeCryptoBlobOut = new SafeCryptoBlobOut();
				bool flag = NativeMethods.CryptProtectData(safeCryptoBlobIn, szDataDescr, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, dwFlags, safeCryptoBlobOut);
				safeCryptoBlobIn.ZeroBuffer();
				if (flag)
				{
					NativeMethods.DATA_BLOB blob = safeCryptoBlobOut.Blob;
					array = new byte[blob.cbData];
					Marshal.Copy(blob.pbData, array, 0, blob.cbData);
					return array;
				}
				int lastWin32Error = Marshal.GetLastWin32Error();
				string additionalTraceMessage = string.Format(CultureInfo.InvariantCulture, "CryptProtectData: Win32 error:{0}", lastWin32Error);
				throw new ServerConfigurationErrorException(additionalTraceMessage);
			}
			finally
			{
				if (safeCryptoBlobIn != null)
				{
					safeCryptoBlobIn.Close();
				}
				if (safeCryptoBlobOut != null)
				{
					safeCryptoBlobOut.Close();
				}
			}
		}
	}
}

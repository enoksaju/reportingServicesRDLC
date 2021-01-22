using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace AspNetCore.ReportingServices.RdlObjectModel.Serialization
{
	public class XmlCustomFormatter
	{
		private static string[] allDateTimeFormats = new string[37]
		{
			"yyyy",
			"---dd",
			"--MM-dd",
			"yyyy-MM",
			"yyyy-MM-dd",
			"HH:mm:ss",
			"HH:mm:ss.f",
			"HH:mm:ss.ff",
			"HH:mm:ss.fff",
			"HH:mm:ss.ffff",
			"HH:mm:ss.fffff",
			"HH:mm:ss.ffffff",
			"HH:mm:ss.fffffff",
			"HH:mm:sszzzzzz",
			"HH:mm:ss.fzzzzzz",
			"HH:mm:ss.ffzzzzzz",
			"HH:mm:ss.fffzzzzzz",
			"HH:mm:ss.ffffzzzzzz",
			"HH:mm:ss.fffffzzzzzz",
			"HH:mm:ss.ffffffzzzzzz",
			"HH:mm:ss.fffffffzzzzzz",
			"yyyy-MM-ddTHH:mm:ss",
			"yyyy-MM-ddTHH:mm:ss.f",
			"yyyy-MM-ddTHH:mm:ss.ff",
			"yyyy-MM-ddTHH:mm:ss.fff",
			"yyyy-MM-ddTHH:mm:ss.ffff",
			"yyyy-MM-ddTHH:mm:ss.fffff",
			"yyyy-MM-ddTHH:mm:ss.ffffff",
			"yyyy-MM-ddTHH:mm:ss.fffffff",
			"yyyy-MM-ddTHH:mm:sszzzzzz",
			"yyyy-MM-ddTHH:mm:ss.fzzzzzz",
			"yyyy-MM-ddTHH:mm:ss.ffzzzzzz",
			"yyyy-MM-ddTHH:mm:ss.fffzzzzzz",
			"yyyy-MM-ddTHH:mm:ss.ffffzzzzzz",
			"yyyy-MM-ddTHH:mm:ss.fffffzzzzzz",
			"yyyy-MM-ddTHH:mm:ss.ffffffzzzzzz",
			"yyyy-MM-ddTHH:mm:ss.fffffffzzzzzz"
		};

		private static string[] allDateFormats = new string[1]
		{
			"yyyy-MM-dd"
		};

		private static string[] allTimeFormats = new string[16]
		{
			"HH:mm:ss",
			"HH:mm:ss.f",
			"HH:mm:ss.ff",
			"HH:mm:ss.fff",
			"HH:mm:ss.ffff",
			"HH:mm:ss.fffff",
			"HH:mm:ss.ffffff",
			"HH:mm:ss.fffffff",
			"HH:mm:sszzzzzz",
			"HH:mm:ss.fzzzzzz",
			"HH:mm:ss.ffzzzzzz",
			"HH:mm:ss.fffzzzzzz",
			"HH:mm:ss.ffffzzzzzz",
			"HH:mm:ss.fffffzzzzzz",
			"HH:mm:ss.ffffffzzzzzz",
			"HH:mm:ss.fffffffzzzzzz"
		};

		public static string FromDate(DateTime value)
		{
			return XmlConvert.ToString(value, "yyyy-MM-dd");
		}

		public static string FromTime(DateTime value)
		{
			return XmlConvert.ToString(value, "HH:mm:ss.fffffffzzzzzz");
		}

		public static string FromDateTime(DateTime value)
		{
			return XmlConvert.ToString(value, "yyyy-MM-ddTHH:mm:ss.fffffffzzzzzz");
		}

		public static string FromChar(char value)
		{
			return XmlConvert.ToString((ushort)value);
		}

		public static string FromXmlNmToken(string nmToken)
		{
			return XmlConvert.DecodeName(nmToken);
		}

		public static string FromByteArrayBase64(byte[] value)
		{
			if (value == null)
			{
				return null;
			}
			return Convert.ToBase64String(value);
		}

		public static string FromEnum(long val, string[] vals, long[] ids)
		{
			long num = val;
			StringBuilder stringBuilder = new StringBuilder();
			int num2 = -1;
			for (int i = 0; i < ids.Length; i++)
			{
				if (ids[i] == 0)
				{
					num2 = i;
				}
				else
				{
					if (val == 0)
					{
						break;
					}
					if ((ids[i] & num) == ids[i])
					{
						if (stringBuilder.Length != 0)
						{
							stringBuilder.Append(" ");
						}
						stringBuilder.Append(vals[i]);
						val &= ~ids[i];
					}
				}
			}
			if (val != 0)
			{
				return XmlConvert.ToString(num);
			}
			if (stringBuilder.Length == 0 && num2 >= 0)
			{
				stringBuilder.Append(vals[num2]);
			}
			return stringBuilder.ToString();
		}

		public static DateTime ToDateTime(string value)
		{
			return XmlCustomFormatter.ToDateTime(value, XmlCustomFormatter.allDateTimeFormats);
		}

		public static DateTime ToDateTime(string value, string[] formats)
		{
			return XmlConvert.ToDateTime(value, formats);
		}

		public static DateTime ToDate(string value)
		{
			return XmlCustomFormatter.ToDateTime(value, XmlCustomFormatter.allDateFormats);
		}

		public static DateTime ToTime(string value)
		{
			return XmlCustomFormatter.ToDateTime(value, XmlCustomFormatter.allTimeFormats);
		}

		public static char ToChar(string value)
		{
			return (char)XmlConvert.ToUInt16(value);
		}

		public static string ToXmlNmToken(string value)
		{
			return XmlConvert.EncodeNmToken(value);
		}

		public static byte[] ToByteArrayBase64(string value)
		{
			if (value == null)
			{
				return null;
			}
			value = value.Trim();
			if (value.Length == 0)
			{
				return new byte[0];
			}
			return Convert.FromBase64String(value);
		}

		public static long ToEnum(string val, Hashtable vals, string typeName)
		{
			long num = 0L;
			string[] array = val.Split(null);
			for (int i = 0; i < array.Length; i++)
			{
				object obj = vals[array[i]];
				if (obj == null)
				{
					throw new Exception("UnknownConstant");
				}
				num |= (long)obj;
			}
			return num;
		}
	}
}

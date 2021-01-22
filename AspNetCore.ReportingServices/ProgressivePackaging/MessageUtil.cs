using System.IO;
using System.Text;

namespace AspNetCore.ReportingServices.ProgressivePackaging
{
	public static class MessageUtil
	{
		public static readonly Encoding StringEncoding;

		static MessageUtil()
		{
			MessageUtil.StringEncoding = Encoding.UTF8;
		}

		public static string[] ReadStringArray(BinaryReader reader)
		{
			string[] array = new string[reader.ReadInt32()];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = reader.ReadString();
			}
			return array;
		}

		public static void WriteStringArray(BinaryWriter writer, string[] value)
		{
			writer.Write(value.Length);
			for (int i = 0; i < value.Length; i++)
			{
				writer.Write(value[i]);
			}
		}

		public static void WriteByteArray(BinaryWriter writer, byte[] value, int offset, int length)
		{
			writer.Write(length);
			writer.Write(value, offset, length);
		}
	}
}

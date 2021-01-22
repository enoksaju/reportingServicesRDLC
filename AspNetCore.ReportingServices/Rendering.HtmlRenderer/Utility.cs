using System;
using System.Globalization;
using System.IO;


namespace AspNetCore.ReportingServices.Rendering.HtmlRenderer
{
	public class Utility
	{
		public const int TextBufferSize = 16384;

		private Utility()
		{
		}

		public static void CopyStream(Stream source, Stream sink)
		{
			byte[] array = new byte[4096];
			for (int num = source.Read(array, 0, array.Length); num != 0; num = source.Read(array, 0, array.Length))
			{
				sink.Write(array, 0, num);
			}
		}

		public static string MmToPxAsString(double size)
		{
			return Convert.ToInt64(size * 3.7795275590551185).ToString(CultureInfo.InvariantCulture);
		}

		public static long MMToPx(double size)
		{
			return Convert.ToInt64(size * 3.7795275590551185);
		}

		public static BufferedStream CreateBufferedStream(TextWriter sourceWriter)
		{
			return Utility.CreateBufferedStream(((StreamWriter)sourceWriter).BaseStream);
		}

		public static BufferedStream CreateBufferedStream(Stream sourceStream)
		{
			return new BufferedStream(sourceStream, 16384);
		}
	}
}

using System.IO;

namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.ExcelGenerator
{
	public static class ExcelGeneratorConstants
	{
		public delegate Stream CreateTempStream(string name);

		public const int EASTASIACHAR_RANGE1_START = 4352;

		public const int EASTASIACHAR_RANGE1_END = 4607;

		public const int EASTASIACHAR_RANGE2_START = 11904;

		public const int EASTASIACHAR_RANGE2_END = 55215;

		public const int EASTASIACHAR_RANGE3_START = 63744;

		public const int EASTASIACHAR_RANGE3_END = 65519;

		public const int EASTASIACHAR_RANGE4_START = 55296;

		public const int EASTASIACHAR_RANGE4_END = 56319;

		public const int MAX_LENGTH_SPREADSHEET_NAME = 31;

		public const string UNSUPPORTED_WORKSHEETNAME_CHARS = "[]:?*/\\";

		public const int MAX_COLORS_IN_PALETTE = 56;

		public const string BMP_EXTENSION = "bmp";

		public const string JPEG_EXTENSION = "jpg";

		public const string GIF_EXTENSION = "gif";

		public const string PNG_EXTENSION = "png";

		public const int MAX_STRING_LENGTH = 32767;

		public const int STREAM_COPY_BUFFER_SIZE = 1024;

		public const int CONTROL_CHARACTER_RANGE_END = 31;
	}
}

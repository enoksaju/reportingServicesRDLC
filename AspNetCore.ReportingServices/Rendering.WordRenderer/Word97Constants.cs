namespace AspNetCore.ReportingServices.Rendering.WordRenderer
{
	public class Word97Constants
	{
		public const int TcSize = 20;

		public const byte TcFirstMerge = 1;

		public const byte TcMerge = 2;

		public const byte TcVertMerge = 32;

		public const byte TcVertRestart = 64;

		public const byte TextPieceTableMarker = 2;

		public const ushort TextPieceDescriptor = 64;

		public const ushort DefaultStyleCount = 17;

		public const short DefaultSepxSize = 24;

		public const int WordPageSize = 512;

		public const float InchInTwips = 1440f;

		public const int NumHdrFtrOffsets = 14;

		public const int NumHdrFtrOffsetsPerSection = 6;

		public const byte PaddingTop = 1;

		public const byte PaddingLeft = 2;

		public const byte PaddingBottom = 4;

		public const byte PaddingRight = 8;

		public const byte PaddingTwips = 3;

		public const int OddHeader = 8;

		public const int EvenHeader = 7;

		public const int FirstHeader = 11;

		public const int OddFooter = 10;

		public const int EvenFooter = 9;

		public const int FirstFooter = 12;

		public const string InlineImgCode = "\u0001";

		public const int LeftJc = 0;

		public const int CenterJc = 1;

		public const int RightJc = 2;

		public const float InchInMm = 25.4f;

		public const int WordMaxColumns = 63;

		public const float WordMaxWidth = 22f;

		public const ushort WordMaxWidthTwips = 31680;

		public const float WordMaxWidthMM = 558.8f;

		public const int WorddmOrientPortrait = 1;

		public const int WorddmOrientLandscape = 2;

		public const string Word97ClsId = "00020906-0000-0000-c000-000000000046";
	}
}

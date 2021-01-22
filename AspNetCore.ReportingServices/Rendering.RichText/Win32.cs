using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Security;

namespace AspNetCore.ReportingServices.Rendering.RichText
{
	[SuppressUnmanagedCodeSecurity]
	public sealed class Win32
	{
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct BITMAPINFOHEADER
		{
			public uint biSize;

			public int biWidth;

			public int biHeight;

			public short biPlanes;

			public short biBitCount;

			public uint biCompression;

			public uint biSizeImage;

			public int biXPelsPerMeter;

			public int biYPelsPerMeter;

			public uint biClrUsed;

			public uint biClrImportant;

			public BITMAPINFOHEADER(int widthPX, int heightPX, int dpiX, int dpiY)
			{
				this.biSize = 40u;
				this.biWidth = widthPX;
				this.biHeight = heightPX;
				this.biPlanes = 1;
				this.biBitCount = 24;
				this.biCompression = 0u;
				this.biSizeImage = 0u;
				this.biXPelsPerMeter = (int)((double)dpiX / 0.0254);
				this.biYPelsPerMeter = (int)((double)dpiY / 0.0254);
				this.biClrUsed = 0u;
				this.biClrImportant = 0u;
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct SCRIPT_FONTPROPERTIES
		{
			public int cBytes;

			public short wgBlank;

			public short wgDefault;

			public short wgInvalid;

			public short wgKashida;

			public int iKashidaWidth;
		}

		public struct POINT
		{
			public int x;

			public int y;
		}

		public struct TEXTMETRIC
		{
			public int tmHeight;

			public int tmAscent;

			public int tmDescent;

			public int tminternalLeading;

			public int tmExternalLeading;

			public int tmAveCharWidth;

			public int tmMaxCharWidth;

			public int tmWeight;

			public int tmOverhang;

			public int tmDigitizedAspectX;

			public int tmDigitizedAspectY;

			public char tmFirstChar;

			public char tmLastChar;

			public char tmDefaultChar;

			public char tmBreakChar;

			public byte tmItalic;

			public byte tmUnderlined;

			public byte tmStruckOut;

			public byte tmPitchAndFamily;

			public byte tmCharSet;
		}

		public struct LOGFONT
		{
			public int lfHeight;

			public int lfWidth;

			public int lfEscapement;

			public int lfOrientation;

			public int lfWeight;

			public byte lfItalic;

			public byte lfUnderline;

			public byte lfStrikeOut;

			public byte lfCharSet;

			public byte lfOutPrecision;

			public byte lfClipPrecision;

			public byte lfQuality;

			public byte lfPitchAndFamily;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string lfFaceName;
		}

		public struct LOGBRUSH
		{
			public uint lbStyle;

			public uint lbColor;

			public int lbHatch;
		}

		public struct ABCFloat
		{
			public float abcfA;

			public float abcfB;

			public float abcfC;

			public ABCFloat(float a, float b, float c)
			{
				this.abcfA = a;
				this.abcfB = b;
				this.abcfC = c;
			}
		}

		public struct XFORM
		{
			public float eM11;

			public float eM12;

			public float eM21;

			public float eM22;

			public float eDx;

			public float eDy;

			public static XFORM Identity
			{
				get
				{
					XFORM result = default(XFORM);
					result.eM11 = 1f;
					result.eM22 = 1f;
					return result;
				}
			}

			public XFORM(Matrix matrix, GraphicsUnit units, float dpi)
			{
				this.eM11 = matrix.Elements[0];
				this.eM12 = matrix.Elements[1];
				this.eM21 = matrix.Elements[2];
				this.eM22 = matrix.Elements[3];
				this.eDx = 0f;
				this.eDy = 0f;
				switch (units)
				{
				case GraphicsUnit.Document:
					this.eDx = (float)(matrix.Elements[4] / 300.0 * dpi);
					this.eDy = (float)(matrix.Elements[5] / 300.0 * dpi);
					break;
				case GraphicsUnit.Inch:
					this.eDx = matrix.Elements[4] * dpi;
					this.eDy = matrix.Elements[5] * dpi;
					break;
				case GraphicsUnit.Millimeter:
					this.eDx = (float)(matrix.Elements[4] / 25.399999618530273 * dpi);
					this.eDy = (float)(matrix.Elements[5] / 25.399999618530273 * dpi);
					break;
				case GraphicsUnit.Point:
					this.eDx = (float)(matrix.Elements[4] / 72.0 * dpi);
					this.eDy = (float)(matrix.Elements[5] / 72.0 * dpi);
					break;
				default:
					this.eDx = matrix.Elements[4];
					this.eDy = matrix.Elements[5];
					break;
				}
			}

			public void Transform(ref RectangleF rect)
			{
				PointF location = rect.Location;
				PointF pointF = new PointF(rect.Right, rect.Bottom);
				this.Transform(ref location);
				this.Transform(ref pointF);
				rect = RectangleF.FromLTRB(location.X, location.Y, pointF.X, pointF.Y);
			}

			public void Transform(ref PointF pt)
			{
				pt = new PointF(pt.X * this.eM11 + pt.Y * this.eM12 + this.eDx, pt.X * this.eM21 + pt.Y * this.eM22 + this.eDy);
			}

			public void Transform(ref Rectangle rect)
			{
				Point location = rect.Location;
				Point point = new Point(rect.Right, rect.Bottom);
				this.Transform(ref location);
				this.Transform(ref point);
				rect = System.Drawing.Rectangle.FromLTRB(location.X, location.Y, point.X, point.Y);
			}

			public void Transform(ref Point pt)
			{
				pt = new Point((int)((float)pt.X * this.eM11 + (float)pt.Y * this.eM12 + this.eDx), (int)((float)pt.X * this.eM21 + (float)pt.Y * this.eM22 + this.eDy));
			}
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct OutlineTextMetric
		{
			public uint otmSize;

			public int tmHeight;

			public int tmAscent;

			public int tmDescent;

			public int tmInternalLeading;

			public int tmExternalLeading;

			public int tmAveCharWidth;

			public int tmMaxCharWidth;

			public int tmWeight;

			public int tmOverhang;

			public int tmDigitizedAspectX;

			public int tmDigitizedAspectY;

			public char tmFirstChar;

			public char tmLastChar;

			public char tmDefaultChar;

			public char tmBreakChar;

			public byte tmItalic;

			public byte tmUnderlined;

			public byte tmStruckOut;

			public byte tmPitchAndFamily;

			public byte tmCharSet;

			public byte otmFiller;

			public byte bFamilyType;

			public byte bSerifStyle;

			public byte bWeight;

			public byte bProportion;

			public byte bContrast;

			public byte bStrokeVariation;

			public byte bArmStyle;

			public byte bLetterform;

			public byte bMidline;

			public byte bXHeight;

			public uint otmfsSelection;

			public uint otmfsType;

			public int otmsCharSlopeRise;

			public int otmsCharSlopeRun;

			public int otmItalicAngle;

			public uint otmEMSquare;

			public int otmAscent;

			public int otmDescent;

			public uint otmLineGap;

			public uint otmsCapEmHeight;

			public uint otmsXHeight;

			public int left;

			public int top;

			public int right;

			public int bottom;

			public int otmMacAscent;

			public int otmMacDescent;

			public uint otmMacLineGap;

			public uint otmusMinimumPPEM;

			public int otmptSubscriptSizeX;

			public int otmptSubscriptSizeY;

			public int otmptSubscriptOffsetX;

			public int otmptSubscriptOffsetY;

			public int otmptSuperscriptSizeX;

			public int otmptSuperscriptSizeY;

			public int otmptSuperscriptOffsetX;

			public int otmptSuperscriptOffsetY;

			public uint otmsStrikeoutSize;

			public int otmsStrikeoutPosition;

			public int otmsUnderscoreSize;

			public int otmsUnderscorePosition;

			public string otmpFamilyName;

			public string otmpFaceName;

			public string otmpStyleName;

			public string otmpFullName;

			public OutlineTextMetric(string intialValue)
			{
				this.otmSize = 0u;
				this.tmHeight = 0;
				this.tmAscent = 0;
				this.tmDescent = 0;
				this.tmInternalLeading = 0;
				this.tmExternalLeading = 0;
				this.tmAveCharWidth = 0;
				this.tmMaxCharWidth = 0;
				this.tmWeight = 0;
				this.tmOverhang = 0;
				this.tmDigitizedAspectX = 0;
				this.tmDigitizedAspectY = 0;
				this.tmFirstChar = ' ';
				this.tmLastChar = ' ';
				this.tmDefaultChar = ' ';
				this.tmBreakChar = ' ';
				this.tmItalic = 0;
				this.tmUnderlined = 0;
				this.tmStruckOut = 0;
				this.tmPitchAndFamily = 0;
				this.tmCharSet = 0;
				this.otmFiller = 0;
				this.bFamilyType = 0;
				this.bSerifStyle = 0;
				this.bWeight = 0;
				this.bProportion = 0;
				this.bContrast = 0;
				this.bStrokeVariation = 0;
				this.bArmStyle = 0;
				this.bLetterform = 0;
				this.bMidline = 0;
				this.bXHeight = 0;
				this.otmfsSelection = 0u;
				this.otmfsType = 0u;
				this.otmsCharSlopeRise = 0;
				this.otmsCharSlopeRun = 0;
				this.otmItalicAngle = 0;
				this.otmEMSquare = 0u;
				this.otmAscent = 0;
				this.otmDescent = 0;
				this.otmLineGap = 0u;
				this.otmsCapEmHeight = 0u;
				this.otmsXHeight = 0u;
				this.left = 0;
				this.top = 0;
				this.right = 0;
				this.bottom = 0;
				this.otmMacAscent = 0;
				this.otmMacDescent = 0;
				this.otmMacLineGap = 0u;
				this.otmusMinimumPPEM = 0u;
				this.otmptSubscriptSizeX = 0;
				this.otmptSubscriptSizeY = 0;
				this.otmptSubscriptOffsetX = 0;
				this.otmptSubscriptOffsetY = 0;
				this.otmptSuperscriptSizeX = 0;
				this.otmptSuperscriptSizeY = 0;
				this.otmptSuperscriptOffsetX = 0;
				this.otmptSuperscriptOffsetY = 0;
				this.otmsStrikeoutSize = 0u;
				this.otmsStrikeoutPosition = 0;
				this.otmsUnderscoreSize = 0;
				this.otmsUnderscorePosition = 0;
				this.otmpFamilyName = intialValue;
				this.otmpFaceName = "";
				this.otmpStyleName = "";
				this.otmpFullName = "";
			}
		}

		public const int HORZSIZE = 4;

		public const int VERTSIZE = 6;

		public const int HORZRES = 8;

		public const int VERTRES = 10;

		public const int LOGPIXELSX = 88;

		public const int LOGPIXELSY = 90;

		public const int S_FALSE = 1;

		public const int ETO_CLIPPED = 4;

		public const int ETO_OPAQUE = 2;

		public const int ETO_RTLREADING = 128;

		public const int USP_E_SCRIPT_NOT_IN_FONT = -2147220992;

		public const int E_OUTOFMEMORY = -2147024882;

		public const int E_PENDING = -2147483638;

		public const int SCRIPT_UNDEFINED = 0;

		public const int SIC_COMPLEX = 1;

		public const int SIC_ASCIIDIGIT = 2;

		public const int SIC_NEUTRAL = 4;

		public const int GM_COMPATIBLE = 1;

		public const int GM_ADVANCED = 2;

		public const int PS_GEOMETRIC = 65536;

		public const int PS_COSMETIC = 0;

		public const int PS_ALTERNATE = 8;

		public const int PS_SOLID = 0;

		public const int PS_DASH = 1;

		public const int PS_DOT = 2;

		public const int PS_DASHDOT = 3;

		public const int PS_DASHDOTDOT = 4;

		public const int PS_NULL = 5;

		public const int PS_USERSTYLE = 7;

		public const int PS_INSIDEFRAME = 6;

		public const int PS_ENDCAP_ROUND = 0;

		public const int PS_ENDCAP_SQUARE = 256;

		public const int PS_ENDCAP_FLAT = 512;

		public const int PS_JOIN_BEVEL = 4096;

		public const int PS_JOIN_MITER = 8192;

		public const int PS_JOIN_ROUND = 0;

		public const int BS_SOLID = 0;

		public const int BS_NULL = 1;

		public const int BS_HOLLOW = 1;

		public const int BS_HATCHED = 2;

		public const int BS_PATTERN = 3;

		public const int BS_INDEXED = 4;

		public const int BS_DIBPATTERN = 5;

		public const int BS_DIBPATTERNPT = 6;

		public const int BS_PATTERN8X8 = 7;

		public const int BS_DIBPATTERN8X8 = 8;

		public const int BS_MONOPATTERN = 9;

		public const int HS_HORIZONTAL = 0;

		public const int HS_VERTICAL = 1;

		public const int HS_FDIAGONAL = 2;

		public const int HS_BDIAGONAL = 3;

		public const int HS_CROSS = 4;

		public const int HS_DIAGCROSS = 5;

		public const int MM_ANISOTROPIC = 8;

		public const int MM_HIENGLISH = 5;

		public const int MM_HIMETRIC = 3;

		public const int MM_ISOTROPIC = 7;

		public const int MM_LOENGLISH = 4;

		public const int MM_LOMETRIC = 2;

		public const int MM_TEXT = 1;

		public const int MM_TWIPS = 6;

		public const int TextRenderingHintSystemDefault = 0;

		public const int TextRenderingHintSingleBitPerPixelGridFit = 1;

		public const int TextRenderingHintSingleBitPerPixel = 2;

		public const int TextRenderingHintAntiAliasGridFit = 3;

		public const int TextRenderingHintAntiAlias = 4;

		public const int TextRenderingHintClearTypeGridFit = 5;

		public const uint DEFAULT_QUALITY = 0u;

		public const uint DRAFT_QUALITY = 1u;

		public const uint PROOF_QUALITY = 2u;

		public const uint NONANTIALIASED_QUALITY = 3u;

		public const uint ANTIALIASED_QUALITY = 4u;

		public const uint CLEARTYPE_QUALITY = 5u;

		public const uint CLEARTYPE_NATURAL_QUALITY = 6u;

		public const uint BI_RGB = 0u;

		public const double METER_PER_INCH = 0.0254;

		public const int TA_BASELINE = 24;

		public const int TA_RTLREADING = 256;

		public const int TRANSPARENT = 1;

		public const int OPAQUE = 2;

		public const int OUT_TT_PRECIS = 4;

		public const int OUT_TT_ONLY_PRECIS = 7;

		public const int OUT_DEFAULT_PRECIS = 0;

		public const int LF_FACESIZE = 32;

		public const int LF_FULLFACESIZE = 64;

		public const int DEFAULT_CHARSET = 1;

		public const int SYMBOL_CHARSET = 2;

		private Win32()
		{
		}

		public static bool Failed(int hr)
		{
			return hr < 0;
		}

		public static bool Succeeded(int hr)
		{
			return hr >= 0;
		}

		[DllImport("usp10.dll")]
		public static extern int ScriptXtoCP(int iX, int cChars, int cGlyphs, [MarshalAs(UnmanagedType.LPArray)] short[] pwLogClust, [MarshalAs(UnmanagedType.LPArray)] SCRIPT_VISATTR[] psva, [MarshalAs(UnmanagedType.LPArray)] int[] piAdvance, ref SCRIPT_ANALYSIS psa, ref int piCP, ref int piTrailing);

		[DllImport("usp10.dll")]
		public static extern int ScriptCPtoX(int iCP, bool fTrailing, int cChars, int cGlyphs, [MarshalAs(UnmanagedType.LPArray)] short[] pwLogClust, [MarshalAs(UnmanagedType.LPArray)] SCRIPT_VISATTR[] psva, [MarshalAs(UnmanagedType.LPArray)] int[] piAdvance, ref SCRIPT_ANALYSIS psa, ref int piX);

		[DllImport("usp10.dll")]
		public static extern int ScriptGetCMap(IntPtr hdc, ref IntPtr psc, [MarshalAs(UnmanagedType.LPWStr)] string pwcInChars, int cChars, uint dwFlags, [Out] [MarshalAs(UnmanagedType.LPArray)] short[] pwOutGlyphs);

		[DllImport("usp10.dll")]
		public static extern int ScriptGetLogicalWidths(ref SCRIPT_ANALYSIS psa, int cChars, int cGlyphs, int[] piGlyphWidth, short[] pwLogClust, SCRIPT_VISATTR[] psva, [Out] [MarshalAs(UnmanagedType.LPArray)] int[] piDx);

		[DllImport("usp10.dll")]
		public static extern int ScriptBreak([MarshalAs(UnmanagedType.LPWStr)] string pwcChars, int cChars, ref SCRIPT_ANALYSIS psa, [Out] [MarshalAs(UnmanagedType.LPArray)] SCRIPT_LOGATTR[] psla);

		[DllImport("usp10.dll")]
		public static extern int ScriptGetProperties(out IntPtr ppScriptProperties, out int pNumScripts);

		[DllImport("usp10.dll")]
		public static extern int ScriptIsComplex([MarshalAs(UnmanagedType.LPWStr)] string pwcInChars, int cInChars, uint dwFlags);

		[DllImport("usp10.dll")]
		public static extern int ScriptItemize([MarshalAs(UnmanagedType.LPWStr)] string pwcInChars, int cInChars, int cMaxItems, ref SCRIPT_CONTROL psControl, ref SCRIPT_STATE psState, [In] [Out] SCRIPT_ITEM[] pItems, ref int pcItems);

		[DllImport("usp10.dll")]
		public static extern int ScriptLayout(int cRuns, [MarshalAs(UnmanagedType.LPArray)] byte[] pbLevel, [MarshalAs(UnmanagedType.LPArray)] int[] piVisualToLogical, [MarshalAs(UnmanagedType.LPArray)] int[] piLogicalToVisual);

		[DllImport("usp10.dll")]
		public static extern int ScriptShape(IntPtr hdc, ref ScriptCacheSafeHandle psc, [MarshalAs(UnmanagedType.LPWStr)] string pwcChars, int cChars, int cMaxGlyphs, ref SCRIPT_ANALYSIS psa, [Out] [MarshalAs(UnmanagedType.LPArray)] short[] pwOutGlyphs, [Out] [MarshalAs(UnmanagedType.LPArray)] short[] pwLogClust, [Out] [MarshalAs(UnmanagedType.LPArray)] SCRIPT_VISATTR[] psva, ref int pcGlyphs);

		[DllImport("usp10.dll")]
		public static extern int ScriptShape(Win32DCSafeHandle hdc, ref ScriptCacheSafeHandle psc, [MarshalAs(UnmanagedType.LPWStr)] string pwcChars, int cChars, int cMaxGlyphs, ref SCRIPT_ANALYSIS psa, [Out] [MarshalAs(UnmanagedType.LPArray)] short[] pwOutGlyphs, [Out] [MarshalAs(UnmanagedType.LPArray)] short[] pwLogClust, [Out] [MarshalAs(UnmanagedType.LPArray)] SCRIPT_VISATTR[] psva, ref int pcGlyphs);

		[DllImport("usp10.dll")]
		public static extern int ScriptFreeCache(ref IntPtr psc);

		[DllImport("usp10.dll")]
		public static extern int ScriptPlace(IntPtr hdc, ref ScriptCacheSafeHandle psc, [MarshalAs(UnmanagedType.LPArray)] short[] pwGlyphs, int cGlyphs, [MarshalAs(UnmanagedType.LPArray)] SCRIPT_VISATTR[] psva, ref SCRIPT_ANALYSIS psa, [MarshalAs(UnmanagedType.LPArray)] int[] piAdvance, [Out] [MarshalAs(UnmanagedType.LPArray)] GOFFSET[] pGoffset, ref ABC pABC);

		[DllImport("usp10.dll")]
		public static extern int ScriptPlace(Win32DCSafeHandle hdc, ref ScriptCacheSafeHandle psc, [MarshalAs(UnmanagedType.LPArray)] short[] pwGlyphs, int cGlyphs, [MarshalAs(UnmanagedType.LPArray)] SCRIPT_VISATTR[] psva, ref SCRIPT_ANALYSIS psa, [MarshalAs(UnmanagedType.LPArray)] int[] piAdvance, [Out] [MarshalAs(UnmanagedType.LPArray)] GOFFSET[] pGoffset, ref ABC pABC);

		[DllImport("usp10.dll")]
		public static extern int ScriptTextOut(IntPtr hdc, ref ScriptCacheSafeHandle psc, int x, int y, uint fuOptions, IntPtr lprc, ref SCRIPT_ANALYSIS psa, IntPtr pwcReserved, int iReserved, [MarshalAs(UnmanagedType.LPArray)] short[] pwGlyphs, int cGlyphs, [MarshalAs(UnmanagedType.LPArray)] int[] piAdvance, [MarshalAs(UnmanagedType.LPArray)] int[] piJustify, [MarshalAs(UnmanagedType.LPArray)] GOFFSET[] pGoffset);

		[DllImport("usp10.dll")]
		public static extern int ScriptTextOut(Win32DCSafeHandle hdc, ref ScriptCacheSafeHandle psc, int x, int y, uint fuOptions, IntPtr lprc, ref SCRIPT_ANALYSIS psa, IntPtr pwcReserved, int iReserved, [MarshalAs(UnmanagedType.LPArray)] short[] pwGlyphs, int cGlyphs, [MarshalAs(UnmanagedType.LPArray)] int[] piAdvance, [MarshalAs(UnmanagedType.LPArray)] int[] piJustify, [MarshalAs(UnmanagedType.LPArray)] GOFFSET[] pGoffset);

		[DllImport("gdi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool ExtTextOut(IntPtr hdc, int X, int Y, uint fuOptions, IntPtr lprc, [MarshalAs(UnmanagedType.LPWStr)] string ptcInText, uint cbCount, [In] [MarshalAs(UnmanagedType.LPArray)] int[] lpDx);

		[DllImport("gdi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool ExtTextOut(Win32DCSafeHandle hdc, int X, int Y, uint fuOptions, IntPtr lprc, [MarshalAs(UnmanagedType.LPWStr)] string ptcInText, uint cbCount, [In] [MarshalAs(UnmanagedType.LPArray)] int[] lpDx);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern Win32DCSafeHandle CreateCompatibleDC(IntPtr hdc);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern Win32ObjectSafeHandle CreateDIBSection([In] Win32DCSafeHandle hdc, [In] ref BITMAPINFOHEADER pbmi, [In] uint iUsage, [In] [Out] ref IntPtr ppvBits, [In] IntPtr hSection, [In] uint dwOffset);

		[DllImport("usp10.dll", SetLastError = true)]
		public static extern int ScriptGetFontProperties(IntPtr hdc, ref ScriptCacheSafeHandle psc, ref SCRIPT_FONTPROPERTIES sfp);

		[DllImport("usp10.dll", SetLastError = true)]
		public static extern int ScriptGetFontProperties(Win32DCSafeHandle hdc, ref ScriptCacheSafeHandle psc, ref SCRIPT_FONTPROPERTIES sfp);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern IntPtr SelectObject(IntPtr hDC, IntPtr gdiobj);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern Win32ObjectSafeHandle SelectObject(Win32DCSafeHandle hDC, Win32ObjectSafeHandle gdiobj);

		[DllImport("User32.dll", SetLastError = true)]
		public static extern IntPtr GetDC(IntPtr hWnd);

		[DllImport("User32.dll", SetLastError = true)]
		public static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

		[DllImport("User32.dll", SetLastError = true)]
		public static extern bool ReleaseDC(Win32DCSafeHandle hWnd, Win32DCSafeHandle hDC);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern bool DeleteDC(IntPtr hdc);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern bool DeleteDC(Win32DCSafeHandle hdc);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern int DeleteObject(IntPtr hObject);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern int DeleteObject(Win32ObjectSafeHandle hObject);

		[DllImport("gdi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern Win32ObjectSafeHandle CreateFont(int nHeight, int nWidth, int nEscapement, int nOrientation, int fnWeight, uint fdwItalic, uint fdwUnderline, uint fdwStrikeOut, uint fdwCharSet, uint fdwOutputPrecision, uint fdwClipPrecision, uint fdwQuality, uint fdwPitchAndFamily, string lpszFace);

		[DllImport("gdi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr CreateFontIndirect(ref LOGFONT lplf);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern int GetDeviceCaps(Win32DCSafeHandle hdc, int nIndex);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern uint SetTextColor(IntPtr hdc, uint crColor);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern uint SetTextColor(Win32DCSafeHandle hdc, uint crColor);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern uint SetTextAlign(IntPtr hdc, uint fMode);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern uint SetTextAlign(Win32DCSafeHandle hdc, uint fMode);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern int SetBkMode(IntPtr hdc, int iBkMode);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern int SetBkMode(Win32DCSafeHandle hdc, int iBkMode);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern Win32ObjectSafeHandle CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern bool SetViewportOrgEx(IntPtr hdc, int X, int Y, IntPtr lpPoint);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern bool SetViewportOrgEx(Win32DCSafeHandle hdc, int X, int Y, Win32ObjectSafeHandle lpPoint);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern bool GetViewportOrgEx(IntPtr hdc, out POINT point);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern bool GetViewportOrgEx(Win32DCSafeHandle hdc, out POINT point);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern int GetClipRgn(IntPtr hdc, IntPtr hrgn);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern int GetClipRgn(Win32DCSafeHandle hdc, Win32ObjectSafeHandle hrgn);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern int SelectClipRgn(IntPtr hdc, IntPtr hrgn);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern int SelectClipRgn(Win32DCSafeHandle hdc, Win32ObjectSafeHandle hrgn);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern bool GetTextMetrics(IntPtr hdc, out TEXTMETRIC tm);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern bool GetTextMetrics(Win32DCSafeHandle hdc, out TEXTMETRIC tm);

		[DllImport("gdi32.dll", EntryPoint = "GetObject", SetLastError = true)]
		public static extern int GetFontObject(IntPtr hgdiobj, int cbBuffer, ref LOGFONT lf);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern bool Rectangle(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern int GetCharABCWidthsFloat(IntPtr hdc, uint iFirstChar, uint iLastChar, [In] [Out] ABCFloat[] lpABCF);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern int GetCharABCWidthsFloat(Win32DCSafeHandle hdc, uint iFirstChar, uint iLastChar, [In] [Out] ABCFloat[] lpABCF);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern uint GetGlyphIndicesW(IntPtr hdc, ushort[] lpstr, int c, ushort[] g, uint fl);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern uint GetGlyphIndicesW(Win32DCSafeHandle hdc, ushort[] lpstr, int c, ushort[] g, uint fl);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern bool GetTextExtentExPointI(IntPtr hdc, ushort[] pgiIn, int cgi, int nMaxExtent, ref int lpnFit, [In] [Out] int[] alpDx, ref Size lpSize);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern bool GetTextExtentExPointI(Win32DCSafeHandle hdc, ushort[] pgiIn, int cgi, int nMaxExtent, ref int lpnFit, [In] [Out] int[] alpDx, ref Size lpSize);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern uint GetOutlineTextMetrics(IntPtr hdc, uint cbData, ref OutlineTextMetric lpOTM);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern uint GetOutlineTextMetrics(Win32DCSafeHandle hdc, uint cbData, ref OutlineTextMetric lpOTM);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern uint GetFontData(IntPtr hdc, int dwTable, int dwOffset, byte[] lpvBuffer, int cbData);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern uint GetFontData(Win32DCSafeHandle hdc, int dwTable, int dwOffset, byte[] lpvBuffer, int cbData);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern bool MoveToEx(IntPtr hdc, int X, int Y, IntPtr old);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern bool MoveToEx(Win32DCSafeHandle hdc, int X, int Y, IntPtr old);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern bool LineTo(IntPtr hdc, int nXEnd, int nYEnd);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern bool LineTo(Win32DCSafeHandle hdc, int nXEnd, int nYEnd);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern Win32ObjectSafeHandle CreatePen(int fnPenStyle, int nWidth, uint crColor);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern IntPtr CreateSolidBrush(uint crColor);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern Win32ObjectSafeHandle ExtCreatePen(uint dwPenStyle, uint dwWidth, ref LOGBRUSH lplb, uint dwStyleCount, [In] [Out] uint[] lpStyle);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern int SetMapMode(IntPtr hdc, int fnMapMode);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern int SetMapMode(Win32DCSafeHandle hdc, int fnMapMode);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern int GetMapMode(IntPtr hdc);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern int GetMapMode(Win32DCSafeHandle hdc);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern bool SetWorldTransform(IntPtr hdc, [In] ref XFORM lpXform);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern bool SetWorldTransform(Win32DCSafeHandle hdc, [In] ref XFORM lpXform);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern int GetGraphicsMode(IntPtr hdc);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern int GetGraphicsMode(Win32DCSafeHandle hdc);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern int SetGraphicsMode(IntPtr hdc, int iMode);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern int SetGraphicsMode(Win32DCSafeHandle hdc, int iMode);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern bool GetWorldTransform(IntPtr hdc, [In] [Out] ref XFORM lpXform);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern bool GetWorldTransform(Win32DCSafeHandle hdc, [In] [Out] ref XFORM lpXform);
	}
}

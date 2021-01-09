using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Security;

namespace AspNetCore.ReportingServices.Rendering.RichText
{
	[SuppressUnmanagedCodeSecurity]
	internal sealed class Win32
	{
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		internal struct BITMAPINFOHEADER
		{
			internal uint biSize;

			internal int biWidth;

			internal int biHeight;

			internal short biPlanes;

			internal short biBitCount;

			internal uint biCompression;

			internal uint biSizeImage;

			internal int biXPelsPerMeter;

			internal int biYPelsPerMeter;

			internal uint biClrUsed;

			internal uint biClrImportant;

			internal BITMAPINFOHEADER(int widthPX, int heightPX, int dpiX, int dpiY)
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
		internal struct SCRIPT_FONTPROPERTIES
		{
			internal int cBytes;

			internal short wgBlank;

			internal short wgDefault;

			internal short wgInvalid;

			internal short wgKashida;

			internal int iKashidaWidth;
		}

		internal struct POINT
		{
			internal int x;

			internal int y;
		}

		internal struct TEXTMETRIC
		{
			internal int tmHeight;

			internal int tmAscent;

			internal int tmDescent;

			internal int tminternalLeading;

			internal int tmExternalLeading;

			internal int tmAveCharWidth;

			internal int tmMaxCharWidth;

			internal int tmWeight;

			internal int tmOverhang;

			internal int tmDigitizedAspectX;

			internal int tmDigitizedAspectY;

			internal char tmFirstChar;

			internal char tmLastChar;

			internal char tmDefaultChar;

			internal char tmBreakChar;

			internal byte tmItalic;

			internal byte tmUnderlined;

			internal byte tmStruckOut;

			internal byte tmPitchAndFamily;

			internal byte tmCharSet;
		}

		internal struct LOGFONT
		{
			internal int lfHeight;

			internal int lfWidth;

			internal int lfEscapement;

			internal int lfOrientation;

			internal int lfWeight;

			internal byte lfItalic;

			internal byte lfUnderline;

			internal byte lfStrikeOut;

			internal byte lfCharSet;

			internal byte lfOutPrecision;

			internal byte lfClipPrecision;

			internal byte lfQuality;

			internal byte lfPitchAndFamily;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			internal string lfFaceName;
		}

		internal struct LOGBRUSH
		{
			internal uint lbStyle;

			internal uint lbColor;

			internal int lbHatch;
		}

		internal struct ABCFloat
		{
			internal float abcfA;

			internal float abcfB;

			internal float abcfC;

			internal ABCFloat(float a, float b, float c)
			{
				this.abcfA = a;
				this.abcfB = b;
				this.abcfC = c;
			}
		}

		internal struct XFORM
		{
			internal float eM11;

			internal float eM12;

			internal float eM21;

			internal float eM22;

			internal float eDx;

			internal float eDy;

			internal static XFORM Identity
			{
				get
				{
					XFORM result = default(XFORM);
					result.eM11 = 1f;
					result.eM22 = 1f;
					return result;
				}
			}

			internal XFORM(Matrix matrix, GraphicsUnit units, float dpi)
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

			internal void Transform(ref RectangleF rect)
			{
				PointF location = rect.Location;
				PointF pointF = new PointF(rect.Right, rect.Bottom);
				this.Transform(ref location);
				this.Transform(ref pointF);
				rect = RectangleF.FromLTRB(location.X, location.Y, pointF.X, pointF.Y);
			}

			internal void Transform(ref PointF pt)
			{
				pt = new PointF(pt.X * this.eM11 + pt.Y * this.eM12 + this.eDx, pt.X * this.eM21 + pt.Y * this.eM22 + this.eDy);
			}

			internal void Transform(ref Rectangle rect)
			{
				Point location = rect.Location;
				Point point = new Point(rect.Right, rect.Bottom);
				this.Transform(ref location);
				this.Transform(ref point);
				rect = System.Drawing.Rectangle.FromLTRB(location.X, location.Y, point.X, point.Y);
			}

			internal void Transform(ref Point pt)
			{
				pt = new Point((int)((float)pt.X * this.eM11 + (float)pt.Y * this.eM12 + this.eDx), (int)((float)pt.X * this.eM21 + (float)pt.Y * this.eM22 + this.eDy));
			}
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal struct OutlineTextMetric
		{
			internal uint otmSize;

			internal int tmHeight;

			internal int tmAscent;

			internal int tmDescent;

			internal int tmInternalLeading;

			internal int tmExternalLeading;

			internal int tmAveCharWidth;

			internal int tmMaxCharWidth;

			internal int tmWeight;

			internal int tmOverhang;

			internal int tmDigitizedAspectX;

			internal int tmDigitizedAspectY;

			internal char tmFirstChar;

			internal char tmLastChar;

			internal char tmDefaultChar;

			internal char tmBreakChar;

			internal byte tmItalic;

			internal byte tmUnderlined;

			internal byte tmStruckOut;

			internal byte tmPitchAndFamily;

			internal byte tmCharSet;

			internal byte otmFiller;

			internal byte bFamilyType;

			internal byte bSerifStyle;

			internal byte bWeight;

			internal byte bProportion;

			internal byte bContrast;

			internal byte bStrokeVariation;

			internal byte bArmStyle;

			internal byte bLetterform;

			internal byte bMidline;

			internal byte bXHeight;

			internal uint otmfsSelection;

			internal uint otmfsType;

			internal int otmsCharSlopeRise;

			internal int otmsCharSlopeRun;

			internal int otmItalicAngle;

			internal uint otmEMSquare;

			internal int otmAscent;

			internal int otmDescent;

			internal uint otmLineGap;

			internal uint otmsCapEmHeight;

			internal uint otmsXHeight;

			internal int left;

			internal int top;

			internal int right;

			internal int bottom;

			internal int otmMacAscent;

			internal int otmMacDescent;

			internal uint otmMacLineGap;

			internal uint otmusMinimumPPEM;

			internal int otmptSubscriptSizeX;

			internal int otmptSubscriptSizeY;

			internal int otmptSubscriptOffsetX;

			internal int otmptSubscriptOffsetY;

			internal int otmptSuperscriptSizeX;

			internal int otmptSuperscriptSizeY;

			internal int otmptSuperscriptOffsetX;

			internal int otmptSuperscriptOffsetY;

			internal uint otmsStrikeoutSize;

			internal int otmsStrikeoutPosition;

			internal int otmsUnderscoreSize;

			internal int otmsUnderscorePosition;

			internal string otmpFamilyName;

			internal string otmpFaceName;

			internal string otmpStyleName;

			internal string otmpFullName;

			internal OutlineTextMetric(string intialValue)
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

		internal const int HORZSIZE = 4;

		internal const int VERTSIZE = 6;

		internal const int HORZRES = 8;

		internal const int VERTRES = 10;

		internal const int LOGPIXELSX = 88;

		internal const int LOGPIXELSY = 90;

		internal const int S_FALSE = 1;

		internal const int ETO_CLIPPED = 4;

		internal const int ETO_OPAQUE = 2;

		internal const int ETO_RTLREADING = 128;

		internal const int USP_E_SCRIPT_NOT_IN_FONT = -2147220992;

		internal const int E_OUTOFMEMORY = -2147024882;

		internal const int E_PENDING = -2147483638;

		internal const int SCRIPT_UNDEFINED = 0;

		internal const int SIC_COMPLEX = 1;

		internal const int SIC_ASCIIDIGIT = 2;

		internal const int SIC_NEUTRAL = 4;

		internal const int GM_COMPATIBLE = 1;

		internal const int GM_ADVANCED = 2;

		internal const int PS_GEOMETRIC = 65536;

		internal const int PS_COSMETIC = 0;

		internal const int PS_ALTERNATE = 8;

		internal const int PS_SOLID = 0;

		internal const int PS_DASH = 1;

		internal const int PS_DOT = 2;

		internal const int PS_DASHDOT = 3;

		internal const int PS_DASHDOTDOT = 4;

		internal const int PS_NULL = 5;

		internal const int PS_USERSTYLE = 7;

		internal const int PS_INSIDEFRAME = 6;

		internal const int PS_ENDCAP_ROUND = 0;

		internal const int PS_ENDCAP_SQUARE = 256;

		internal const int PS_ENDCAP_FLAT = 512;

		internal const int PS_JOIN_BEVEL = 4096;

		internal const int PS_JOIN_MITER = 8192;

		internal const int PS_JOIN_ROUND = 0;

		internal const int BS_SOLID = 0;

		internal const int BS_NULL = 1;

		internal const int BS_HOLLOW = 1;

		internal const int BS_HATCHED = 2;

		internal const int BS_PATTERN = 3;

		internal const int BS_INDEXED = 4;

		internal const int BS_DIBPATTERN = 5;

		internal const int BS_DIBPATTERNPT = 6;

		internal const int BS_PATTERN8X8 = 7;

		internal const int BS_DIBPATTERN8X8 = 8;

		internal const int BS_MONOPATTERN = 9;

		internal const int HS_HORIZONTAL = 0;

		internal const int HS_VERTICAL = 1;

		internal const int HS_FDIAGONAL = 2;

		internal const int HS_BDIAGONAL = 3;

		internal const int HS_CROSS = 4;

		internal const int HS_DIAGCROSS = 5;

		internal const int MM_ANISOTROPIC = 8;

		internal const int MM_HIENGLISH = 5;

		internal const int MM_HIMETRIC = 3;

		internal const int MM_ISOTROPIC = 7;

		internal const int MM_LOENGLISH = 4;

		internal const int MM_LOMETRIC = 2;

		internal const int MM_TEXT = 1;

		internal const int MM_TWIPS = 6;

		internal const int TextRenderingHintSystemDefault = 0;

		internal const int TextRenderingHintSingleBitPerPixelGridFit = 1;

		internal const int TextRenderingHintSingleBitPerPixel = 2;

		internal const int TextRenderingHintAntiAliasGridFit = 3;

		internal const int TextRenderingHintAntiAlias = 4;

		internal const int TextRenderingHintClearTypeGridFit = 5;

		internal const uint DEFAULT_QUALITY = 0u;

		internal const uint DRAFT_QUALITY = 1u;

		internal const uint PROOF_QUALITY = 2u;

		internal const uint NONANTIALIASED_QUALITY = 3u;

		internal const uint ANTIALIASED_QUALITY = 4u;

		internal const uint CLEARTYPE_QUALITY = 5u;

		internal const uint CLEARTYPE_NATURAL_QUALITY = 6u;

		internal const uint BI_RGB = 0u;

		internal const double METER_PER_INCH = 0.0254;

		internal const int TA_BASELINE = 24;

		internal const int TA_RTLREADING = 256;

		internal const int TRANSPARENT = 1;

		internal const int OPAQUE = 2;

		internal const int OUT_TT_PRECIS = 4;

		internal const int OUT_TT_ONLY_PRECIS = 7;

		internal const int OUT_DEFAULT_PRECIS = 0;

		internal const int LF_FACESIZE = 32;

		internal const int LF_FULLFACESIZE = 64;

		internal const int DEFAULT_CHARSET = 1;

		internal const int SYMBOL_CHARSET = 2;

		private Win32()
		{
		}

		internal static bool Failed(int hr)
		{
			return hr < 0;
		}

		internal static bool Succeeded(int hr)
		{
			return hr >= 0;
		}

		[DllImport("usp10.dll")]
		internal static extern int ScriptXtoCP(int iX, int cChars, int cGlyphs, [MarshalAs(UnmanagedType.LPArray)] short[] pwLogClust, [MarshalAs(UnmanagedType.LPArray)] SCRIPT_VISATTR[] psva, [MarshalAs(UnmanagedType.LPArray)] int[] piAdvance, ref SCRIPT_ANALYSIS psa, ref int piCP, ref int piTrailing);

		[DllImport("usp10.dll")]
		internal static extern int ScriptCPtoX(int iCP, bool fTrailing, int cChars, int cGlyphs, [MarshalAs(UnmanagedType.LPArray)] short[] pwLogClust, [MarshalAs(UnmanagedType.LPArray)] SCRIPT_VISATTR[] psva, [MarshalAs(UnmanagedType.LPArray)] int[] piAdvance, ref SCRIPT_ANALYSIS psa, ref int piX);

		[DllImport("usp10.dll")]
		internal static extern int ScriptGetCMap(IntPtr hdc, ref IntPtr psc, [MarshalAs(UnmanagedType.LPWStr)] string pwcInChars, int cChars, uint dwFlags, [Out] [MarshalAs(UnmanagedType.LPArray)] short[] pwOutGlyphs);

		[DllImport("usp10.dll")]
		internal static extern int ScriptGetLogicalWidths(ref SCRIPT_ANALYSIS psa, int cChars, int cGlyphs, int[] piGlyphWidth, short[] pwLogClust, SCRIPT_VISATTR[] psva, [Out] [MarshalAs(UnmanagedType.LPArray)] int[] piDx);

		[DllImport("usp10.dll")]
		internal static extern int ScriptBreak([MarshalAs(UnmanagedType.LPWStr)] string pwcChars, int cChars, ref SCRIPT_ANALYSIS psa, [Out] [MarshalAs(UnmanagedType.LPArray)] SCRIPT_LOGATTR[] psla);

		[DllImport("usp10.dll")]
		internal static extern int ScriptGetProperties(out IntPtr ppScriptProperties, out int pNumScripts);

		[DllImport("usp10.dll")]
		internal static extern int ScriptIsComplex([MarshalAs(UnmanagedType.LPWStr)] string pwcInChars, int cInChars, uint dwFlags);

		[DllImport("usp10.dll")]
		internal static extern int ScriptItemize([MarshalAs(UnmanagedType.LPWStr)] string pwcInChars, int cInChars, int cMaxItems, ref SCRIPT_CONTROL psControl, ref SCRIPT_STATE psState, [In] [Out] SCRIPT_ITEM[] pItems, ref int pcItems);

		[DllImport("usp10.dll")]
		internal static extern int ScriptLayout(int cRuns, [MarshalAs(UnmanagedType.LPArray)] byte[] pbLevel, [MarshalAs(UnmanagedType.LPArray)] int[] piVisualToLogical, [MarshalAs(UnmanagedType.LPArray)] int[] piLogicalToVisual);

		[DllImport("usp10.dll")]
		internal static extern int ScriptShape(IntPtr hdc, ref ScriptCacheSafeHandle psc, [MarshalAs(UnmanagedType.LPWStr)] string pwcChars, int cChars, int cMaxGlyphs, ref SCRIPT_ANALYSIS psa, [Out] [MarshalAs(UnmanagedType.LPArray)] short[] pwOutGlyphs, [Out] [MarshalAs(UnmanagedType.LPArray)] short[] pwLogClust, [Out] [MarshalAs(UnmanagedType.LPArray)] SCRIPT_VISATTR[] psva, ref int pcGlyphs);

		[DllImport("usp10.dll")]
		internal static extern int ScriptShape(Win32DCSafeHandle hdc, ref ScriptCacheSafeHandle psc, [MarshalAs(UnmanagedType.LPWStr)] string pwcChars, int cChars, int cMaxGlyphs, ref SCRIPT_ANALYSIS psa, [Out] [MarshalAs(UnmanagedType.LPArray)] short[] pwOutGlyphs, [Out] [MarshalAs(UnmanagedType.LPArray)] short[] pwLogClust, [Out] [MarshalAs(UnmanagedType.LPArray)] SCRIPT_VISATTR[] psva, ref int pcGlyphs);

		[DllImport("usp10.dll")]
		internal static extern int ScriptFreeCache(ref IntPtr psc);

		[DllImport("usp10.dll")]
		internal static extern int ScriptPlace(IntPtr hdc, ref ScriptCacheSafeHandle psc, [MarshalAs(UnmanagedType.LPArray)] short[] pwGlyphs, int cGlyphs, [MarshalAs(UnmanagedType.LPArray)] SCRIPT_VISATTR[] psva, ref SCRIPT_ANALYSIS psa, [MarshalAs(UnmanagedType.LPArray)] int[] piAdvance, [Out] [MarshalAs(UnmanagedType.LPArray)] GOFFSET[] pGoffset, ref ABC pABC);

		[DllImport("usp10.dll")]
		internal static extern int ScriptPlace(Win32DCSafeHandle hdc, ref ScriptCacheSafeHandle psc, [MarshalAs(UnmanagedType.LPArray)] short[] pwGlyphs, int cGlyphs, [MarshalAs(UnmanagedType.LPArray)] SCRIPT_VISATTR[] psva, ref SCRIPT_ANALYSIS psa, [MarshalAs(UnmanagedType.LPArray)] int[] piAdvance, [Out] [MarshalAs(UnmanagedType.LPArray)] GOFFSET[] pGoffset, ref ABC pABC);

		[DllImport("usp10.dll")]
		internal static extern int ScriptTextOut(IntPtr hdc, ref ScriptCacheSafeHandle psc, int x, int y, uint fuOptions, IntPtr lprc, ref SCRIPT_ANALYSIS psa, IntPtr pwcReserved, int iReserved, [MarshalAs(UnmanagedType.LPArray)] short[] pwGlyphs, int cGlyphs, [MarshalAs(UnmanagedType.LPArray)] int[] piAdvance, [MarshalAs(UnmanagedType.LPArray)] int[] piJustify, [MarshalAs(UnmanagedType.LPArray)] GOFFSET[] pGoffset);

		[DllImport("usp10.dll")]
		internal static extern int ScriptTextOut(Win32DCSafeHandle hdc, ref ScriptCacheSafeHandle psc, int x, int y, uint fuOptions, IntPtr lprc, ref SCRIPT_ANALYSIS psa, IntPtr pwcReserved, int iReserved, [MarshalAs(UnmanagedType.LPArray)] short[] pwGlyphs, int cGlyphs, [MarshalAs(UnmanagedType.LPArray)] int[] piAdvance, [MarshalAs(UnmanagedType.LPArray)] int[] piJustify, [MarshalAs(UnmanagedType.LPArray)] GOFFSET[] pGoffset);

		[DllImport("gdi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool ExtTextOut(IntPtr hdc, int X, int Y, uint fuOptions, IntPtr lprc, [MarshalAs(UnmanagedType.LPWStr)] string ptcInText, uint cbCount, [In] [MarshalAs(UnmanagedType.LPArray)] int[] lpDx);

		[DllImport("gdi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool ExtTextOut(Win32DCSafeHandle hdc, int X, int Y, uint fuOptions, IntPtr lprc, [MarshalAs(UnmanagedType.LPWStr)] string ptcInText, uint cbCount, [In] [MarshalAs(UnmanagedType.LPArray)] int[] lpDx);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern Win32DCSafeHandle CreateCompatibleDC(IntPtr hdc);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern Win32ObjectSafeHandle CreateDIBSection([In] Win32DCSafeHandle hdc, [In] ref BITMAPINFOHEADER pbmi, [In] uint iUsage, [In] [Out] ref IntPtr ppvBits, [In] IntPtr hSection, [In] uint dwOffset);

		[DllImport("usp10.dll", SetLastError = true)]
		internal static extern int ScriptGetFontProperties(IntPtr hdc, ref ScriptCacheSafeHandle psc, ref SCRIPT_FONTPROPERTIES sfp);

		[DllImport("usp10.dll", SetLastError = true)]
		internal static extern int ScriptGetFontProperties(Win32DCSafeHandle hdc, ref ScriptCacheSafeHandle psc, ref SCRIPT_FONTPROPERTIES sfp);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern IntPtr SelectObject(IntPtr hDC, IntPtr gdiobj);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern Win32ObjectSafeHandle SelectObject(Win32DCSafeHandle hDC, Win32ObjectSafeHandle gdiobj);

		[DllImport("User32.dll", SetLastError = true)]
		internal static extern IntPtr GetDC(IntPtr hWnd);

		[DllImport("User32.dll", SetLastError = true)]
		internal static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

		[DllImport("User32.dll", SetLastError = true)]
		internal static extern bool ReleaseDC(Win32DCSafeHandle hWnd, Win32DCSafeHandle hDC);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern bool DeleteDC(IntPtr hdc);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern bool DeleteDC(Win32DCSafeHandle hdc);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern int DeleteObject(IntPtr hObject);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern int DeleteObject(Win32ObjectSafeHandle hObject);

		[DllImport("gdi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern Win32ObjectSafeHandle CreateFont(int nHeight, int nWidth, int nEscapement, int nOrientation, int fnWeight, uint fdwItalic, uint fdwUnderline, uint fdwStrikeOut, uint fdwCharSet, uint fdwOutputPrecision, uint fdwClipPrecision, uint fdwQuality, uint fdwPitchAndFamily, string lpszFace);

		[DllImport("gdi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern IntPtr CreateFontIndirect(ref LOGFONT lplf);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern int GetDeviceCaps(Win32DCSafeHandle hdc, int nIndex);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern uint SetTextColor(IntPtr hdc, uint crColor);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern uint SetTextColor(Win32DCSafeHandle hdc, uint crColor);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern uint SetTextAlign(IntPtr hdc, uint fMode);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern uint SetTextAlign(Win32DCSafeHandle hdc, uint fMode);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern int SetBkMode(IntPtr hdc, int iBkMode);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern int SetBkMode(Win32DCSafeHandle hdc, int iBkMode);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern Win32ObjectSafeHandle CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern bool SetViewportOrgEx(IntPtr hdc, int X, int Y, IntPtr lpPoint);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern bool SetViewportOrgEx(Win32DCSafeHandle hdc, int X, int Y, Win32ObjectSafeHandle lpPoint);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern bool GetViewportOrgEx(IntPtr hdc, out POINT point);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern bool GetViewportOrgEx(Win32DCSafeHandle hdc, out POINT point);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern int GetClipRgn(IntPtr hdc, IntPtr hrgn);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern int GetClipRgn(Win32DCSafeHandle hdc, Win32ObjectSafeHandle hrgn);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern int SelectClipRgn(IntPtr hdc, IntPtr hrgn);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern int SelectClipRgn(Win32DCSafeHandle hdc, Win32ObjectSafeHandle hrgn);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern bool GetTextMetrics(IntPtr hdc, out TEXTMETRIC tm);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern bool GetTextMetrics(Win32DCSafeHandle hdc, out TEXTMETRIC tm);

		[DllImport("gdi32.dll", EntryPoint = "GetObject", SetLastError = true)]
		internal static extern int GetFontObject(IntPtr hgdiobj, int cbBuffer, ref LOGFONT lf);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern bool Rectangle(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern int GetCharABCWidthsFloat(IntPtr hdc, uint iFirstChar, uint iLastChar, [In] [Out] ABCFloat[] lpABCF);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern int GetCharABCWidthsFloat(Win32DCSafeHandle hdc, uint iFirstChar, uint iLastChar, [In] [Out] ABCFloat[] lpABCF);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern uint GetGlyphIndicesW(IntPtr hdc, ushort[] lpstr, int c, ushort[] g, uint fl);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern uint GetGlyphIndicesW(Win32DCSafeHandle hdc, ushort[] lpstr, int c, ushort[] g, uint fl);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern bool GetTextExtentExPointI(IntPtr hdc, ushort[] pgiIn, int cgi, int nMaxExtent, ref int lpnFit, [In] [Out] int[] alpDx, ref Size lpSize);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern bool GetTextExtentExPointI(Win32DCSafeHandle hdc, ushort[] pgiIn, int cgi, int nMaxExtent, ref int lpnFit, [In] [Out] int[] alpDx, ref Size lpSize);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern uint GetOutlineTextMetrics(IntPtr hdc, uint cbData, ref OutlineTextMetric lpOTM);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern uint GetOutlineTextMetrics(Win32DCSafeHandle hdc, uint cbData, ref OutlineTextMetric lpOTM);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern uint GetFontData(IntPtr hdc, int dwTable, int dwOffset, byte[] lpvBuffer, int cbData);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern uint GetFontData(Win32DCSafeHandle hdc, int dwTable, int dwOffset, byte[] lpvBuffer, int cbData);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern bool MoveToEx(IntPtr hdc, int X, int Y, IntPtr old);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern bool MoveToEx(Win32DCSafeHandle hdc, int X, int Y, IntPtr old);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern bool LineTo(IntPtr hdc, int nXEnd, int nYEnd);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern bool LineTo(Win32DCSafeHandle hdc, int nXEnd, int nYEnd);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern Win32ObjectSafeHandle CreatePen(int fnPenStyle, int nWidth, uint crColor);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern IntPtr CreateSolidBrush(uint crColor);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern Win32ObjectSafeHandle ExtCreatePen(uint dwPenStyle, uint dwWidth, ref LOGBRUSH lplb, uint dwStyleCount, [In] [Out] uint[] lpStyle);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern int SetMapMode(IntPtr hdc, int fnMapMode);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern int SetMapMode(Win32DCSafeHandle hdc, int fnMapMode);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern int GetMapMode(IntPtr hdc);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern int GetMapMode(Win32DCSafeHandle hdc);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern bool SetWorldTransform(IntPtr hdc, [In] ref XFORM lpXform);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern bool SetWorldTransform(Win32DCSafeHandle hdc, [In] ref XFORM lpXform);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern int GetGraphicsMode(IntPtr hdc);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern int GetGraphicsMode(Win32DCSafeHandle hdc);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern int SetGraphicsMode(IntPtr hdc, int iMode);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern int SetGraphicsMode(Win32DCSafeHandle hdc, int iMode);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern bool GetWorldTransform(IntPtr hdc, [In] [Out] ref XFORM lpXform);

		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern bool GetWorldTransform(Win32DCSafeHandle hdc, [In] [Out] ref XFORM lpXform);
	}
}

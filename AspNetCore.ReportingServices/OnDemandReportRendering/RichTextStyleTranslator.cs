using System;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class RichTextStyleTranslator
	{
		public class StyleEnumConstants
		{
			public const string Default = "Default";

			public const string Normal = "Normal";

			public const string General = "General";

			public const string Center = "Center";

			public const string Left = "Left";

			public const string Right = "Right";

			public const string Thin = "Thin";

			public const string ExtraLight = "ExtraLight";

			public const string Light = "Light";

			public const string Lighter = "Lighter";

			public const string Medium = "Medium";

			public const string SemiBold = "SemiBold";

			public const string Bold = "Bold";

			public const string Bolder = "Bolder";

			public const string ExtraBold = "ExtraBold";

			public const string Heavy = "Heavy";

			public const string FontWeight100 = "100";

			public const string FontWeight200 = "200";

			public const string FontWeight300 = "300";

			public const string FontWeight400 = "400";

			public const string FontWeight500 = "500";

			public const string FontWeight600 = "600";

			public const string FontWeight700 = "700";

			public const string FontWeight800 = "800";

			public const string FontWeight900 = "900";
		}

		public static bool CompareWithInvariantCulture(string str1, string str2)
		{
			return string.Compare(str1, str2, StringComparison.OrdinalIgnoreCase) == 0;
		}

		public static bool TranslateHtmlFontSize(string value, out string translatedSize)
		{
			int num = default(int);
			if (int.TryParse(value, out num))
			{
				if (num <= 0)
				{
					translatedSize = "7.5pt";
				}
				else
				{
					switch (num)
					{
					case 1:
						translatedSize = "7.5pt";
						break;
					case 2:
						translatedSize = "10pt";
						break;
					case 3:
						translatedSize = "11pt";
						break;
					case 4:
						translatedSize = "13.5pt";
						break;
					case 5:
						translatedSize = "18pt";
						break;
					case 6:
						translatedSize = "24pt";
						break;
					default:
						translatedSize = "36pt";
						break;
					}
				}
				return true;
			}
			translatedSize = null;
			return false;
		}

		public static string TranslateHtmlColor(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				if (value[0] == '#')
				{
					return value;
				}
				if (char.IsDigit(value[0]))
				{
					return "#" + value;
				}
			}
			return value;
		}

		public static bool TranslateFontWeight(string styleString, out FontWeights fontWieght)
		{
			fontWieght = FontWeights.Normal;
			if (!string.IsNullOrEmpty(styleString))
			{
				if (RichTextStyleTranslator.CompareWithInvariantCulture("Normal", styleString))
				{
					fontWieght = FontWeights.Normal;
					goto IL_01b6;
				}
				if (RichTextStyleTranslator.CompareWithInvariantCulture("Bold", styleString))
				{
					fontWieght = FontWeights.Bold;
					goto IL_01b6;
				}
				if (RichTextStyleTranslator.CompareWithInvariantCulture("Bolder", styleString))
				{
					fontWieght = FontWeights.Bold;
					goto IL_01b6;
				}
				if (RichTextStyleTranslator.CompareWithInvariantCulture("100", styleString))
				{
					fontWieght = FontWeights.Thin;
					goto IL_01b6;
				}
				if (RichTextStyleTranslator.CompareWithInvariantCulture("200", styleString))
				{
					fontWieght = FontWeights.ExtraLight;
					goto IL_01b6;
				}
				if (RichTextStyleTranslator.CompareWithInvariantCulture("300", styleString))
				{
					fontWieght = FontWeights.Light;
					goto IL_01b6;
				}
				if (RichTextStyleTranslator.CompareWithInvariantCulture("400", styleString))
				{
					fontWieght = FontWeights.Normal;
					goto IL_01b6;
				}
				if (RichTextStyleTranslator.CompareWithInvariantCulture("500", styleString))
				{
					fontWieght = FontWeights.Medium;
					goto IL_01b6;
				}
				if (RichTextStyleTranslator.CompareWithInvariantCulture("600", styleString))
				{
					fontWieght = FontWeights.SemiBold;
					goto IL_01b6;
				}
				if (RichTextStyleTranslator.CompareWithInvariantCulture("700", styleString))
				{
					fontWieght = FontWeights.Bold;
					goto IL_01b6;
				}
				if (RichTextStyleTranslator.CompareWithInvariantCulture("800", styleString))
				{
					fontWieght = FontWeights.ExtraBold;
					goto IL_01b6;
				}
				if (RichTextStyleTranslator.CompareWithInvariantCulture("900", styleString))
				{
					fontWieght = FontWeights.Heavy;
					goto IL_01b6;
				}
				if (RichTextStyleTranslator.CompareWithInvariantCulture("Thin", styleString))
				{
					fontWieght = FontWeights.Thin;
					goto IL_01b6;
				}
				if (RichTextStyleTranslator.CompareWithInvariantCulture("ExtraLight", styleString))
				{
					fontWieght = FontWeights.ExtraLight;
					goto IL_01b6;
				}
				if (RichTextStyleTranslator.CompareWithInvariantCulture("Light", styleString))
				{
					fontWieght = FontWeights.Light;
					goto IL_01b6;
				}
				if (RichTextStyleTranslator.CompareWithInvariantCulture("Lighter", styleString))
				{
					fontWieght = FontWeights.Light;
					goto IL_01b6;
				}
				if (RichTextStyleTranslator.CompareWithInvariantCulture("Medium", styleString))
				{
					fontWieght = FontWeights.Medium;
					goto IL_01b6;
				}
				if (RichTextStyleTranslator.CompareWithInvariantCulture("SemiBold", styleString))
				{
					fontWieght = FontWeights.SemiBold;
					goto IL_01b6;
				}
				if (RichTextStyleTranslator.CompareWithInvariantCulture("ExtraBold", styleString))
				{
					fontWieght = FontWeights.ExtraBold;
					goto IL_01b6;
				}
				if (RichTextStyleTranslator.CompareWithInvariantCulture("Heavy", styleString))
				{
					fontWieght = FontWeights.Heavy;
					goto IL_01b6;
				}
				if (RichTextStyleTranslator.CompareWithInvariantCulture("Default", styleString))
				{
					fontWieght = FontWeights.Normal;
					goto IL_01b6;
				}
				return false;
			}
			return false;
			IL_01b6:
			return true;
		}

		public static bool TranslateTextAlign(string styleString, out TextAlignments textAlignment)
		{
			textAlignment = TextAlignments.General;
			if (!string.IsNullOrEmpty(styleString))
			{
				if (RichTextStyleTranslator.CompareWithInvariantCulture("General", styleString))
				{
					textAlignment = TextAlignments.General;
					goto IL_0067;
				}
				if (RichTextStyleTranslator.CompareWithInvariantCulture("Left", styleString))
				{
					textAlignment = TextAlignments.Left;
					goto IL_0067;
				}
				if (RichTextStyleTranslator.CompareWithInvariantCulture("Center", styleString))
				{
					textAlignment = TextAlignments.Center;
					goto IL_0067;
				}
				if (RichTextStyleTranslator.CompareWithInvariantCulture("Right", styleString))
				{
					textAlignment = TextAlignments.Right;
					goto IL_0067;
				}
				if (RichTextStyleTranslator.CompareWithInvariantCulture("Default", styleString))
				{
					textAlignment = TextAlignments.General;
					goto IL_0067;
				}
				return false;
			}
			return false;
			IL_0067:
			return true;
		}
	}
}

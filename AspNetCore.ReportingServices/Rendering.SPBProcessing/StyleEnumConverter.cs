using AspNetCore.ReportingServices.OnDemandReportRendering;
using AspNetCore.ReportingServices.Rendering.RPLProcessing;

namespace AspNetCore.ReportingServices.Rendering.SPBProcessing
{
	public static class StyleEnumConverter
	{
		public static byte Translate(FontStyles aValue)
		{
			switch (aValue)
			{
			case FontStyles.Italic:
				return 1;
			default:
				return 0;
			}
		}

		public static byte Translate(FontWeights aValue)
		{
			switch (aValue)
			{
			case FontWeights.Bold:
				return 6;
			case FontWeights.ExtraBold:
				return 7;
			case FontWeights.ExtraLight:
				return 2;
			case FontWeights.Heavy:
				return 8;
			case FontWeights.Light:
				return 3;
			case FontWeights.Medium:
				return 4;
			case FontWeights.SemiBold:
				return 5;
			case FontWeights.Thin:
				return 1;
			default:
				return 0;
			}
		}

		public static byte Translate(TextDecorations aValue)
		{
			switch (aValue)
			{
			case TextDecorations.LineThrough:
				return 3;
			case TextDecorations.Overline:
				return 2;
			case TextDecorations.Underline:
				return 1;
			default:
				return 0;
			}
		}

		public static byte Translate(TextAlignments aValue)
		{
			switch (aValue)
			{
			case TextAlignments.Center:
				return 2;
			case TextAlignments.Left:
				return 1;
			case TextAlignments.Right:
				return 3;
			default:
				return 0;
			}
		}

		public static byte Translate(VerticalAlignments aValue)
		{
			switch (aValue)
			{
			case VerticalAlignments.Middle:
				return 1;
			case VerticalAlignments.Bottom:
				return 2;
			default:
				return 0;
			}
		}

		public static byte Translate(Directions aValue)
		{
			switch (aValue)
			{
			case Directions.RTL:
				return 1;
			default:
				return 0;
			}
		}

		public static byte Translate(WritingModes aValue)
		{
			switch (aValue)
			{
			case WritingModes.Vertical:
				return 1;
			case WritingModes.Rotate270:
				return 2;
			default:
				return 0;
			}
		}

		public static byte Translate(WritingModes aValue, RPLVersionEnum rplVersion)
		{
			if ((int)rplVersion <= 3 && aValue == WritingModes.Rotate270)
			{
				return 0;
			}
			return StyleEnumConverter.Translate(aValue);
		}

		public static byte Translate(UnicodeBiDiTypes aValue)
		{
			switch (aValue)
			{
			case UnicodeBiDiTypes.BiDiOverride:
				return 2;
			case UnicodeBiDiTypes.Embed:
				return 1;
			default:
				return 0;
			}
		}

		public static byte Translate(Calendars aValue)
		{
			switch (aValue)
			{
			case Calendars.GregorianArabic:
				return 1;
			case Calendars.GregorianMiddleEastFrench:
				return 2;
			case Calendars.GregorianTransliteratedEnglish:
				return 3;
			case Calendars.GregorianTransliteratedFrench:
				return 4;
			case Calendars.GregorianUSEnglish:
				return 5;
			case Calendars.Hebrew:
				return 6;
			case Calendars.Hijri:
				return 7;
			case Calendars.Japanese:
				return 8;
			case Calendars.Julian:
				return 10;
			case Calendars.Korean:
				return 9;
			case Calendars.Taiwan:
				return 11;
			case Calendars.ThaiBuddhist:
				return 12;
			default:
				return 0;
			}
		}

		public static byte? Translate(BorderStyles aValue)
		{
			switch (aValue)
			{
			case BorderStyles.Dashed:
				return 2;
			case BorderStyles.Dotted:
				return 1;
			case BorderStyles.Double:
				return 4;
			case BorderStyles.Solid:
				return 3;
			case BorderStyles.Default:
				return null;
			default:
				return 0;
			}
		}

		public static byte Translate(BackgroundRepeatTypes aValue)
		{
			switch (aValue)
			{
			case BackgroundRepeatTypes.Clip:
				return 1;
			case BackgroundRepeatTypes.RepeatX:
				return 2;
			case BackgroundRepeatTypes.RepeatY:
				return 3;
			default:
				return 0;
			}
		}

		public static byte Translate(ListStyle listStyle)
		{
			switch (listStyle)
			{
			case ListStyle.Bulleted:
				return 2;
			case ListStyle.Numbered:
				return 1;
			default:
				return 0;
			}
		}

		public static byte Translate(MarkupType markupType)
		{
			switch (markupType)
			{
			case MarkupType.HTML:
				return 1;
			default:
				return 0;
			}
		}
	}
}

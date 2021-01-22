using AspNetCore.ReportingServices.Common;
using AspNetCore.ReportingServices.OnDemandReportRendering;
using AspNetCore.ReportingServices.ReportIntermediateFormat;
using AspNetCore.ReportingServices.ReportProcessing;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace AspNetCore.ReportingServices.ReportPublishing
{
	public sealed class ProcessingValidator
	{
		private ProcessingValidator()
		{
		}

		public static string ValidateColor(string color, IErrorContext errorContext, bool allowTransparency)
		{
			if (color == null)
			{
				return null;
			}
			string result = default(string);
			if (Validator.ValidateColor(color, out result, allowTransparency))
			{
				return result;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidColor, Severity.Warning, color);
			return null;
		}

		public static string ValidateSize(string size, IErrorContext errorContext)
		{
			return ProcessingValidator.ValidateSize(size, false, errorContext);
		}

		public static string ValidateSize(string size, bool allowNegative, IErrorContext errorContext)
		{
			return ProcessingValidator.ValidateSize(size, -1.7976931348623157E+308, 1.7976931348623157E+308, allowNegative, errorContext);
		}

		public static string ValidateBorderWidth(string size, IErrorContext errorContext)
		{
			return ProcessingValidator.ValidateSize(size, Validator.BorderWidthMin, Validator.BorderWidthMax, false, errorContext);
		}

		public static string ValidateFontSize(string size, IErrorContext errorContext)
		{
			return ProcessingValidator.ValidateSize(size, Validator.FontSizeMin, Validator.FontSizeMax, false, errorContext);
		}

		public static string ValidatePadding(string size, IErrorContext errorContext)
		{
			return ProcessingValidator.ValidateSize(size, Validator.PaddingMin, Validator.PaddingMax, false, errorContext);
		}

		public static string ValidateLineHeight(string size, IErrorContext errorContext)
		{
			return ProcessingValidator.ValidateSize(size, Validator.LineHeightMin, Validator.LineHeightMax, false, errorContext);
		}

		private static string ValidateSize(string size, double minValue, double maxValue, bool allowNegative, IErrorContext errorContext)
		{
			if (size == null)
			{
				return null;
			}
			RVUnit rVUnit = default(RVUnit);
			if (!Validator.ValidateSizeString(size, out rVUnit))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidSize, Severity.Warning, size);
				return null;
			}
			if (!Validator.ValidateSizeUnitType(rVUnit))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidMeasurementUnit, Severity.Warning, rVUnit.Type.ToString());
				return null;
			}
			if (!allowNegative && !Validator.ValidateSizeIsPositive(rVUnit))
			{
				errorContext.Register(ProcessingErrorCode.rsNegativeSize, Severity.Warning);
				return null;
			}
			double sizeInMM = Converter.ConvertToMM(rVUnit);
			if (!Validator.ValidateSizeValue(sizeInMM, minValue, maxValue))
			{
				errorContext.Register(ProcessingErrorCode.rsOutOfRangeSize, Severity.Warning, size, Converter.ConvertSizeFromMM(minValue, rVUnit.Type), Converter.ConvertSizeFromMM(maxValue, rVUnit.Type));
				return null;
			}
			return size;
		}

		public static string ValidateEmbeddedImageName(string embeddedImageName, Dictionary<string, AspNetCore.ReportingServices.ReportIntermediateFormat.ImageInfo> embeddedImages, IErrorContext errorContext)
		{
			if (embeddedImageName == null)
			{
				return null;
			}
			if (!Validator.ValidateEmbeddedImageName(embeddedImageName, embeddedImages))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidEmbeddedImageProperty, Severity.Warning, embeddedImageName);
				return null;
			}
			return embeddedImageName;
		}

		public static string ValidateEmbeddedImageName(string embeddedImageName, Dictionary<string, AspNetCore.ReportingServices.ReportIntermediateFormat.ImageInfo> embeddedImages, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			if (embeddedImageName == null)
			{
				return null;
			}
			if (!Validator.ValidateEmbeddedImageName(embeddedImageName, embeddedImages))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidEmbeddedImageProperty, Severity.Warning, objectType, objectName, propertyName, embeddedImageName);
				return null;
			}
			return embeddedImageName;
		}

		public static string ValidateLanguage(string language, IErrorContext errorContext, out CultureInfo culture)
		{
			if (Validator.ValidateLanguage(language, out culture))
			{
				return language;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidLanguage, Severity.Warning, language);
			return null;
		}

		public static string ValidateSpecificLanguage(string language, IErrorContext errorContext, out CultureInfo culture)
		{
			if (Validator.ValidateSpecificLanguage(language, out culture))
			{
				return language;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidLanguage, Severity.Warning, language);
			return null;
		}

		public static object ValidateNumeralVariant(int numeralVariant, IErrorContext errorContext)
		{
			if (Validator.ValidateNumeralVariant(numeralVariant))
			{
				return numeralVariant;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidNumeralVariant, Severity.Warning, numeralVariant.ToString(CultureInfo.InvariantCulture));
			return null;
		}

		public static string ValidateMimeType(string mimeType, IErrorContext errorContext)
		{
			if (Validator.ValidateMimeType(mimeType))
			{
				return mimeType;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidMIMEType, Severity.Warning, mimeType);
			return null;
		}

		public static string ValidateMimeType(string mimeType, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			if (Validator.ValidateMimeType(mimeType))
			{
				return mimeType;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidMIMEType, Severity.Warning, objectType, objectName, propertyName, mimeType);
			return null;
		}

		public static string ValidateBackgroundHatchType(string backgroundHatchType, IErrorContext errorContext)
		{
			if (Validator.ValidateBackgroundHatchType(backgroundHatchType))
			{
				return backgroundHatchType;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidBackgroundHatchType, Severity.Warning, backgroundHatchType);
			return null;
		}

		public static string ValidateTextEffect(string textEffect, IErrorContext errorContext)
		{
			if (Validator.ValidateTextEffect(textEffect))
			{
				return textEffect;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidTextEffect, Severity.Warning, textEffect);
			return null;
		}

		public static string ValidateBorderStyle(string borderStyle, ObjectType objectType, bool isDynamicImageSubElement, IErrorContext errorContext, bool isDefaultBorder)
		{
			string result = default(string);
			if (!Validator.ValidateBorderStyle(borderStyle, isDefaultBorder, objectType, isDynamicImageSubElement, out result))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidBorderStyle, Severity.Warning, borderStyle);
				result = null;
				return result;
			}
			return result;
		}

		public static string ValidateBackgroundGradientType(string gradientType, IErrorContext errorContext)
		{
			if (Validator.ValidateBackgroundGradientType(gradientType))
			{
				return gradientType;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidBackgroundGradientType, Severity.Warning, gradientType);
			return null;
		}

		public static string ValidateFontStyle(string fontStyle, IErrorContext errorContext)
		{
			if (Validator.ValidateFontStyle(fontStyle))
			{
				return fontStyle;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidFontStyle, Severity.Warning, fontStyle);
			return null;
		}

		public static string ValidateFontWeight(string fontWeight, IErrorContext errorContext)
		{
			if (Validator.ValidateFontWeight(fontWeight))
			{
				return fontWeight;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidFontWeight, Severity.Warning, fontWeight);
			return null;
		}

		public static string ValidateTextDecoration(string textDecoration, IErrorContext errorContext)
		{
			if (Validator.ValidateTextDecoration(textDecoration))
			{
				return textDecoration;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidTextDecoration, Severity.Warning, textDecoration);
			return null;
		}

		public static string ValidateTextAlign(string textAlign, IErrorContext errorContext)
		{
			if (Validator.ValidateTextAlign(textAlign))
			{
				return textAlign;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidTextAlign, Severity.Warning, textAlign);
			return null;
		}

		public static string ValidateVerticalAlign(string verticalAlign, IErrorContext errorContext)
		{
			if (Validator.ValidateVerticalAlign(verticalAlign))
			{
				return verticalAlign;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidVerticalAlign, Severity.Warning, verticalAlign);
			return null;
		}

		public static string ValidateDirection(string direction, IErrorContext errorContext)
		{
			if (Validator.ValidateDirection(direction))
			{
				return direction;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidDirection, Severity.Warning, direction);
			return null;
		}

		public static string ValidateWritingMode(string writingMode, IErrorContext errorContext)
		{
			if (Validator.ValidateWritingMode(writingMode))
			{
				return writingMode;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidWritingMode, Severity.Warning, writingMode);
			return null;
		}

		public static string ValidateUnicodeBiDi(string unicodeBiDi, IErrorContext errorContext)
		{
			if (Validator.ValidateUnicodeBiDi(unicodeBiDi))
			{
				return unicodeBiDi;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidUnicodeBiDi, Severity.Warning, unicodeBiDi);
			return null;
		}

		public static string ValidateCalendar(string calendar, IErrorContext errorContext)
		{
			if (Validator.ValidateCalendar(calendar))
			{
				return calendar;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidCalendar, Severity.Warning, calendar);
			return null;
		}

		public static object ValidateCustomStyle(string styleName, object styleValue, IErrorContext errorContext)
		{
			return ProcessingValidator.ValidateCustomStyle(styleName, styleValue, ObjectType.Image, errorContext);
		}

		public static object ValidateCustomStyle(string styleName, object styleValue, ObjectType objectType, IErrorContext errorContext)
		{
			CultureInfo cultureInfo = default(CultureInfo);
			switch (styleName)
			{
			case "BorderColor":
			case "BorderColorLeft":
			case "BorderColorRight":
			case "BorderColorTop":
			case "BorderColorBottom":
				return ProcessingValidator.ValidateColor(styleValue as string, errorContext, Validator.IsDynamicImageReportItem(objectType));
			case "BorderStyle":
				return ProcessingValidator.ValidateBorderStyle(styleValue as string, objectType, false, errorContext, true);
			case "BorderStyleLeft":
			case "BorderStyleRight":
			case "BorderStyleTop":
			case "BorderStyleBottom":
				return ProcessingValidator.ValidateBorderStyle(styleValue as string, objectType, false, errorContext, false);
			case "BorderWidth":
			case "BorderWidthLeft":
			case "BorderWidthRight":
			case "BorderWidthTop":
			case "BorderWidthBottom":
				return ProcessingValidator.ValidateSize((styleValue as ReportSize).ToString(), Validator.BorderWidthMin, Validator.BorderWidthMax, false, errorContext);
			case "Color":
			case "BackgroundColor":
			case "BackgroundGradientEndColor":
				return ProcessingValidator.ValidateColor(styleValue as string, errorContext, Validator.IsDynamicImageReportItem(objectType));
			case "BackgroundGradientType":
				return ProcessingValidator.ValidateBackgroundGradientType(styleValue as string, errorContext);
			case "FontStyle":
				return ProcessingValidator.ValidateFontStyle(styleValue as string, errorContext);
			case "FontFamily":
				return styleValue as string;
			case "FontSize":
				return ProcessingValidator.ValidateSize((styleValue as ReportSize).ToString(), Validator.FontSizeMin, Validator.FontSizeMax, false, errorContext);
			case "FontWeight":
				return ProcessingValidator.ValidateFontWeight(styleValue as string, errorContext);
			case "Format":
				return styleValue as string;
			case "TextDecoration":
				return ProcessingValidator.ValidateTextDecoration(styleValue as string, errorContext);
			case "TextAlign":
				return ProcessingValidator.ValidateTextAlign(styleValue as string, errorContext);
			case "VerticalAlign":
				return ProcessingValidator.ValidateVerticalAlign(styleValue as string, errorContext);
			case "PaddingLeft":
			case "PaddingRight":
			case "PaddingTop":
			case "PaddingBottom":
				return ProcessingValidator.ValidateSize((styleValue as ReportSize).ToString(), Validator.PaddingMin, Validator.PaddingMax, false, errorContext);
			case "LineHeight":
				return ProcessingValidator.ValidateSize((styleValue as ReportSize).ToString(), Validator.LineHeightMin, Validator.LineHeightMax, false, errorContext);
			case "Direction":
				return ProcessingValidator.ValidateDirection(styleValue as string, errorContext);
			case "WritingMode":
				return ProcessingValidator.ValidateWritingMode(styleValue as string, errorContext);
			case "Language":
				return ProcessingValidator.ValidateSpecificLanguage(styleValue as string, errorContext, out cultureInfo);
			case "UnicodeBiDi":
				return ProcessingValidator.ValidateUnicodeBiDi(styleValue as string, errorContext);
			case "Calendar":
				return ProcessingValidator.ValidateCalendar(styleValue as string, errorContext);
			case "CurrencyLanguage":
				return ProcessingValidator.ValidateLanguage(styleValue as string, errorContext, out cultureInfo);
			case "NumeralLanguage":
				return ProcessingValidator.ValidateLanguage(styleValue as string, errorContext, out cultureInfo);
			case "NumeralVariant":
			{
				int numeralVariant = default(int);
				if (int.TryParse(styleValue as string, out numeralVariant))
				{
					return ProcessingValidator.ValidateNumeralVariant(numeralVariant, errorContext);
				}
				errorContext.Register(ProcessingErrorCode.rsInvalidNumeralVariant, Severity.Warning, styleValue as string);
				return null;
			}
			default:
				Global.Tracer.Assert(false);
				break;
			case "BackgroundImageSource":
			case "BackgroundImageValue":
			case "BackgroundImageMIMEType":
			case "BackgroundRepeat":
				break;
			}
			return null;
		}

		public static string ValidateTextRunMarkupType(string value, IErrorContext errorContext)
		{
			if (Validator.ValidateTextRunMarkupType(value))
			{
				return value;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidMarkupType, Severity.Warning, value);
			return "None";
		}

		public static string ValidateParagraphListStyle(string value, IErrorContext errorContext)
		{
			if (Validator.ValidateParagraphListStyle(value))
			{
				return value;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidListStyle, Severity.Warning, value);
			return "None";
		}

		public static int? ValidateParagraphListLevel(int value, IErrorContext errorContext)
		{
			int? result = default(int?);
			if (!Validator.ValidateParagraphListLevel(value, out result))
			{
				errorContext.Register(ProcessingErrorCode.rsOutOfRangeSize, Severity.Warning, Convert.ToString(value, CultureInfo.InvariantCulture), Convert.ToString(0, CultureInfo.InvariantCulture), Convert.ToString(9, CultureInfo.InvariantCulture));
				return result;
			}
			return value;
		}
	}
}

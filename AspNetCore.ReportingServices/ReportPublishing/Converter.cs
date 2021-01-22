using AspNetCore.ReportingServices.Common;
using AspNetCore.ReportingServices.ReportProcessing;
using System;
using System.Globalization;

namespace AspNetCore.ReportingServices.ReportPublishing
{
	public sealed class Converter
	{
		public const double Inches455 = 11557.0;

		public const double Pt1 = 0.35277777777777775;

		public const double Pc1 = 4.2333333333333325;

		public const double Pt200 = 70.555555555555543;

		public const double PtPoint25 = 0.08814;

		public const double Pt20 = 7.0555555555555554;

		public const double Pt1000 = 352.77777777777777;

		public const string FullDoubleFormatCode = "0.###############";

		private Converter()
		{
		}

		public static string ConvertSize(double size)
		{
			return size.ToString("0.###############", CultureInfo.InvariantCulture) + "mm";
		}

		public static string ConvertSizeFromMM(double sizeValue, RVUnitType unitType)
		{
			string str = "mm";
			switch (unitType)
			{
			case RVUnitType.Cm:
				sizeValue /= 10.0;
				str = "cm";
				break;
			case RVUnitType.Inch:
				sizeValue /= 25.4;
				str = "in";
				break;
			case RVUnitType.Pica:
				sizeValue /= 4.2333333333333325;
				str = "pc";
				break;
			case RVUnitType.Point:
				sizeValue /= 0.35277777777777775;
				str = "pt";
				break;
			default:
				unitType = RVUnitType.Mm;
				break;
			}
			return Math.Round(sizeValue, 5).ToString(CultureInfo.InvariantCulture) + str;
		}

		public static double ConvertToMM(RVUnit unit)
		{
			if (!Validator.ValidateSizeUnitType(unit))
			{
				Global.Tracer.Assert(false);
			}
			return unit.ToMillimeters();
		}
	}
}

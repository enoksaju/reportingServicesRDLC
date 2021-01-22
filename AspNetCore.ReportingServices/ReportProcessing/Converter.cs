using AspNetCore.ReportingServices.Common;
using System.Globalization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	public sealed class Converter
	{
		public static double Inches160 = 4064.0;

		public static double Pt1 = 0.3528;

		public static double Pt200 = 70.56;

		public static double PtPoint25 = 0.08814;

		public static double Pt20 = 7.056;

		public static double Pt1000 = 352.8;

		private Converter()
		{
		}

		public static string ConvertSize(double size)
		{
			return size.ToString(CultureInfo.InvariantCulture) + "mm";
		}

		public static double ConvertToMM(RVUnit unit)
		{
			double num = unit.Value;
			switch (unit.Type)
			{
			case RVUnitType.Cm:
				num *= 10.0;
				break;
			case RVUnitType.Inch:
				num *= 25.4;
				break;
			case RVUnitType.Pica:
				num *= 4.2333;
				break;
			case RVUnitType.Point:
				num *= 0.3528;
				break;
			default:
				Global.Tracer.Assert(false);
				break;
			case RVUnitType.Mm:
				break;
			}
			return num;
		}
	}
}

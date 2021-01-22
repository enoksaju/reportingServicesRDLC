using System.Collections;
using System.Text.RegularExpressions;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer
{
	public sealed class FormulaHandler
	{
		public enum GlobalExpressionType
		{
			PageNumber,
			TotalPages,
			ReportName,
			Unknown
		}

		public const string GLOBALS_PAGENUMBER = "PAGENUMBER";

		public const string GLOBALS_TOTALPAGES = "TOTALPAGES";

		public const string GLOBALS_OVERALLPAGENUMBER = "OVERALLPAGENUMBER";

		public const string GLOBALS_OVERALLTOTALPAGES = "OVERALLTOTALPAGES";

		public const string GLOBALS_REPORTNAME = "REPORTNAME";

		private static Regex m_RegexGlobalOnly;

		private static Regex m_RegexAmpDetection;

		private static RegexOptions m_regexOptions;

		static FormulaHandler()
		{
			FormulaHandler.m_RegexGlobalOnly = null;
			FormulaHandler.m_RegexAmpDetection = null;
			FormulaHandler.InitRegularExpressions();
		}

		private static void InitRegularExpressions()
		{
			FormulaHandler.m_regexOptions = (RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);
			string text = "(\"((\"\")|[^\"])*\")";
			string text2 = "Globals";
			string text3 = "Item";
			string text4 = "&";
			string str = Regex.Escape("!");
			string str2 = Regex.Escape(".");
			string text5 = "[" + str + str2 + "]";
			string text6 = Regex.Escape("(");
			string text7 = Regex.Escape(")");
			string str3 = text2 + "(" + text5 + ")?(" + text3 + ")?(" + text6 + ")?(?<GlobalParameterName>(\\s*" + text + "\\s*)|[\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Pc}][\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Pc}\\p{Nd}\\p{Mn}\\p{Mc}\\p{Cf}]*)(" + text7 + ")?";
			FormulaHandler.m_RegexGlobalOnly = new Regex("^\\s*" + str3 + "\\s*$", FormulaHandler.m_regexOptions);
			FormulaHandler.m_RegexAmpDetection = new Regex("\\s*" + text + "\\s*|(?<detected>" + text4 + ")", FormulaHandler.m_regexOptions);
		}

		public FormulaHandler()
		{
		}

		public static ArrayList ProcessHeaderFooterFormula(string formulaExpression)
		{
			if (formulaExpression == null)
			{
				return null;
			}
			ArrayList arrayList = new ArrayList();
			string text = null;
			MatchCollection matchCollection = FormulaHandler.m_RegexAmpDetection.Matches(formulaExpression, 0);
			if (matchCollection == null || matchCollection.Count == 0)
			{
				text = formulaExpression;
			}
			else
			{
				int num = 0;
				string text2 = null;
				string text3 = null;
				for (int i = 0; i <= matchCollection.Count; i++)
				{
					text3 = ((i >= matchCollection.Count) ? formulaExpression.Substring(num, formulaExpression.Length - num) : formulaExpression.Substring(num, matchCollection[i].Index - num));
					text3 = text3.Trim();
					if (text3.Length > 0)
					{
						text = ((text != null) ? (text + "&" + text3) : text3);
					}
					if (i < matchCollection.Count)
					{
						text3 = formulaExpression.Substring(matchCollection[i].Index, matchCollection[i].Length);
						num = matchCollection[i].Index + matchCollection[i].Length;
						if (!(text3 == "&"))
						{
							text2 = text3.Trim();
							int length = text2.Length;
							if (length > 1 && text2[0] == '"' && text2[length - 1] == '"')
							{
								text2 = text2.Substring(1, length - 2);
							}
							if (text != null)
							{
								Match match = FormulaHandler.m_RegexGlobalOnly.Match(text);
								if (match.Success)
								{
									GlobalExpressionType globalExpressionType = FormulaHandler.WordHeaderFooterFormula(match, text);
									if (globalExpressionType == GlobalExpressionType.Unknown)
									{
										return null;
									}
									arrayList.Add(globalExpressionType);
									text = null;
									goto IL_0174;
								}
								return null;
							}
							goto IL_0174;
						}
					}
					continue;
					IL_0174:
					arrayList.Add(text2);
				}
			}
			if (text != null)
			{
				Match match2 = FormulaHandler.m_RegexGlobalOnly.Match(text);
				if (match2.Success)
				{
					GlobalExpressionType globalExpressionType2 = FormulaHandler.WordHeaderFooterFormula(match2, text);
					if (globalExpressionType2 == GlobalExpressionType.Unknown)
					{
						return null;
					}
					arrayList.Add(globalExpressionType2);
					goto IL_01cc;
				}
				return null;
			}
			goto IL_01cc;
			IL_01cc:
			return arrayList;
		}

		private static GlobalExpressionType WordHeaderFooterFormula(Match match, string formulaExpression)
		{
			GlobalExpressionType result = GlobalExpressionType.Unknown;
			string text = match.Result("${GlobalParameterName}");
			text = text.Replace("\"", string.Empty);
			if (text != null && text.Length != 0)
			{
				switch (text.Trim().ToUpperInvariant())
				{
				case "PAGENUMBER":
				case "OVERALLPAGENUMBER":
					result = GlobalExpressionType.PageNumber;
					break;
				case "TOTALPAGES":
				case "OVERALLTOTALPAGES":
					result = GlobalExpressionType.TotalPages;
					break;
				case "REPORTNAME":
					result = GlobalExpressionType.ReportName;
					break;
				default:
					result = GlobalExpressionType.Unknown;
					break;
				}
			}
			return result;
		}
	}
}

using System.Text.RegularExpressions;

namespace AspNetCore.ReportingServices.RdlObjectModel2005.Upgrade
{
	public sealed class ReportRegularExpressions
	{
		private const string m_identifierStart = "\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}";

		private const string m_identifierExtend = "\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}";

		public const string ClsReplacerPattern = "[^\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]";

		public Regex Whitespace;

		public Regex NonConstant;

		public Regex FieldDetection;

		public Regex ReportItemsDetection;

		public Regex ParametersDetection;

		public Regex PageGlobalsDetection;

		public Regex AggregatesDetection;

		public Regex UserDetection;

		public Regex DataSetsDetection;

		public Regex DataSourcesDetection;

		public Regex VariablesDetection;

		public Regex MeDotValueExpression;

		public Regex MeDotValueDetection;

		public Regex IllegalCharacterDetection;

		public Regex LineTerminatorDetection;

		public Regex FieldOnly;

		public Regex ParameterOnly;

		public Regex StringLiteralOnly;

		public Regex NothingOnly;

		public Regex ReportItemName;

		public Regex FieldName;

		public Regex ParameterName;

		public Regex DataSetName;

		public Regex DataSourceName;

		public Regex SpecialFunction;

		public Regex PSAFunction;

		public Regex Arguments;

		public Regex DynamicFieldReference;

		public Regex DynamicFieldPropertyReference;

		public Regex StaticFieldPropertyReference;

		public Regex RewrittenCommandText;

		public Regex SimpleDynamicFieldReference;

		public Regex SimpleDynamicReportItemReference;

		public Regex SimpleDynamicVariableReference;

		public Regex ReportItemValueReference;

		public Regex VariableName;

		public Regex ClsIdentifierRegex;

		public static readonly ReportRegularExpressions Value = new ReportRegularExpressions();

		private ReportRegularExpressions()
		{
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline;
			this.NonConstant = new Regex("^\\s*=", options);
			string text = Regex.Escape("-+()#,:&*/\\^<=>");
			string text2 = Regex.Escape("!");
			string text3 = Regex.Escape(".");
			string text4 = "[" + text2 + text3 + "]";
			string text5 = "(^|[" + text + "\\s])";
			string text6 = "($|[" + text + text2 + text3 + "\\s])";
			this.Whitespace = new Regex("\\s+", options);
			this.FieldDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>Fields)" + text6, options);
			this.ReportItemsDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>ReportItems)" + text6, options);
			this.ParametersDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>Parameters)" + text6, options);
			this.PageGlobalsDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>(Globals" + text4 + "PageNumber)|(Globals" + text4 + "TotalPages))" + text6, options);
			this.AggregatesDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>Aggregates)" + text6, options);
			this.UserDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>User)" + text6, options);
			this.DataSetsDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>DataSets)" + text6, options);
			this.DataSourcesDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>DataSources)" + text6, options);
			this.VariablesDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>Variables)" + text6, options);
			this.MeDotValueDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>(?:Me.)?Value)" + text6, options);
			this.MeDotValueExpression = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<medotvalue>(Me" + text3 + ")?Value)*" + text6, options);
			string text7 = Regex.Escape(":");
			string text8 = Regex.Escape("#");
			string text9 = "(" + text8 + "[^" + text8 + "]*" + text8 + ")";
			string text10 = Regex.Escape(":=");
			this.LineTerminatorDetection = new Regex("(?<detected>(\\u000D\\u000A)|([\\u000D\\u000A\\u2028\\u2029]))", options);
			this.IllegalCharacterDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text9 + "|" + text10 + "|(?<detected>" + text7 + ")", options);
			string text11 = "[\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Pc}][\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Pc}\\p{Nd}\\p{Mn}\\p{Mc}\\p{Cf}]*";
			string text12 = "ReportItems" + text2 + "(?<reportitemname>" + text11 + ")";
			string text13 = "Fields" + text2 + "(?<fieldname>" + text11 + ")";
			string text14 = "Parameters" + text2 + "(?<parametername>" + text11 + ")";
			string text15 = "DataSets" + text2 + "(?<datasetname>" + text11 + ")";
			string str = "DataSources" + text2 + "(?<datasourcename>" + text11 + ")";
			string str2 = "Variables" + text2 + "(?<variablename>" + text11 + ")";
			string text16 = "(?<detected>(ReportItems(" + text3 + "Item)?" + Regex.Escape("(") + "[ \t]*" + Regex.Escape("\"") + "(?<reportitemname>" + text11 + ")" + Regex.Escape("\"") + "[ \t]*" + Regex.Escape(")") + "))";
			this.SimpleDynamicReportItemReference = new Regex(text5 + text16, options);
			this.SimpleDynamicVariableReference = new Regex(text5 + "(?<detected>(Variables(" + text3 + "Item)?" + Regex.Escape("(") + "[ \t]*" + Regex.Escape("\"") + "(?<variablename>" + text11 + ")" + Regex.Escape("\"") + "[ \t]*" + Regex.Escape(")") + "))", options);
			this.SimpleDynamicFieldReference = new Regex(text5 + "(?<detected>(Fields(" + text3 + "Item)?" + Regex.Escape("(") + "[ \t]*" + Regex.Escape("\"") + "(?<fieldname>" + text11 + ")" + Regex.Escape("\"") + "[ \t]*" + Regex.Escape(")") + "))", options);
			this.DynamicFieldReference = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>(Fields(" + text3 + "Item)?" + Regex.Escape("(") + "))", options);
			this.DynamicFieldPropertyReference = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + text13 + Regex.Escape("("), options);
			this.StaticFieldPropertyReference = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + text13 + text3 + "(?<propertyname>" + text11 + ")", options);
			this.FieldOnly = new Regex("^\\s*" + text13 + text3 + "Value\\s*$", options);
			this.RewrittenCommandText = new Regex("^\\s*" + text15 + text3 + "RewrittenCommandText\\s*$", options);
			this.ParameterOnly = new Regex("^\\s*" + text14 + text3 + "Value\\s*$", options);
			this.StringLiteralOnly = new Regex("^\\s*\"(?<string>((\"\")|[^\"])*)\"\\s*$", options);
			this.NothingOnly = new Regex("^\\s*Nothing\\s*$", options);
			this.ReportItemName = new Regex(text5 + text12, options);
			this.FieldName = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + text13, options);
			this.ParameterName = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + text14, options);
			this.DataSetName = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + text15, options);
			this.DataSourceName = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + str, options);
			this.VariableName = new Regex(text5 + str2, options);
			this.SpecialFunction = new Regex("(\"((\"\")|[^\"])*\")|(?<prefix>" + text5 + ")(?<sfname>RunningValue|RowNumber|First|Last|Previous|Sum|Avg|Max|Min|CountDistinct|Count|CountRows|StDevP|VarP|StDev|Var|Aggregate)\\s*\\(", options);
			string text17 = Regex.Escape("(");
			string text18 = Regex.Escape(")");
			this.PSAFunction = new Regex("(\"((\"\")|[^\"])*\")|(?<prefix>" + text5 + ")(?<psaname>RunningValue|First|Last|Previous)\\s*\\(", options);
			string text19 = Regex.Escape(",");
			this.Arguments = new Regex("(\"((\"\")|[^\"])*\")|(?<openParen>" + text17 + ")|(?<closeParen>" + text18 + ")|(?<comma>" + text19 + ")", options);
			this.ReportItemValueReference = new Regex("((" + text12 + ")|" + text16 + ")" + text3 + "Value");
			this.ClsIdentifierRegex = new Regex("^[\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}][\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]*$", options);
		}
	}
}

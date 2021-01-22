using AspNetCore.ReportingServices.ReportProcessing.ExprHostObjectModel;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Globalization;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	public sealed class ExprHostBuilder
	{
		public enum ErrorSource
		{
			Expression,
			CodeModuleClassInstanceDecl,
			CustomCode,
			Unknown
		}

		private static class Constants
		{
			public const string ReportObjectModelNS = "AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel";

			public const string ExprHostObjectModelNS = "AspNetCore.ReportingServices.ReportProcessing.ExprHostObjectModel";

			public const string ReportExprHost = "ReportExprHost";

			public const string IndexedExprHost = "IndexedExprHost";

			public const string ReportParamExprHost = "ReportParamExprHost";

			public const string CalcFieldExprHost = "CalcFieldExprHost";

			public const string DataSourceExprHost = "DataSourceExprHost";

			public const string DataSetExprHost = "DataSetExprHost";

			public const string ReportItemExprHost = "ReportItemExprHost";

			public const string ActionExprHost = "ActionExprHost";

			public const string ActionInfoExprHost = "ActionInfoExprHost";

			public const string TextBoxExprHost = "TextBoxExprHost";

			public const string ImageExprHost = "ImageExprHost";

			public const string ParamExprHost = "ParamExprHost";

			public const string SubreportExprHost = "SubreportExprHost";

			public const string ActiveXControlExprHost = "ActiveXControlExprHost";

			public const string SortingExprHost = "SortingExprHost";

			public const string FilterExprHost = "FilterExprHost";

			public const string GroupingExprHost = "GroupingExprHost";

			public const string ListExprHost = "ListExprHost";

			public const string TableGroupExprHost = "TableGroupExprHost";

			public const string TableExprHost = "TableExprHost";

			public const string MatrixDynamicGroupExprHost = "MatrixDynamicGroupExprHost";

			public const string MatrixExprHost = "MatrixExprHost";

			public const string ChartExprHost = "ChartExprHost";

			public const string OWCChartExprHost = "OWCChartExprHost";

			public const string StyleExprHost = "StyleExprHost";

			public const string AggregateParamExprHost = "AggregateParamExprHost";

			public const string MultiChartExprHost = "MultiChartExprHost";

			public const string ChartDynamicGroupExprHost = "ChartDynamicGroupExprHost";

			public const string ChartDataPointExprHost = "ChartDataPointExprHost";

			public const string ChartTitleExprHost = "ChartTitleExprHost";

			public const string AxisExprHost = "AxisExprHost";

			public const string DataValueExprHost = "DataValueExprHost";

			public const string CustomReportItemExprHost = "CustomReportItemExprHost";

			public const string DataGroupingExprHost = "DataGroupingExprHost";

			public const string DataCellExprHost = "DataCellExprHost";

			public const string ParametersOnlyParam = "parametersOnly";

			public const string CustomCodeProxy = "CustomCodeProxy";

			public const string CustomCodeProxyBase = "CustomCodeProxyBase";

			public const string ReportObjectModelParam = "reportObjectModel";

			public const string SetReportObjectModel = "SetReportObjectModel";

			public const string Code = "Code";

			public const string CodeProxyBase = "m_codeProxyBase";

			public const string CodeParam = "code";

			public const string Report = "Report";

			public const string RemoteArrayWrapper = "RemoteArrayWrapper";

			public const string LabelExpr = "LabelExpr";

			public const string ValueExpr = "ValueExpr";

			public const string NoRowsExpr = "NoRowsExpr";

			public const string ParameterHosts = "m_parameterHostsRemotable";

			public const string IndexParam = "index";

			public const string FilterHosts = "m_filterHostsRemotable";

			public const string SortingHost = "SortingHost";

			public const string GroupingHost = "GroupingHost";

			public const string SubgroupHost = "SubgroupHost";

			public const string VisibilityHiddenExpr = "VisibilityHiddenExpr";

			public const string SortDirectionHosts = "SortDirectionHosts";

			public const string DataValueHosts = "m_dataValueHostsRemotable";

			public const string CustomPropertyHosts = "m_customPropertyHostsRemotable";

			public const string ReportLanguageExpr = "ReportLanguageExpr";

			public const string AggregateParamHosts = "m_aggregateParamHostsRemotable";

			public const string ReportParameterHosts = "m_reportParameterHostsRemotable";

			public const string DataSourceHosts = "m_dataSourceHostsRemotable";

			public const string DataSetHosts = "m_dataSetHostsRemotable";

			public const string PageSectionHosts = "m_pageSectionHostsRemotable";

			public const string LineHosts = "m_lineHostsRemotable";

			public const string RectangleHosts = "m_rectangleHostsRemotable";

			public const string TextBoxHosts = "m_textBoxHostsRemotable";

			public const string ImageHosts = "m_imageHostsRemotable";

			public const string SubreportHosts = "m_subreportHostsRemotable";

			public const string ActiveXControlHosts = "m_activeXControlHostsRemotable";

			public const string ListHosts = "m_listHostsRemotable";

			public const string TableHosts = "m_tableHostsRemotable";

			public const string MatrixHosts = "m_matrixHostsRemotable";

			public const string ChartHosts = "m_chartHostsRemotable";

			public const string OWCChartHosts = "m_OWCChartHostsRemotable";

			public const string CustomReportItemHosts = "m_customReportItemHostsRemotable";

			public const string ConnectStringExpr = "ConnectStringExpr";

			public const string FieldHosts = "m_fieldHostsRemotable";

			public const string QueryParametersHost = "QueryParametersHost";

			public const string QueryCommandTextExpr = "QueryCommandTextExpr";

			public const string ValidValuesHost = "ValidValuesHost";

			public const string ValidValueLabelsHost = "ValidValueLabelsHost";

			public const string ValidationExpressionExpr = "ValidationExpressionExpr";

			public const string ActionInfoHost = "ActionInfoHost";

			public const string ActionHost = "ActionHost";

			public const string ActionItemHosts = "m_actionItemHostsRemotable";

			public const string BookmarkExpr = "BookmarkExpr";

			public const string ToolTipExpr = "ToolTipExpr";

			public const string ToggleImageInitialStateExpr = "ToggleImageInitialStateExpr";

			public const string UserSortExpressionsHost = "UserSortExpressionsHost";

			public const string MIMETypeExpr = "MIMETypeExpr";

			public const string OmitExpr = "OmitExpr";

			public const string HyperlinkExpr = "HyperlinkExpr";

			public const string DrillThroughReportNameExpr = "DrillThroughReportNameExpr";

			public const string DrillThroughParameterHosts = "m_drillThroughParameterHostsRemotable";

			public const string DrillThroughBookmakLinkExpr = "DrillThroughBookmarkLinkExpr";

			public const string BookmarkLinkExpr = "BookmarkLinkExpr";

			public const string FilterExpressionExpr = "FilterExpressionExpr";

			public const string ParentExpressionsHost = "ParentExpressionsHost";

			public const string SubGroupHost = "SubGroupHost";

			public const string SubtotalHost = "SubtotalHost";

			public const string RowGroupingsHost = "RowGroupingsHost";

			public const string ColumnGroupingsHost = "ColumnGroupingsHost";

			public const string SeriesGroupingsHost = "SeriesGroupingsHost";

			public const string CategoryGroupingsHost = "CategoryGroupingsHost";

			public const string MultiChartHost = "MultiChartHost";

			public const string HeadingLabelExpr = "HeadingLabelExpr";

			public const string ChartDataPointHosts = "m_chartDataPointHostsRemotable";

			public const string DataLabelValueExpr = "DataLabelValueExpr";

			public const string DataLabelStyleHost = "DataLabelStyleHost";

			public const string StyleHost = "StyleHost";

			public const string MarkerStyleHost = "MarkerStyleHost";

			public const string TitleHost = "TitleHost";

			public const string CaptionExpr = "CaptionExpr";

			public const string MajorGridLinesHost = "MajorGridLinesHost";

			public const string MinorGridLinesHost = "MinorGridLinesHost";

			public const string StaticRowLabelsHost = "StaticRowLabelsHost";

			public const string StaticColumnLabelsHost = "StaticColumnLabelsHost";

			public const string CategoryAxisHost = "CategoryAxisHost";

			public const string ValueAxisHost = "ValueAxisHost";

			public const string LegendHost = "LegendHost";

			public const string PlotAreaHost = "PlotAreaHost";

			public const string AxisMinExpr = "AxisMinExpr";

			public const string AxisMaxExpr = "AxisMaxExpr";

			public const string AxisCrossAtExpr = "AxisCrossAtExpr";

			public const string AxisMajorIntervalExpr = "AxisMajorIntervalExpr";

			public const string AxisMinorIntervalExpr = "AxisMinorIntervalExpr";

			public const string TableGroupsHost = "TableGroupsHost";

			public const string TableRowVisibilityHiddenExpressions = "TableRowVisibilityHiddenExpressions";

			public const string TableColumnVisibilityHiddenExpressions = "TableColumnVisibilityHiddenExpressions";

			public const string OWCChartColumnHosts = "OWCChartColumnHosts";

			public const string DataValueNameExpr = "DataValueNameExpr";

			public const string DataValueValueExpr = "DataValueValueExpr";

			public const string DataGroupingHosts = "m_dataGroupingHostsRemotable";

			public const string DataCellHosts = "m_dataCellHostsRemotable";
		}

		private abstract class TypeDecl
		{
			public CodeTypeDeclaration Type;

			public string BaseTypeName;

			public TypeDecl Parent;

			public CodeConstructor Constructor;

			public bool HasExpressions;

			public CodeExpressionCollection DataValues;

			protected bool m_setCode;

			public void NestedTypeAdd(string name, CodeTypeDeclaration nestedType)
			{
				this.ConstructorCreate();
				this.Type.Members.Add(nestedType);
				this.Constructor.Statements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), name), this.CreateTypeCreateExpression(nestedType.Name)));
			}

			public int NestedTypeColAdd(string name, string baseTypeName, ref CodeExpressionCollection initializers, CodeTypeDeclaration nestedType)
			{
				this.Type.Members.Add(nestedType);
				this.TypeColInit(name, baseTypeName, ref initializers);
				return initializers.Add(this.CreateTypeCreateExpression(nestedType.Name));
			}

			protected TypeDecl(string typeName, string baseTypeName, TypeDecl parent, bool setCode)
			{
				this.BaseTypeName = baseTypeName;
				this.Parent = parent;
				this.m_setCode = setCode;
				this.Type = this.CreateType(typeName, baseTypeName);
			}

			protected void ConstructorCreate()
			{
				if (this.Constructor == null)
				{
					this.Constructor = this.CreateConstructor();
					this.Type.Members.Add(this.Constructor);
				}
			}

			protected virtual CodeConstructor CreateConstructor()
			{
				CodeConstructor codeConstructor = new CodeConstructor();
				codeConstructor.Attributes = MemberAttributes.Public;
				return codeConstructor;
			}

			protected CodeAssignStatement CreateTypeColInitStatement(string name, string baseTypeName, ref CodeExpressionCollection initializers)
			{
				CodeObjectCreateExpression codeObjectCreateExpression = new CodeObjectCreateExpression();
				codeObjectCreateExpression.CreateType = new CodeTypeReference("RemoteArrayWrapper", new CodeTypeReference(baseTypeName));
				if (initializers != null)
				{
					codeObjectCreateExpression.Parameters.AddRange(initializers);
				}
				initializers = codeObjectCreateExpression.Parameters;
				return new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), name), codeObjectCreateExpression);
			}

			protected virtual CodeTypeDeclaration CreateType(string name, string baseType)
			{
				Global.Tracer.Assert(name != null);
				CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(name);
				if (baseType != null)
				{
					codeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(baseType));
				}
				codeTypeDeclaration.Attributes = (MemberAttributes)24578;
				return codeTypeDeclaration;
			}

			private void TypeColInit(string name, string baseTypeName, ref CodeExpressionCollection initializers)
			{
				this.ConstructorCreate();
				if (initializers == null)
				{
					this.Constructor.Statements.Add(this.CreateTypeColInitStatement(name, baseTypeName, ref initializers));
				}
			}

			private CodeObjectCreateExpression CreateTypeCreateExpression(string typeName)
			{
				if (this.m_setCode)
				{
					return new CodeObjectCreateExpression(typeName, new CodeArgumentReferenceExpression("Code"));
				}
				return new CodeObjectCreateExpression(typeName);
			}
		}

		private sealed class RootTypeDecl : TypeDecl
		{
			public CodeExpressionCollection Aggregates;

			public CodeExpressionCollection PageSections;

			public CodeExpressionCollection ReportParameters;

			public CodeExpressionCollection DataSources;

			public CodeExpressionCollection DataSets;

			public CodeExpressionCollection Lines;

			public CodeExpressionCollection Rectangles;

			public CodeExpressionCollection TextBoxes;

			public CodeExpressionCollection Images;

			public CodeExpressionCollection Subreports;

			public CodeExpressionCollection ActiveXControls;

			public CodeExpressionCollection Lists;

			public CodeExpressionCollection Tables;

			public CodeExpressionCollection Matrices;

			public CodeExpressionCollection Charts;

			public CodeExpressionCollection OWCCharts;

			public CodeExpressionCollection CustomReportItems;

			public RootTypeDecl(bool setCode)
				: base("ReportExprHostImpl", "ReportExprHost", null, setCode)
			{
			}

			protected override CodeConstructor CreateConstructor()
			{
				CodeConstructor codeConstructor = base.CreateConstructor();
				codeConstructor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(bool), "parametersOnly"));
				CodeParameterDeclarationExpression value = new CodeParameterDeclarationExpression(typeof(object), "reportObjectModel");
				codeConstructor.Parameters.Add(value);
				codeConstructor.BaseConstructorArgs.Add(new CodeArgumentReferenceExpression("reportObjectModel"));
				this.ReportParameters = new CodeExpressionCollection();
				this.DataSources = new CodeExpressionCollection();
				this.DataSets = new CodeExpressionCollection();
				return codeConstructor;
			}

			protected override CodeTypeDeclaration CreateType(string name, string baseType)
			{
				CodeTypeDeclaration codeTypeDeclaration = base.CreateType(name, baseType);
				if (base.m_setCode)
				{
					CodeMemberField codeMemberField = new CodeMemberField("CustomCodeProxy", "Code");
					codeMemberField.Attributes = (MemberAttributes)20482;
					codeTypeDeclaration.Members.Add(codeMemberField);
				}
				return codeTypeDeclaration;
			}

			public void CompleteConstructorCreation()
			{
				if (base.HasExpressions)
				{
					if (base.Constructor == null)
					{
						base.ConstructorCreate();
					}
					else
					{
						CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
						codeConditionStatement.Condition = new CodeBinaryOperatorExpression(new CodeArgumentReferenceExpression("parametersOnly"), CodeBinaryOperatorType.ValueEquality, new CodePrimitiveExpression(true));
						if (this.ReportParameters.Count > 0)
						{
							codeConditionStatement.TrueStatements.Add(base.CreateTypeColInitStatement("m_reportParameterHostsRemotable", "ReportParamExprHost", ref this.ReportParameters));
						}
						codeConditionStatement.TrueStatements.Add(new CodeMethodReturnStatement());
						base.Constructor.Statements.Insert(0, codeConditionStatement);
						if (this.DataSources.Count > 0)
						{
							base.Constructor.Statements.Insert(0, base.CreateTypeColInitStatement("m_dataSourceHostsRemotable", "DataSourceExprHost", ref this.DataSources));
						}
						if (this.DataSets.Count > 0)
						{
							base.Constructor.Statements.Insert(0, base.CreateTypeColInitStatement("m_dataSetHostsRemotable", "DataSetExprHost", ref this.DataSets));
						}
						if (base.m_setCode)
						{
							base.Constructor.Statements.Insert(0, new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "m_codeProxyBase"), new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Code")));
							base.Constructor.Statements.Insert(0, new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Code"), new CodeObjectCreateExpression("CustomCodeProxy", new CodeThisReferenceExpression())));
						}
					}
				}
			}
		}

		private sealed class NonRootTypeDecl : TypeDecl
		{
			public CodeExpressionCollection Parameters;

			public CodeExpressionCollection Filters;

			public CodeExpressionCollection Actions;

			public CodeExpressionCollection Fields;

			public CodeExpressionCollection DataPoints;

			public CodeExpressionCollection DataGroupings;

			public CodeExpressionCollection DataCells;

			public ReturnStatementList IndexedExpressions;

			public NonRootTypeDecl(string typeName, string baseTypeName, TypeDecl parent, bool setCode)
				: base(typeName, baseTypeName, parent, setCode)
			{
				if (setCode)
				{
					base.ConstructorCreate();
				}
			}

			protected override CodeConstructor CreateConstructor()
			{
				CodeConstructor codeConstructor = base.CreateConstructor();
				if (base.m_setCode)
				{
					codeConstructor.Parameters.Add(new CodeParameterDeclarationExpression("CustomCodeProxy", "code"));
					codeConstructor.Statements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Code"), new CodeArgumentReferenceExpression("code")));
				}
				return codeConstructor;
			}

			protected override CodeTypeDeclaration CreateType(string name, string baseType)
			{
				CodeTypeDeclaration codeTypeDeclaration = base.CreateType(string.Format(CultureInfo.InvariantCulture, "{0}_{1}", name, baseType), baseType);
				if (base.m_setCode)
				{
					CodeMemberField codeMemberField = new CodeMemberField("CustomCodeProxy", "Code");
					codeMemberField.Attributes = (MemberAttributes)20482;
					codeTypeDeclaration.Members.Add(codeMemberField);
				}
				return codeTypeDeclaration;
			}
		}

		private sealed class CustomCodeProxyDecl : TypeDecl
		{
			public CustomCodeProxyDecl(TypeDecl parent)
				: base("CustomCodeProxy", "CustomCodeProxyBase", parent, false)
			{
				base.ConstructorCreate();
			}

			protected override CodeConstructor CreateConstructor()
			{
				CodeConstructor codeConstructor = base.CreateConstructor();
				codeConstructor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(IReportObjectModelProxyForCustomCode), "reportObjectModel"));
				codeConstructor.BaseConstructorArgs.Add(new CodeArgumentReferenceExpression("reportObjectModel"));
				return codeConstructor;
			}

			public void AddClassInstance(string className, string instanceName, int id)
			{
				string fileName = "CMCID" + id.ToString(CultureInfo.InvariantCulture) + "end";
				CodeMemberField codeMemberField = new CodeMemberField(className, "m_" + instanceName);
				codeMemberField.Attributes = (MemberAttributes)20482;
				codeMemberField.InitExpression = new CodeObjectCreateExpression(className);
				codeMemberField.LinePragma = new CodeLinePragma(fileName, 0);
				base.Type.Members.Add(codeMemberField);
				CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
				codeMemberProperty.Type = new CodeTypeReference(className);
				codeMemberProperty.Name = instanceName;
				codeMemberProperty.Attributes = (MemberAttributes)24578;
				codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), codeMemberField.Name)));
				codeMemberProperty.LinePragma = new CodeLinePragma(fileName, 2);
				base.Type.Members.Add(codeMemberProperty);
			}

			public void AddCode(string code)
			{
				CodeTypeMember codeTypeMember = new CodeSnippetTypeMember(code);
				codeTypeMember.LinePragma = new CodeLinePragma("CustomCode", 0);
				base.Type.Members.Add(codeTypeMember);
			}
		}

		private sealed class ReturnStatementList
		{
			private ArrayList m_list = new ArrayList();

			public CodeMethodReturnStatement this[int index]
			{
				get
				{
					return (CodeMethodReturnStatement)this.m_list[index];
				}
			}

			public int Count
			{
				get
				{
					return this.m_list.Count;
				}
			}

			public int Add(CodeMethodReturnStatement retStatement)
			{
				return this.m_list.Add(retStatement);
			}
		}

		public const string RootType = "ReportExprHostImpl";

		private const string EndSrcMarker = "end";

		private const string ExprSrcMarker = "Expr";

		private const string CustomCodeSrcMarker = "CustomCode";

		private const string CodeModuleClassInstanceDeclSrcMarker = "CMCID";

		private RootTypeDecl m_rootTypeDecl;

		private TypeDecl m_currentTypeDecl;

		private bool m_setCode;

		private static readonly Regex m_findExprNumber = new Regex("^Expr([0-9]+)end", RegexOptions.Compiled);

		private static readonly Regex m_findCodeModuleClassInstanceDeclNumber = new Regex("^CMCID([0-9]+)end", RegexOptions.Compiled);

		public bool HasExpressions
		{
			get
			{
				if (this.m_rootTypeDecl != null)
				{
					return this.m_rootTypeDecl.HasExpressions;
				}
				return false;
			}
		}

		public bool CustomCode
		{
			get
			{
				return this.m_setCode;
			}
		}

		public ExprHostBuilder()
		{
		}

		public void SetCustomCode()
		{
			this.m_setCode = true;
		}

		public CodeCompileUnit GetExprHost(IntermediateFormatVersion version, bool refusePermissions)
		{
			Global.Tracer.Assert(this.m_rootTypeDecl != null && this.m_currentTypeDecl.Parent == null, "(m_rootTypeDecl != null && m_currentTypeDecl.Parent == null)");
			CodeCompileUnit codeCompileUnit = null;
			if (this.HasExpressions)
			{
				codeCompileUnit = new CodeCompileUnit();
				codeCompileUnit.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration("System.Reflection.AssemblyVersion", new CodeAttributeArgument(new CodePrimitiveExpression(version.ToString()))));
				if (refusePermissions)
				{
					codeCompileUnit.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration("System.Security.Permissions.SecurityPermission", new CodeAttributeArgument(new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SecurityAction)), "RequestMinimum")), new CodeAttributeArgument("Execution", new CodePrimitiveExpression(true))));
					codeCompileUnit.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration("System.Security.Permissions.SecurityPermission", new CodeAttributeArgument(new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SecurityAction)), "RequestOptional")), new CodeAttributeArgument("Execution", new CodePrimitiveExpression(true))));
				}
				CodeNamespace codeNamespace = new CodeNamespace();
				codeCompileUnit.Namespaces.Add(codeNamespace);
				codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
				codeNamespace.Imports.Add(new CodeNamespaceImport("System.Convert"));
				codeNamespace.Imports.Add(new CodeNamespaceImport("System.Math"));
				codeNamespace.Imports.Add(new CodeNamespaceImport("Microsoft.VisualBasic"));
				codeNamespace.Imports.Add(new CodeNamespaceImport("AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel"));
				codeNamespace.Imports.Add(new CodeNamespaceImport("AspNetCore.ReportingServices.ReportProcessing.ExprHostObjectModel"));
				codeNamespace.Types.Add(this.m_rootTypeDecl.Type);
			}
			this.m_rootTypeDecl = null;
			return codeCompileUnit;
		}

		public ErrorSource ParseErrorSource(CompilerError error, out int id)
		{
			Global.Tracer.Assert(error.FileName != null, "(error.FileName != null)");
			id = -1;
			if (error.FileName.StartsWith("CustomCode", StringComparison.Ordinal))
			{
				return ErrorSource.CustomCode;
			}
			Match match = ExprHostBuilder.m_findCodeModuleClassInstanceDeclNumber.Match(error.FileName);
			if (match.Success && int.TryParse(match.Groups[1].Value, NumberStyles.Integer, (IFormatProvider)CultureInfo.InvariantCulture, out id))
			{
				return ErrorSource.CodeModuleClassInstanceDecl;
			}
			match = ExprHostBuilder.m_findExprNumber.Match(error.FileName);
			if (match.Success && int.TryParse(match.Groups[1].Value, NumberStyles.Integer, (IFormatProvider)CultureInfo.InvariantCulture, out id))
			{
				return ErrorSource.Expression;
			}
			return ErrorSource.Unknown;
		}

		public void ReportStart()
		{
			this.m_currentTypeDecl = (this.m_rootTypeDecl = new RootTypeDecl(this.m_setCode));
		}

		public void ReportEnd()
		{
			this.m_rootTypeDecl.CompleteConstructorCreation();
		}

		public void ReportLanguage(ExpressionInfo expression)
		{
			this.ExpressionAdd("ReportLanguageExpr", expression);
		}

		public void GenericLabel(ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelExpr", expression);
		}

		public void GenericValue(ExpressionInfo expression)
		{
			this.ExpressionAdd("ValueExpr", expression);
		}

		public void GenericNoRows(ExpressionInfo expression)
		{
			this.ExpressionAdd("NoRowsExpr", expression);
		}

		public void GenericVisibilityHidden(ExpressionInfo expression)
		{
			this.ExpressionAdd("VisibilityHiddenExpr", expression);
		}

		public void AggregateParamExprAdd(ExpressionInfo expression)
		{
			this.AggregateStart();
			this.GenericValue(expression);
			expression.ExprHostID = this.AggregateEnd();
		}

		public void CustomCodeProxyStart()
		{
			Global.Tracer.Assert(this.m_setCode, "(m_setCode)");
			this.m_currentTypeDecl = new CustomCodeProxyDecl(this.m_currentTypeDecl);
		}

		public void CustomCodeProxyEnd()
		{
			this.m_rootTypeDecl.Type.Members.Add(this.m_currentTypeDecl.Type);
			this.TypeEnd(this.m_rootTypeDecl);
		}

		public void CustomCodeClassInstance(string className, string instanceName, int id)
		{
			((CustomCodeProxyDecl)this.m_currentTypeDecl).AddClassInstance(className, instanceName, id);
		}

		public void ReportCode(string code)
		{
			((CustomCodeProxyDecl)this.m_currentTypeDecl).AddCode(code);
		}

		public void ReportParameterStart(string name)
		{
			this.TypeStart(name, "ReportParamExprHost");
		}

		public int ReportParameterEnd()
		{
			this.ExprIndexerCreate();
			return this.TypeEnd((TypeDecl)this.m_rootTypeDecl, "m_reportParameterHostsRemotable", ref this.m_rootTypeDecl.ReportParameters);
		}

		public void ReportParameterValidationExpression(ExpressionInfo expression)
		{
			this.ExpressionAdd("ValidationExpressionExpr", expression);
		}

		public void ReportParameterDefaultValue(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		public void ReportParameterValidValuesStart()
		{
			this.TypeStart("ReportParameterValidValues", "IndexedExprHost");
		}

		public void ReportParameterValidValuesEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ValidValuesHost");
		}

		public void ReportParameterValidValue(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		public void ReportParameterValidValueLabelsStart()
		{
			this.TypeStart("ReportParameterValidValueLabels", "IndexedExprHost");
		}

		public void ReportParameterValidValueLabelsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ValidValueLabelsHost");
		}

		public void ReportParameterValidValueLabel(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		public void CalcFieldStart(string name)
		{
			this.TypeStart(name, "CalcFieldExprHost");
		}

		public int CalcFieldEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_fieldHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).Fields);
		}

		public void QueryParametersStart()
		{
			this.TypeStart("QueryParameters", "IndexedExprHost");
		}

		public void QueryParametersEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "QueryParametersHost");
		}

		public void QueryParameterValue(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		public void DataSourceStart(string name)
		{
			this.TypeStart(name, "DataSourceExprHost");
		}

		public int DataSourceEnd()
		{
			return this.TypeEnd((TypeDecl)this.m_rootTypeDecl, "m_dataSourceHostsRemotable", ref this.m_rootTypeDecl.DataSources);
		}

		public void DataSourceConnectString(ExpressionInfo expression)
		{
			this.ExpressionAdd("ConnectStringExpr", expression);
		}

		public void DataSetStart(string name)
		{
			this.TypeStart(name, "DataSetExprHost");
		}

		public int DataSetEnd()
		{
			return this.TypeEnd((TypeDecl)this.m_rootTypeDecl, "m_dataSetHostsRemotable", ref this.m_rootTypeDecl.DataSets);
		}

		public void DataSetQueryCommandText(ExpressionInfo expression)
		{
			this.ExpressionAdd("QueryCommandTextExpr", expression);
		}

		public void PageSectionStart()
		{
			this.TypeStart(this.CreateTypeName("PageSection", this.m_rootTypeDecl.PageSections), "StyleExprHost");
		}

		public int PageSectionEnd()
		{
			return this.TypeEnd((TypeDecl)this.m_rootTypeDecl, "m_pageSectionHostsRemotable", ref this.m_rootTypeDecl.PageSections);
		}

		public void ParameterOmit(ExpressionInfo expression)
		{
			this.ExpressionAdd("OmitExpr", expression);
		}

		public void StyleAttribute(string name, ExpressionInfo expression)
		{
			this.ExpressionAdd(name + "Expr", expression);
		}

		public void ActionInfoStart()
		{
			this.TypeStart("ActionInfo", "ActionInfoExprHost");
		}

		public void ActionInfoEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ActionInfoHost");
		}

		public void ActionStart()
		{
			this.TypeStart(this.CreateTypeName("Action", ((NonRootTypeDecl)this.m_currentTypeDecl).Actions), "ActionExprHost");
		}

		public int ActionEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_actionItemHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).Actions);
		}

		public void ActionHyperlink(ExpressionInfo expression)
		{
			this.ExpressionAdd("HyperlinkExpr", expression);
		}

		public void ActionDrillThroughReportName(ExpressionInfo expression)
		{
			this.ExpressionAdd("DrillThroughReportNameExpr", expression);
		}

		public void ActionDrillThroughBookmarkLink(ExpressionInfo expression)
		{
			this.ExpressionAdd("DrillThroughBookmarkLinkExpr", expression);
		}

		public void ActionBookmarkLink(ExpressionInfo expression)
		{
			this.ExpressionAdd("BookmarkLinkExpr", expression);
		}

		public void ActionDrillThroughParameterStart()
		{
			this.ParameterStart();
		}

		public int ActionDrillThroughParameterEnd()
		{
			return this.ParameterEnd("m_drillThroughParameterHostsRemotable");
		}

		public void ReportItemBookmark(ExpressionInfo expression)
		{
			this.ExpressionAdd("BookmarkExpr", expression);
		}

		public void ReportItemToolTip(ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		public void LineStart(string name)
		{
			this.TypeStart(name, "ReportItemExprHost");
		}

		public int LineEnd()
		{
			return this.ReportItemEnd("m_lineHostsRemotable", ref this.m_rootTypeDecl.Lines);
		}

		public void RectangleStart(string name)
		{
			this.TypeStart(name, "ReportItemExprHost");
		}

		public int RectangleEnd()
		{
			return this.ReportItemEnd("m_rectangleHostsRemotable", ref this.m_rootTypeDecl.Rectangles);
		}

		public void TextBoxStart(string name)
		{
			this.TypeStart(name, "TextBoxExprHost");
		}

		public int TextBoxEnd()
		{
			return this.ReportItemEnd("m_textBoxHostsRemotable", ref this.m_rootTypeDecl.TextBoxes);
		}

		public void TextBoxToggleImageInitialState(ExpressionInfo expression)
		{
			this.ExpressionAdd("ToggleImageInitialStateExpr", expression);
		}

		public void UserSortExpressionsStart()
		{
			this.TypeStart("UserSort", "IndexedExprHost");
		}

		public void UserSortExpressionsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "UserSortExpressionsHost");
		}

		public void UserSortExpression(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		public void ImageStart(string name)
		{
			this.TypeStart(name, "ImageExprHost");
		}

		public int ImageEnd()
		{
			return this.ReportItemEnd("m_imageHostsRemotable", ref this.m_rootTypeDecl.Images);
		}

		public void ImageMIMEType(ExpressionInfo expression)
		{
			this.ExpressionAdd("MIMETypeExpr", expression);
		}

		public void SubreportStart(string name)
		{
			this.TypeStart(name, "SubreportExprHost");
		}

		public int SubreportEnd()
		{
			return this.ReportItemEnd("m_subreportHostsRemotable", ref this.m_rootTypeDecl.Subreports);
		}

		public void SubreportParameterStart()
		{
			this.ParameterStart();
		}

		public int SubreportParameterEnd()
		{
			return this.ParameterEnd("m_parameterHostsRemotable");
		}

		public void ActiveXControlStart(string name)
		{
			this.TypeStart(name, "ActiveXControlExprHost");
		}

		public int ActiveXControlEnd()
		{
			return this.ReportItemEnd("m_activeXControlHostsRemotable", ref this.m_rootTypeDecl.ActiveXControls);
		}

		public void ActiveXControlParameterStart()
		{
			this.ParameterStart();
		}

		public int ActiveXControlParameterEnd()
		{
			return this.ParameterEnd("m_parameterHostsRemotable");
		}

		public void SortingStart()
		{
			this.TypeStart("Sorting", "SortingExprHost");
		}

		public void SortingEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "SortingHost");
		}

		public void SortingExpression(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		public void SortDirectionsStart()
		{
			this.TypeStart("SortDirections", "IndexedExprHost");
		}

		public void SortDirectionsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "SortDirectionHosts");
		}

		public void SortDirection(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		public void FilterStart()
		{
			this.TypeStart(this.CreateTypeName("Filter", ((NonRootTypeDecl)this.m_currentTypeDecl).Filters), "FilterExprHost");
		}

		public int FilterEnd()
		{
			this.ExprIndexerCreate();
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_filterHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).Filters);
		}

		public void FilterExpression(ExpressionInfo expression)
		{
			this.ExpressionAdd("FilterExpressionExpr", expression);
		}

		public void FilterValue(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		public void GroupingStart(string typeName)
		{
			this.TypeStart(typeName, "GroupingExprHost");
		}

		public void GroupingEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "GroupingHost");
		}

		public void GroupingExpression(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		public void GroupingParentExpressionsStart()
		{
			this.TypeStart("Parent", "IndexedExprHost");
		}

		public void GroupingParentExpressionsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ParentExpressionsHost");
		}

		public void GroupingParentExpression(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		public void ListStart(string name)
		{
			this.TypeStart(name, "ListExprHost");
		}

		public int ListEnd()
		{
			return this.ReportItemEnd("m_listHostsRemotable", ref this.m_rootTypeDecl.Lists);
		}

		public void MatrixDynamicGroupStart(string name)
		{
			this.TypeStart("MatrixDynamicGroup_" + name, "MatrixDynamicGroupExprHost");
		}

		public bool MatrixDynamicGroupEnd(bool column)
		{
			switch (this.m_currentTypeDecl.Parent.BaseTypeName)
			{
			case "MatrixExprHost":
				if (column)
				{
					return this.TypeEnd(this.m_currentTypeDecl.Parent, "ColumnGroupingsHost");
				}
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "RowGroupingsHost");
			case "MatrixDynamicGroupExprHost":
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "SubGroupHost");
			default:
				Global.Tracer.Assert(false);
				return false;
			}
		}

		public void SubtotalStart()
		{
			this.TypeStart("Subtotal", "StyleExprHost");
		}

		public void SubtotalEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "SubtotalHost");
		}

		public void MatrixStart(string name)
		{
			this.TypeStart(name, "MatrixExprHost");
		}

		public int MatrixEnd()
		{
			return this.ReportItemEnd("m_matrixHostsRemotable", ref this.m_rootTypeDecl.Matrices);
		}

		public void MultiChartStart()
		{
			this.TypeStart("MultiChart", "MultiChartExprHost");
		}

		public void MultiChartEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MultiChartHost");
		}

		public void ChartDynamicGroupStart(string name)
		{
			this.TypeStart("ChartDynamicGroup_" + name, "ChartDynamicGroupExprHost");
		}

		public bool ChartDynamicGroupEnd(bool column)
		{
			switch (this.m_currentTypeDecl.Parent.BaseTypeName)
			{
			case "ChartExprHost":
				if (column)
				{
					return this.TypeEnd(this.m_currentTypeDecl.Parent, "ColumnGroupingsHost");
				}
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "RowGroupingsHost");
			case "ChartDynamicGroupExprHost":
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "SubGroupHost");
			default:
				Global.Tracer.Assert(false);
				return false;
			}
		}

		public void ChartHeadingLabel(ExpressionInfo expression)
		{
			this.ExpressionAdd("HeadingLabelExpr", expression);
		}

		public void ChartDataPointStart()
		{
			this.TypeStart(this.CreateTypeName("DataPoint", ((NonRootTypeDecl)this.m_currentTypeDecl).DataPoints), "ChartDataPointExprHost");
		}

		public int ChartDataPointEnd()
		{
			this.ExprIndexerCreate();
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_chartDataPointHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).DataPoints);
		}

		public void ChartDataPointDataValue(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		public void DataLabelValue(ExpressionInfo expression)
		{
			this.ExpressionAdd("DataLabelValueExpr", expression);
		}

		public void DataLabelStyleStart()
		{
			this.StyleStart("DataLabelStyle");
		}

		public void DataLabelStyleEnd()
		{
			this.StyleEnd("DataLabelStyleHost");
		}

		public void DataPointStyleStart()
		{
			this.StyleStart("Style");
		}

		public void DataPointStyleEnd()
		{
			this.StyleEnd("StyleHost");
		}

		public void DataPointMarkerStyleStart()
		{
			this.StyleStart("DataPointMarkerStyle");
		}

		public void DataPointMarkerStyleEnd()
		{
			this.StyleEnd("MarkerStyleHost");
		}

		public void ChartTitleStart()
		{
			this.TypeStart("Title", "ChartTitleExprHost");
		}

		public void ChartTitleEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "TitleHost");
		}

		public void ChartCaption(ExpressionInfo expression)
		{
			this.ExpressionAdd("CaptionExpr", expression);
		}

		public void MajorGridLinesStyleStart()
		{
			this.StyleStart("MajorGridLinesStyle");
		}

		public void MajorGridLinesStyleEnd()
		{
			this.StyleEnd("MajorGridLinesHost");
		}

		public void MinorGridLinesStyleStart()
		{
			this.StyleStart("MinorGridLinesStyle");
		}

		public void MinorGridLinesStyleEnd()
		{
			this.StyleEnd("MinorGridLinesHost");
		}

		public void AxisMin(ExpressionInfo expression)
		{
			this.ExpressionAdd("AxisMinExpr", expression);
		}

		public void AxisMax(ExpressionInfo expression)
		{
			this.ExpressionAdd("AxisMaxExpr", expression);
		}

		public void AxisCrossAt(ExpressionInfo expression)
		{
			this.ExpressionAdd("AxisCrossAtExpr", expression);
		}

		public void AxisMajorInterval(ExpressionInfo expression)
		{
			this.ExpressionAdd("AxisMajorIntervalExpr", expression);
		}

		public void AxisMinorInterval(ExpressionInfo expression)
		{
			this.ExpressionAdd("AxisMinorIntervalExpr", expression);
		}

		public void ChartStaticRowLabelsStart()
		{
			this.TypeStart("ChartStaticRowLabels", "IndexedExprHost");
		}

		public void ChartStaticRowLabelsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "StaticRowLabelsHost");
		}

		public void ChartStaticColumnLabelsStart()
		{
			this.TypeStart("ChartStaticColumnLabels", "IndexedExprHost");
		}

		public void ChartStaticColumnLabelsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "StaticColumnLabelsHost");
		}

		public void ChartStaticColumnRowLabel(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		public void ChartStart(string name)
		{
			this.TypeStart(name, "ChartExprHost");
		}

		public int ChartEnd()
		{
			return this.ReportItemEnd("m_chartHostsRemotable", ref this.m_rootTypeDecl.Charts);
		}

		public void ChartCategoryAxisStart()
		{
			this.AxisStart("CategoryAxis");
		}

		public void ChartCategoryAxisEnd()
		{
			this.AxisEnd("CategoryAxisHost");
		}

		public void ChartValueAxisStart()
		{
			this.AxisStart("ValueAxis");
		}

		public void ChartValueAxisEnd()
		{
			this.AxisEnd("ValueAxisHost");
		}

		public void ChartLegendStart()
		{
			this.StyleStart("Legend");
		}

		public void ChartLegendEnd()
		{
			this.StyleEnd("LegendHost");
		}

		public void ChartPlotAreaStart()
		{
			this.StyleStart("PlotArea");
		}

		public void ChartPlotAreaEnd()
		{
			this.StyleEnd("PlotAreaHost");
		}

		public void TableGroupStart(string name)
		{
			this.TypeStart("TableGroup_" + name, "TableGroupExprHost");
		}

		public bool TableGroupEnd()
		{
			switch (this.m_currentTypeDecl.Parent.BaseTypeName)
			{
			case "TableExprHost":
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "TableGroupsHost");
			case "TableGroupExprHost":
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "SubGroupHost");
			default:
				Global.Tracer.Assert(false);
				return false;
			}
		}

		public void TableRowVisibilityHiddenExpressionsStart()
		{
			this.TypeStart("TableRowVisibilityHiddenExpressionsClass", "IndexedExprHost");
		}

		public void TableRowVisibilityHiddenExpressionsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "TableRowVisibilityHiddenExpressions");
		}

		public void TableRowColVisibilityHiddenExpressionsExpr(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		public void TableStart(string name)
		{
			this.TypeStart(name, "TableExprHost");
		}

		public int TableEnd()
		{
			return this.ReportItemEnd("m_tableHostsRemotable", ref this.m_rootTypeDecl.Tables);
		}

		public void TableColumnVisibilityHiddenExpressionsStart()
		{
			this.TypeStart("TableColumnVisibilityHiddenExpressions", "IndexedExprHost");
		}

		public void TableColumnVisibilityHiddenExpressionsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "TableColumnVisibilityHiddenExpressions");
		}

		public void OWCChartStart(string name)
		{
			this.TypeStart(name, "OWCChartExprHost");
		}

		public int OWCChartEnd()
		{
			return this.ReportItemEnd("m_OWCChartHostsRemotable", ref this.m_rootTypeDecl.OWCCharts);
		}

		public void OWCChartColumnsStart()
		{
			this.TypeStart("OWCChartColumns", "IndexedExprHost");
		}

		public void OWCChartColumnsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "OWCChartColumnHosts");
		}

		public void OWCChartColumnsValue(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		public void DataValueStart()
		{
			this.TypeStart(this.CreateTypeName("DataValue", this.m_currentTypeDecl.DataValues), "DataValueExprHost");
		}

		public int DataValueEnd(bool isCustomProperty)
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, isCustomProperty ? "m_customPropertyHostsRemotable" : "m_dataValueHostsRemotable", ref this.m_currentTypeDecl.Parent.DataValues);
		}

		public void DataValueName(ExpressionInfo expression)
		{
			this.ExpressionAdd("DataValueNameExpr", expression);
		}

		public void DataValueValue(ExpressionInfo expression)
		{
			this.ExpressionAdd("DataValueValueExpr", expression);
		}

		public void CustomReportItemStart(string name)
		{
			this.TypeStart(name, "CustomReportItemExprHost");
		}

		public int CustomReportItemEnd()
		{
			return this.ReportItemEnd("m_customReportItemHostsRemotable", ref this.m_rootTypeDecl.CustomReportItems);
		}

		public void DataGroupingStart(bool column)
		{
			string template = "DataGrouping" + (column ? "Column" : "Row");
			this.TypeStart(this.CreateTypeName(template, ((NonRootTypeDecl)this.m_currentTypeDecl).DataGroupings), "DataGroupingExprHost");
		}

		public int DataGroupingEnd(bool column)
		{
			Global.Tracer.Assert("CustomReportItemExprHost" == this.m_currentTypeDecl.Parent.BaseTypeName || "DataGroupingExprHost" == this.m_currentTypeDecl.Parent.BaseTypeName);
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_dataGroupingHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).DataGroupings);
		}

		public void DataCellStart()
		{
			this.TypeStart(this.CreateTypeName("DataCell", ((NonRootTypeDecl)this.m_currentTypeDecl).DataCells), "DataCellExprHost");
		}

		public int DataCellEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_dataCellHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).DataCells);
		}

		private void TypeStart(string typeName, string baseType)
		{
			this.m_currentTypeDecl = new NonRootTypeDecl(typeName, baseType, this.m_currentTypeDecl, this.m_setCode);
		}

		private int TypeEnd(TypeDecl container, string name, ref CodeExpressionCollection initializers)
		{
			int result = -1;
			if (this.m_currentTypeDecl.HasExpressions)
			{
				result = container.NestedTypeColAdd(name, this.m_currentTypeDecl.BaseTypeName, ref initializers, this.m_currentTypeDecl.Type);
			}
			this.TypeEnd(container);
			return result;
		}

		private bool TypeEnd(TypeDecl container, string name)
		{
			bool hasExpressions = this.m_currentTypeDecl.HasExpressions;
			if (hasExpressions)
			{
				container.NestedTypeAdd(name, this.m_currentTypeDecl.Type);
			}
			this.TypeEnd(container);
			return hasExpressions;
		}

		private void TypeEnd(TypeDecl container)
		{
			Global.Tracer.Assert(this.m_currentTypeDecl.Parent != null && container != null, "(m_currentTypeDecl.Parent != null && container != null)");
			container.HasExpressions |= this.m_currentTypeDecl.HasExpressions;
			this.m_currentTypeDecl = this.m_currentTypeDecl.Parent;
		}

		private int ReportItemEnd(string name, ref CodeExpressionCollection initializers)
		{
			return this.TypeEnd((TypeDecl)this.m_rootTypeDecl, name, ref initializers);
		}

		private void ParameterStart()
		{
			this.TypeStart(this.CreateTypeName("Parameter", ((NonRootTypeDecl)this.m_currentTypeDecl).Parameters), "ParamExprHost");
		}

		private int ParameterEnd(string propName)
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, propName, ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).Parameters);
		}

		private void StyleStart(string typeName)
		{
			this.TypeStart(typeName, "StyleExprHost");
		}

		private void StyleEnd(string propName)
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, propName);
		}

		private void AxisStart(string typeName)
		{
			this.TypeStart(typeName, "AxisExprHost");
		}

		private void AxisEnd(string propName)
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, propName);
		}

		private void AggregateStart()
		{
			this.TypeStart(this.CreateTypeName("Aggregate", this.m_rootTypeDecl.Aggregates), "AggregateParamExprHost");
		}

		private int AggregateEnd()
		{
			return this.TypeEnd((TypeDecl)this.m_rootTypeDecl, "m_aggregateParamHostsRemotable", ref this.m_rootTypeDecl.Aggregates);
		}

		private string CreateTypeName(string template, CodeExpressionCollection initializers)
		{
			return template + ((initializers == null) ? "0" : initializers.Count.ToString(CultureInfo.InvariantCulture));
		}

		private void ExprIndexerCreate()
		{
			NonRootTypeDecl nonRootTypeDecl = (NonRootTypeDecl)this.m_currentTypeDecl;
			if (nonRootTypeDecl.IndexedExpressions != null)
			{
				Global.Tracer.Assert(nonRootTypeDecl.IndexedExpressions.Count > 0, "(currentTypeDecl.IndexedExpressions.Count > 0)");
				CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
				codeMemberProperty.Name = "Item";
				codeMemberProperty.Attributes = (MemberAttributes)24580;
				codeMemberProperty.Parameters.Add(new CodeParameterDeclarationExpression(typeof(int), "index"));
				codeMemberProperty.Type = new CodeTypeReference(typeof(object));
				nonRootTypeDecl.Type.Members.Add(codeMemberProperty);
				int count = nonRootTypeDecl.IndexedExpressions.Count;
				if (count == 1)
				{
					codeMemberProperty.GetStatements.Add(nonRootTypeDecl.IndexedExpressions[0]);
				}
				else
				{
					CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
					codeMemberProperty.GetStatements.Add(codeConditionStatement);
					int num = count - 1;
					int num2 = count - 2;
					for (int i = 0; i < num; i++)
					{
						codeConditionStatement.Condition = new CodeBinaryOperatorExpression(new CodeArgumentReferenceExpression("index"), CodeBinaryOperatorType.ValueEquality, new CodePrimitiveExpression(i));
						codeConditionStatement.TrueStatements.Add(nonRootTypeDecl.IndexedExpressions[i]);
						if (i < num2)
						{
							CodeConditionStatement codeConditionStatement2 = new CodeConditionStatement();
							codeConditionStatement.FalseStatements.Add(codeConditionStatement2);
							codeConditionStatement = codeConditionStatement2;
						}
					}
					codeConditionStatement.FalseStatements.Add(nonRootTypeDecl.IndexedExpressions[num]);
				}
			}
		}

		private void IndexedExpressionAdd(ExpressionInfo expression)
		{
			if (expression.Type == ExpressionInfo.Types.Expression)
			{
				NonRootTypeDecl nonRootTypeDecl = (NonRootTypeDecl)this.m_currentTypeDecl;
				if (nonRootTypeDecl.IndexedExpressions == null)
				{
					nonRootTypeDecl.IndexedExpressions = new ReturnStatementList();
				}
				nonRootTypeDecl.HasExpressions = true;
				expression.ExprHostID = nonRootTypeDecl.IndexedExpressions.Add(this.CreateExprReturnStatement(expression));
			}
		}

		private void ExpressionAdd(string name, ExpressionInfo expression)
		{
			if (expression.Type == ExpressionInfo.Types.Expression)
			{
				CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
				codeMemberProperty.Name = name;
				codeMemberProperty.Type = new CodeTypeReference(typeof(object));
				codeMemberProperty.Attributes = (MemberAttributes)24580;
				codeMemberProperty.GetStatements.Add(this.CreateExprReturnStatement(expression));
				this.m_currentTypeDecl.Type.Members.Add(codeMemberProperty);
				this.m_currentTypeDecl.HasExpressions = true;
			}
		}

		private CodeMethodReturnStatement CreateExprReturnStatement(ExpressionInfo expression)
		{
			CodeMethodReturnStatement codeMethodReturnStatement = new CodeMethodReturnStatement(new CodeSnippetExpression(expression.TransformedExpression));
			codeMethodReturnStatement.LinePragma = new CodeLinePragma("Expr" + expression.CompileTimeID.ToString(CultureInfo.InvariantCulture) + "end", 0);
			return codeMethodReturnStatement;
		}
	}
}

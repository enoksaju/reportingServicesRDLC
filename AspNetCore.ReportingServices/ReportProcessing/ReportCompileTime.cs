using AspNetCore.ReportingServices.Diagnostics;
using AspNetCore.ReportingServices.ReportProcessing.ExprHostObjectModel;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	public sealed class ReportCompileTime
	{
		private sealed class ExprCompileTimeInfo
		{
			public ExpressionInfo ExpressionInfo;

			public ObjectType OwnerObjectType;

			public string OwnerObjectName;

			public string OwnerPropertyName;

			public int NumErrors;

			public int NumWarnings;

			public ExprCompileTimeInfo(ExpressionInfo expression, ExpressionParser.ExpressionContext context)
			{
				this.ExpressionInfo = expression;
				this.OwnerObjectType = context.ObjectType;
				this.OwnerObjectName = context.ObjectName;
				this.OwnerPropertyName = context.PropertyName;
				this.NumErrors = 0;
				this.NumWarnings = 0;
			}
		}

		private sealed class ExprCompileTimeInfoList : ArrayList
		{
			public new ExprCompileTimeInfo this[int exprCTId]
			{
				get
				{
					return (ExprCompileTimeInfo)base[exprCTId];
				}
			}
		}

		private sealed class CodeModuleClassInstanceDeclCompileTimeInfo
		{
			public int NumErrors;

			public int NumWarnings;
		}

		private sealed class CodeModuleClassInstanceDeclCompileTimeInfoList : Hashtable
		{
			public new CodeModuleClassInstanceDeclCompileTimeInfo this[object id]
			{
				get
				{
					CodeModuleClassInstanceDeclCompileTimeInfo codeModuleClassInstanceDeclCompileTimeInfo = (CodeModuleClassInstanceDeclCompileTimeInfo)base[id];
					if (codeModuleClassInstanceDeclCompileTimeInfo == null)
					{
						codeModuleClassInstanceDeclCompileTimeInfo = new CodeModuleClassInstanceDeclCompileTimeInfo();
						base.Add(id, codeModuleClassInstanceDeclCompileTimeInfo);
					}
					return codeModuleClassInstanceDeclCompileTimeInfo;
				}
			}
		}

		private ExpressionParser m_langParser;

		private ErrorContext m_errorContext;

		private ExprHostBuilder m_builder;

		private ExprCompileTimeInfoList m_ctExprList;

		private CodeModuleClassInstanceDeclCompileTimeInfoList m_ctClassInstDeclList;

		private int m_customCodeNumErrors;

		private int m_customCodeNumWarnings;

		private ArrayList m_reportLevelFieldReferences;

		public ExprHostBuilder Builder
		{
			get
			{
				return this.m_builder;
			}
		}

		public bool BodyRefersToReportItems
		{
			get
			{
				return this.m_langParser.BodyRefersToReportItems;
			}
		}

		public bool PageSectionRefersToReportItems
		{
			get
			{
				return this.m_langParser.PageSectionRefersToReportItems;
			}
		}

		public int NumberOfAggregates
		{
			get
			{
				return this.m_langParser.NumberOfAggregates;
			}
		}

		public int LastAggregateID
		{
			get
			{
				return this.m_langParser.LastID;
			}
		}

		public bool ValueReferenced
		{
			get
			{
				return this.m_langParser.ValueReferenced;
			}
		}

		public bool ValueReferencedGlobal
		{
			get
			{
				return this.m_langParser.ValueReferencedGlobal;
			}
		}

		public ReportCompileTime(ExpressionParser langParser, ErrorContext errorContext)
		{
			this.m_langParser = langParser;
			this.m_errorContext = errorContext;
			this.m_builder = new ExprHostBuilder();
		}

		public ExpressionInfo ParseExpression(string expression, ExpressionParser.ExpressionContext context)
		{
			ExpressionInfo expressionInfo = this.m_langParser.ParseExpression(expression, context);
			this.ProcessExpression(expressionInfo, context);
			return expressionInfo;
		}

		public ExpressionInfo ParseExpression(string expression, ExpressionParser.ExpressionContext context, ExpressionParser.DetectionFlags flag, out bool reportParameterReferenced, out string reportParameterName, out bool userCollectionReferenced)
		{
			ExpressionInfo expressionInfo = this.m_langParser.ParseExpression(expression, context, flag, out reportParameterReferenced, out reportParameterName, out userCollectionReferenced);
			this.ProcessExpression(expressionInfo, context);
			return expressionInfo;
		}

		public ExpressionInfo ParseExpression(string expression, ExpressionParser.ExpressionContext context, out bool userCollectionReferenced)
		{
			ExpressionInfo expressionInfo = this.m_langParser.ParseExpression(expression, context, out userCollectionReferenced);
			this.ProcessExpression(expressionInfo, context);
			return expressionInfo;
		}

		public void ConvertFields2ComplexExpr()
		{
			if (this.m_reportLevelFieldReferences != null)
			{
				for (int num = this.m_reportLevelFieldReferences.Count - 1; num >= 0; num--)
				{
					ExprCompileTimeInfo exprCompileTimeInfo = (ExprCompileTimeInfo)this.m_reportLevelFieldReferences[num];
					this.m_langParser.ConvertField2ComplexExpr(ref exprCompileTimeInfo.ExpressionInfo);
					this.RegisterExpression(exprCompileTimeInfo);
				}
			}
		}

		public void ResetValueReferencedFlag()
		{
			this.m_langParser.ResetValueReferencedFlag();
		}

		public byte[] Compile(Report report, AppDomain compilationTempAppDomain, bool refusePermissions)
		{
            byte[] result = null;//this.InternalCompile(report, compilationTempAppDomain, refusePermissions);
            //todo: can delete?
            RevertImpersonationContext.Run(delegate
			{
				result = this.InternalCompile(report, compilationTempAppDomain, refusePermissions);
			});
			return result;
		}

		private void ProcessExpression(ExpressionInfo expression, ExpressionParser.ExpressionContext context)
		{
			if (expression.Type == ExpressionInfo.Types.Expression)
			{
				this.RegisterExpression(new ExprCompileTimeInfo(expression, context));
				this.ProcessAggregateParams(expression, context);
			}
			else if (expression.Type == ExpressionInfo.Types.Aggregate)
			{
				this.ProcessAggregateParams(expression, context);
			}
			else if (expression.Type == ExpressionInfo.Types.Field && context.Location == LocationFlags.None)
			{
				if (this.m_reportLevelFieldReferences == null)
				{
					this.m_reportLevelFieldReferences = new ArrayList();
				}
				this.m_reportLevelFieldReferences.Add(new ExprCompileTimeInfo(expression, context));
			}
		}

		private void RegisterExpression(ExprCompileTimeInfo exprCTInfo)
		{
			if (this.m_ctExprList == null)
			{
				this.m_ctExprList = new ExprCompileTimeInfoList();
			}
			exprCTInfo.ExpressionInfo.CompileTimeID = this.m_ctExprList.Add(exprCTInfo);
		}

		private void ProcessAggregateParams(ExpressionInfo expression, ExpressionParser.ExpressionContext context)
		{
			if (expression.Aggregates != null)
			{
				for (int num = expression.Aggregates.Count - 1; num >= 0; num--)
				{
					this.ProcessAggregateParam(expression.Aggregates[num], context);
				}
			}
			if (expression.RunningValues != null)
			{
				for (int num2 = expression.RunningValues.Count - 1; num2 >= 0; num2--)
				{
					this.ProcessAggregateParam(expression.RunningValues[num2], context);
				}
			}
		}

		private void ProcessAggregateParam(DataAggregateInfo aggregate, ExpressionParser.ExpressionContext context)
		{
			if (aggregate != null && aggregate.Expressions != null)
			{
				for (int i = 0; i < aggregate.Expressions.Length; i++)
				{
					this.ProcessAggregateParam(aggregate.Expressions[i], context);
				}
			}
		}

		private void ProcessAggregateParam(ExpressionInfo expression, ExpressionParser.ExpressionContext context)
		{
			if (expression != null && expression.Type == ExpressionInfo.Types.Expression)
			{
				this.RegisterExpression(new ExprCompileTimeInfo(expression, context));
			}
		}

		private byte[] InternalCompile(Report report, AppDomain compilationTempAppDomain, bool refusePermissions)
		{
			if (this.m_builder.HasExpressions)
			{
				CompilerParameters compilerParameters = new CompilerParameters();
				compilerParameters.OutputAssembly = string.Format(CultureInfo.InvariantCulture, "{0}{1}.dll", Path.GetTempPath(), report.ExprHostAssemblyName);
				compilerParameters.GenerateExecutable = false;
				compilerParameters.GenerateInMemory = false;
				compilerParameters.IncludeDebugInformation = false;
				compilerParameters.ReferencedAssemblies.Add("System.dll");
				compilerParameters.ReferencedAssemblies.Add(typeof(ReportObjectModelProxy).Assembly.Location);
				CompilerParameters compilerParameters2 = compilerParameters;
				compilerParameters2.CompilerOptions += this.m_langParser.GetCompilerArguments();
				if (report.CodeModules != null)
				{
					this.ResolveAssemblylocations(report.CodeModules, compilerParameters, this.m_errorContext, compilationTempAppDomain);
				}
				CompilerResults compilerResults = null;
				try
				{
					CodeCompileUnit exprHost = this.m_builder.GetExprHost(report.IntermediateFormatVersion, refusePermissions);
					report.CompiledCodeGeneratedWithRefusedPermissions = refusePermissions;
					CodeDomProvider codeCompiler = this.m_langParser.GetCodeCompiler();
					compilerResults = codeCompiler.CompileAssemblyFromDom(compilerParameters, exprHost);
					if (compilerResults.NativeCompilerReturnValue == 0 && compilerResults.Errors.Count <= 0)
					{
						using (FileStream fileStream = File.OpenRead(compilerResults.PathToAssembly))
						{
							byte[] array = new byte[fileStream.Length];
							int num = fileStream.Read(array, 0, (int)fileStream.Length);
							Global.Tracer.Assert(num == fileStream.Length, "(read == fs.Length)");
							return array;
						}
					}
					if (Global.Tracer.TraceVerbose)
					{
						try
						{
							using (MemoryStream memoryStream = new MemoryStream())
							{
								IndentedTextWriter indentedTextWriter = new IndentedTextWriter(new StreamWriter(memoryStream), "    ");
								codeCompiler.GenerateCodeFromCompileUnit(exprHost, indentedTextWriter, new CodeGeneratorOptions());
								indentedTextWriter.Flush();
								memoryStream.Position = 0L;
								StreamReader streamReader = new StreamReader(memoryStream);
								Global.Tracer.Trace(streamReader.ReadToEnd());
							}
						}
						catch
						{
						}
					}
					this.ParseErrors(compilerResults, report.CodeClasses);
					return new byte[0];
				}
				finally
				{
					if (compilerResults != null && compilerResults.PathToAssembly != null)
					{
						File.Delete(compilerResults.PathToAssembly);
					}
				}
			}
			return new byte[0];
		}

		private void ResolveAssemblylocations(StringList codeModules, CompilerParameters options, ErrorContext errorContext, AppDomain compilationTempAppDomain)
		{
			AssemblyLocationResolver assemblyLocationResolver = AssemblyLocationResolver.CreateResolver(compilationTempAppDomain);
			for (int num = codeModules.Count - 1; num >= 0; num--)
			{
				try
				{
					options.ReferencedAssemblies.Add(assemblyLocationResolver.LoadAssemblyAndResolveLocation(codeModules[num]));
				}
				catch (Exception ex)
				{
					ProcessingMessage processingMessage = errorContext.Register(ProcessingErrorCode.rsErrorLoadingCodeModule, Severity.Error, ObjectType.Report, null, null, codeModules[num], ex.Message);
					if (Global.Tracer.TraceError && processingMessage != null)
					{
						Global.Tracer.Trace(TraceLevel.Error, processingMessage.Message + Environment.NewLine + ex.ToString());
					}
				}
			}
		}

		private void ParseErrors(CompilerResults results, CodeClassList codeClassInstDecls)
		{
			int count = results.Errors.Count;
			if (results.NativeCompilerReturnValue != 0 && count == 0)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsUnexpectedCompilerError, Severity.Error, ObjectType.Report, null, null, results.NativeCompilerReturnValue.ToString(CultureInfo.CurrentCulture));
			}
			for (int i = 0; i < count; i++)
			{
				CompilerError error = results.Errors[i];
				int num = default(int);
				switch (this.m_builder.ParseErrorSource(error, out num))
				{
				case ExprHostBuilder.ErrorSource.Expression:
				{
					ExprCompileTimeInfo exprCompileTimeInfo = this.m_ctExprList[num];
					this.RegisterError(error, ref exprCompileTimeInfo.NumErrors, ref exprCompileTimeInfo.NumWarnings, exprCompileTimeInfo.OwnerObjectType, exprCompileTimeInfo.OwnerObjectName, exprCompileTimeInfo.OwnerPropertyName, ProcessingErrorCode.rsCompilerErrorInExpression);
					break;
				}
				case ExprHostBuilder.ErrorSource.CustomCode:
					this.RegisterError(error, ref this.m_customCodeNumErrors, ref this.m_customCodeNumWarnings, ObjectType.Report, (string)null, (string)null, ProcessingErrorCode.rsCompilerErrorInCode);
					break;
				case ExprHostBuilder.ErrorSource.CodeModuleClassInstanceDecl:
				{
					if (this.m_ctClassInstDeclList == null)
					{
						this.m_ctClassInstDeclList = new CodeModuleClassInstanceDeclCompileTimeInfoList();
					}
					CodeModuleClassInstanceDeclCompileTimeInfo codeModuleClassInstanceDeclCompileTimeInfo = this.m_ctClassInstDeclList[num];
					this.RegisterError(error, ref codeModuleClassInstanceDeclCompileTimeInfo.NumErrors, ref codeModuleClassInstanceDeclCompileTimeInfo.NumWarnings, ObjectType.CodeClass, codeClassInstDecls[num].ClassName, (string)null, ProcessingErrorCode.rsCompilerErrorInClassInstanceDeclaration);
					break;
				}
				default:
					this.m_errorContext.Register(ProcessingErrorCode.rsUnexpectedCompilerError, Severity.Error, ObjectType.Report, null, null, this.FormatError(error));
					throw new ReportProcessingException(this.m_errorContext.Messages);
				}
			}
		}

		private void RegisterError(CompilerError error, ref int numErrors, ref int numWarnings, ObjectType objectType, string objectName, string propertyName, ProcessingErrorCode errorCode)
		{
			if ((error.IsWarning ? numWarnings : numErrors) < 1)
			{
				bool flag = false;
				Severity severity;
				if (error.IsWarning)
				{
					flag = true;
					severity = Severity.Warning;
					numWarnings++;
				}
				else
				{
					flag = true;
					severity = Severity.Error;
					numErrors++;
				}
				if (flag)
				{
					this.m_errorContext.Register(errorCode, severity, objectType, objectName, propertyName, this.FormatError(error), error.Line.ToString(CultureInfo.CurrentCulture));
				}
			}
		}

		private string FormatError(CompilerError error)
		{
			return string.Format(CultureInfo.CurrentCulture, "[{0}] {1}", error.ErrorNumber, error.ErrorText);
		}
	}
}

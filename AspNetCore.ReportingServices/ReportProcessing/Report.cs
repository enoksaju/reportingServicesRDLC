using AspNetCore.ReportingServices.ReportProcessing.ExprHostObjectModel;
using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using AspNetCore.ReportingServices.ReportRendering;
using System;
using System.Globalization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class Report : ReportItem, IAggregateHolder
	{
		public enum ShowHideTypes
		{
			None,
			Static,
			Interactive
		}

		private IntermediateFormatVersion m_intermediateFormatVersion;

		private Guid m_reportVersion = Guid.Empty;

		private string m_author;

		private int m_autoRefresh;

		private EmbeddedImageHashtable m_embeddedImages;

		private PageSection m_pageHeader;

		private PageSection m_pageFooter;

		private ReportItemCollection m_reportItems;

		private DataSourceList m_dataSources;

		private string m_pageHeight = "11in";

		private double m_pageHeightValue;

		private string m_pageWidth = "8.5in";

		private double m_pageWidthValue;

		private string m_leftMargin = "0in";

		private double m_leftMarginValue;

		private string m_rightMargin = "0in";

		private double m_rightMarginValue;

		private string m_topMargin = "0in";

		private double m_topMarginValue;

		private string m_bottomMargin = "0in";

		private double m_bottomMarginValue;

		private int m_columns = 1;

		private string m_columnSpacing = "0.5in";

		private double m_columnSpacingValue;

		private DataAggregateInfoList m_pageAggregates;

		private byte[] m_exprCompiledCode;

		private bool m_exprCompiledCodeGeneratedWithRefusedPermissions;

		private bool m_mergeOnePass;

		private bool m_pageMergeOnePass;

		private bool m_subReportMergeTransactions;

		private bool m_needPostGroupProcessing;

		private bool m_hasPostSortAggregates;

		private bool m_hasReportItemReferences;

		private ShowHideTypes m_showHideType;

		private ImageStreamNames m_imageStreamNames;

		private int m_lastID;

		private int m_bodyID;

		private SubReportList m_subReports;

		private bool m_hasImageStreams;

		private bool m_hasLabels;

		private bool m_hasBookmarks;

		private bool m_parametersNotUsedInQuery;

		private ParameterDefList m_parameters;

		private string m_oneDataSetName;

		private StringList m_codeModules;

		private CodeClassList m_codeClasses;

		private bool m_hasSpecialRecursiveAggregates;

		private ExpressionInfo m_language;

		private string m_dataTransform;

		private string m_dataSchema;

		private bool m_dataElementStyleAttribute = true;

		private string m_code;

		private bool m_hasUserSortFilter;

		private string m_interactiveHeight;

		private double m_interactiveHeightValue = -1.0;

		private string m_interactiveWidth;

		private double m_interactiveWidthValue = -1.0;

		private InScopeSortFilterHashtable m_nonDetailSortFiltersInScope;

		private InScopeSortFilterHashtable m_detailSortFiltersInScope;

		[NonSerialized]
		private int m_lastAggregateID = -1;

		[NonSerialized]
		private ReportExprHost m_exprHost;

		[NonSerialized]
		private ReportSize m_pageHeightForRendering;

		[NonSerialized]
		private ReportSize m_pageWidthForRendering;

		[NonSerialized]
		private ReportSize m_leftMarginForRendering;

		[NonSerialized]
		private ReportSize m_rightMarginForRendering;

		[NonSerialized]
		private ReportSize m_topMarginForRendering;

		[NonSerialized]
		private ReportSize m_bottomMarginForRendering;

		[NonSerialized]
		private ReportSize m_columnSpacingForRendering;

		[NonSerialized]
		private long m_mainChunkSize = -1L;

		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Report;
			}
		}

		public override string DataElementNameDefault
		{
			get
			{
				return "Report";
			}
		}

		public IntermediateFormatVersion IntermediateFormatVersion
		{
			get
			{
				return this.m_intermediateFormatVersion;
			}
		}

		public Guid ReportVersion
		{
			get
			{
				return this.m_reportVersion;
			}
		}

		public string Author
		{
			get
			{
				return this.m_author;
			}
			set
			{
				this.m_author = value;
			}
		}

		public int AutoRefresh
		{
			get
			{
				return this.m_autoRefresh;
			}
			set
			{
				this.m_autoRefresh = value;
			}
		}

		public EmbeddedImageHashtable EmbeddedImages
		{
			get
			{
				return this.m_embeddedImages;
			}
			set
			{
				this.m_embeddedImages = value;
			}
		}

		public PageSection PageHeader
		{
			get
			{
				return this.m_pageHeader;
			}
			set
			{
				this.m_pageHeader = value;
			}
		}

		public bool PageHeaderEvaluation
		{
			get
			{
				if (this.m_pageHeader == null)
				{
					return false;
				}
				return this.m_pageHeader.PostProcessEvaluate;
			}
		}

		public PageSection PageFooter
		{
			get
			{
				return this.m_pageFooter;
			}
			set
			{
				this.m_pageFooter = value;
			}
		}

		public bool PageFooterEvaluation
		{
			get
			{
				if (this.m_pageFooter == null)
				{
					return false;
				}
				return this.m_pageFooter.PostProcessEvaluate;
			}
		}

		public double PageSectionWidth
		{
			get
			{
				double num = base.m_widthValue;
				if (this.m_columns > 1)
				{
					num += (double)(this.m_columns - 1) * (num + this.m_columnSpacingValue);
				}
				return num;
			}
		}

		public ReportItemCollection ReportItems
		{
			get
			{
				return this.m_reportItems;
			}
			set
			{
				this.m_reportItems = value;
			}
		}

		public DataSourceList DataSources
		{
			get
			{
				return this.m_dataSources;
			}
			set
			{
				this.m_dataSources = value;
			}
		}

		public int DataSourceCount
		{
			get
			{
				if (this.m_dataSources != null)
				{
					return this.m_dataSources.Count;
				}
				return 0;
			}
		}

		public string PageHeight
		{
			get
			{
				return this.m_pageHeight;
			}
			set
			{
				this.m_pageHeight = value;
			}
		}

		public double PageHeightValue
		{
			get
			{
				return this.m_pageHeightValue;
			}
			set
			{
				this.m_pageHeightValue = value;
			}
		}

		public string PageWidth
		{
			get
			{
				return this.m_pageWidth;
			}
			set
			{
				this.m_pageWidth = value;
			}
		}

		public double PageWidthValue
		{
			get
			{
				return this.m_pageWidthValue;
			}
			set
			{
				this.m_pageWidthValue = value;
			}
		}

		public string LeftMargin
		{
			get
			{
				return this.m_leftMargin;
			}
			set
			{
				this.m_leftMargin = value;
			}
		}

		public double LeftMarginValue
		{
			get
			{
				return this.m_leftMarginValue;
			}
			set
			{
				this.m_leftMarginValue = value;
			}
		}

		public string RightMargin
		{
			get
			{
				return this.m_rightMargin;
			}
			set
			{
				this.m_rightMargin = value;
			}
		}

		public double RightMarginValue
		{
			get
			{
				return this.m_rightMarginValue;
			}
			set
			{
				this.m_rightMarginValue = value;
			}
		}

		public string TopMargin
		{
			get
			{
				return this.m_topMargin;
			}
			set
			{
				this.m_topMargin = value;
			}
		}

		public double TopMarginValue
		{
			get
			{
				return this.m_topMarginValue;
			}
			set
			{
				this.m_topMarginValue = value;
			}
		}

		public string BottomMargin
		{
			get
			{
				return this.m_bottomMargin;
			}
			set
			{
				this.m_bottomMargin = value;
			}
		}

		public double BottomMarginValue
		{
			get
			{
				return this.m_bottomMarginValue;
			}
			set
			{
				this.m_bottomMarginValue = value;
			}
		}

		public int Columns
		{
			get
			{
				return this.m_columns;
			}
			set
			{
				this.m_columns = value;
			}
		}

		public string ColumnSpacing
		{
			get
			{
				return this.m_columnSpacing;
			}
			set
			{
				this.m_columnSpacing = value;
			}
		}

		public double ColumnSpacingValue
		{
			get
			{
				return this.m_columnSpacingValue;
			}
			set
			{
				this.m_columnSpacingValue = value;
			}
		}

		public DataAggregateInfoList PageAggregates
		{
			get
			{
				return this.m_pageAggregates;
			}
			set
			{
				this.m_pageAggregates = value;
			}
		}

		public byte[] CompiledCode
		{
			get
			{
				return this.m_exprCompiledCode;
			}
			set
			{
				this.m_exprCompiledCode = value;
			}
		}

		public bool CompiledCodeGeneratedWithRefusedPermissions
		{
			get
			{
				return this.m_exprCompiledCodeGeneratedWithRefusedPermissions;
			}
			set
			{
				this.m_exprCompiledCodeGeneratedWithRefusedPermissions = value;
			}
		}

		public bool MergeOnePass
		{
			get
			{
				return this.m_mergeOnePass;
			}
			set
			{
				this.m_mergeOnePass = value;
			}
		}

		public bool PageMergeOnePass
		{
			get
			{
				return this.m_pageMergeOnePass;
			}
			set
			{
				this.m_pageMergeOnePass = value;
			}
		}

		public bool SubReportMergeTransactions
		{
			get
			{
				return this.m_subReportMergeTransactions;
			}
			set
			{
				this.m_subReportMergeTransactions = value;
			}
		}

		public bool NeedPostGroupProcessing
		{
			get
			{
				return this.m_needPostGroupProcessing;
			}
			set
			{
				this.m_needPostGroupProcessing = value;
			}
		}

		public bool HasPostSortAggregates
		{
			get
			{
				return this.m_hasPostSortAggregates;
			}
			set
			{
				this.m_hasPostSortAggregates = value;
			}
		}

		public bool HasReportItemReferences
		{
			get
			{
				return this.m_hasReportItemReferences;
			}
			set
			{
				this.m_hasReportItemReferences = value;
			}
		}

		public ShowHideTypes ShowHideType
		{
			get
			{
				return this.m_showHideType;
			}
			set
			{
				this.m_showHideType = value;
			}
		}

		public ImageStreamNames ImageStreamNames
		{
			get
			{
				return this.m_imageStreamNames;
			}
			set
			{
				this.m_imageStreamNames = value;
			}
		}

		public bool ParametersNotUsedInQuery
		{
			get
			{
				return this.m_parametersNotUsedInQuery;
			}
			set
			{
				this.m_parametersNotUsedInQuery = value;
			}
		}

		public int LastID
		{
			get
			{
				return this.m_lastID;
			}
			set
			{
				this.m_lastID = value;
			}
		}

		public int BodyID
		{
			get
			{
				return this.m_bodyID;
			}
			set
			{
				this.m_bodyID = value;
			}
		}

		public SubReportList SubReports
		{
			get
			{
				return this.m_subReports;
			}
			set
			{
				this.m_subReports = value;
			}
		}

		public bool HasImageStreams
		{
			get
			{
				return this.m_hasImageStreams;
			}
			set
			{
				this.m_hasImageStreams = value;
			}
		}

		public bool HasLabels
		{
			get
			{
				return this.m_hasLabels;
			}
			set
			{
				this.m_hasLabels = value;
			}
		}

		public bool HasBookmarks
		{
			get
			{
				return this.m_hasBookmarks;
			}
			set
			{
				this.m_hasBookmarks = value;
			}
		}

		public ReportSize PageHeightForRendering
		{
			get
			{
				return this.m_pageHeightForRendering;
			}
			set
			{
				this.m_pageHeightForRendering = value;
			}
		}

		public ReportSize PageWidthForRendering
		{
			get
			{
				return this.m_pageWidthForRendering;
			}
			set
			{
				this.m_pageWidthForRendering = value;
			}
		}

		public ReportSize LeftMarginForRendering
		{
			get
			{
				return this.m_leftMarginForRendering;
			}
			set
			{
				this.m_leftMarginForRendering = value;
			}
		}

		public ReportSize RightMarginForRendering
		{
			get
			{
				return this.m_rightMarginForRendering;
			}
			set
			{
				this.m_rightMarginForRendering = value;
			}
		}

		public ReportSize TopMarginForRendering
		{
			get
			{
				return this.m_topMarginForRendering;
			}
			set
			{
				this.m_topMarginForRendering = value;
			}
		}

		public ReportSize BottomMarginForRendering
		{
			get
			{
				return this.m_bottomMarginForRendering;
			}
			set
			{
				this.m_bottomMarginForRendering = value;
			}
		}

		public ReportSize ColumnSpacingForRendering
		{
			get
			{
				return this.m_columnSpacingForRendering;
			}
			set
			{
				this.m_columnSpacingForRendering = value;
			}
		}

		public ParameterDefList Parameters
		{
			get
			{
				return this.m_parameters;
			}
			set
			{
				this.m_parameters = value;
			}
		}

		public string OneDataSetName
		{
			get
			{
				return this.m_oneDataSetName;
			}
			set
			{
				this.m_oneDataSetName = value;
			}
		}

		public StringList CodeModules
		{
			get
			{
				return this.m_codeModules;
			}
			set
			{
				this.m_codeModules = value;
			}
		}

		public CodeClassList CodeClasses
		{
			get
			{
				return this.m_codeClasses;
			}
			set
			{
				this.m_codeClasses = value;
			}
		}

		public bool HasSpecialRecursiveAggregates
		{
			get
			{
				return this.m_hasSpecialRecursiveAggregates;
			}
			set
			{
				this.m_hasSpecialRecursiveAggregates = value;
			}
		}

		public ExpressionInfo Language
		{
			get
			{
				return this.m_language;
			}
			set
			{
				this.m_language = value;
			}
		}

		public ReportExprHost ReportExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		public string DataTransform
		{
			get
			{
				return this.m_dataTransform;
			}
			set
			{
				this.m_dataTransform = value;
			}
		}

		public string DataSchema
		{
			get
			{
				return this.m_dataSchema;
			}
			set
			{
				this.m_dataSchema = value;
			}
		}

		public bool DataElementStyleAttribute
		{
			get
			{
				return this.m_dataElementStyleAttribute;
			}
			set
			{
				this.m_dataElementStyleAttribute = value;
			}
		}

		public string Code
		{
			get
			{
				return this.m_code;
			}
			set
			{
				this.m_code = value;
			}
		}

		public bool HasUserSortFilter
		{
			get
			{
				return this.m_hasUserSortFilter;
			}
			set
			{
				this.m_hasUserSortFilter = value;
			}
		}

		public string InteractiveHeight
		{
			get
			{
				return this.m_interactiveHeight;
			}
			set
			{
				this.m_interactiveHeight = value;
			}
		}

		public double InteractiveHeightValue
		{
			get
			{
				if (!(this.m_interactiveHeightValue < 0.0))
				{
					return this.m_interactiveHeightValue;
				}
				return this.m_pageHeightValue;
			}
			set
			{
				this.m_interactiveHeightValue = value;
			}
		}

		public string InteractiveWidth
		{
			get
			{
				return this.m_interactiveWidth;
			}
			set
			{
				this.m_interactiveWidth = value;
			}
		}

		public double InteractiveWidthValue
		{
			get
			{
				if (!(this.m_interactiveWidthValue < 0.0))
				{
					return this.m_interactiveWidthValue;
				}
				return this.m_pageWidthValue;
			}
			set
			{
				this.m_interactiveWidthValue = value;
			}
		}

		public InScopeSortFilterHashtable NonDetailSortFiltersInScope
		{
			get
			{
				return this.m_nonDetailSortFiltersInScope;
			}
			set
			{
				this.m_nonDetailSortFiltersInScope = value;
			}
		}

		public InScopeSortFilterHashtable DetailSortFiltersInScope
		{
			get
			{
				return this.m_detailSortFiltersInScope;
			}
			set
			{
				this.m_detailSortFiltersInScope = value;
			}
		}

		public string ExprHostAssemblyName
		{
			get
			{
				return "expression_host_" + this.m_reportVersion.ToString().Replace("-", "");
			}
		}

		public int LastAggregateID
		{
			get
			{
				return this.m_lastAggregateID;
			}
			set
			{
				this.m_lastAggregateID = value;
			}
		}

		public long MainChunkSize
		{
			get
			{
				return this.m_mainChunkSize;
			}
			set
			{
				this.m_mainChunkSize = value;
			}
		}

		public Report(int id, int idForReportItems)
			: base(id, null)
		{
			this.m_intermediateFormatVersion = new IntermediateFormatVersion();
			this.m_reportVersion = Guid.NewGuid();
			base.m_height = "11in";
			base.m_width = "8.5in";
			this.m_dataSources = new DataSourceList();
			this.m_reportItems = new ReportItemCollection(idForReportItems, true);
			this.m_pageAggregates = new DataAggregateInfoList();
			this.m_exprCompiledCode = new byte[0];
		}

		public Report(ReportItem parent, IntermediateFormatVersion version, Guid reportVersion)
			: base(parent)
		{
			this.m_intermediateFormatVersion = version;
			this.m_reportVersion = reportVersion;
			base.m_startPage = 0;
			base.m_endPage = 0;
		}

		public override bool Initialize(InitializationContext context)
		{
			context.Location = LocationFlags.None;
			context.ObjectType = this.ObjectType;
			context.ObjectName = null;
			base.Initialize(context);
			if (this.m_language != null)
			{
				this.m_language.Initialize("Language", context);
				context.ExprHostBuilder.ReportLanguage(this.m_language);
			}
			context.ReportDataElementStyleAttribute = this.m_dataElementStyleAttribute;
			this.m_pageHeightValue = context.ValidateSize(ref this.m_pageHeight, "PageHeight");
			if (this.m_pageHeightValue <= 0.0)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsInvalidSize, Severity.Error, context.ObjectType, context.ObjectName, "PageHeight", this.m_pageHeightValue.ToString(CultureInfo.InvariantCulture));
			}
			this.m_pageWidthValue = context.ValidateSize(ref this.m_pageWidth, "PageWidth");
			if (this.m_pageWidthValue <= 0.0)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsInvalidSize, Severity.Error, context.ObjectType, context.ObjectName, "PageWidth", this.m_pageWidthValue.ToString(CultureInfo.InvariantCulture));
			}
			if (this.m_interactiveHeight != null)
			{
				this.m_interactiveHeightValue = context.ValidateSize(ref this.m_interactiveHeight, false, "InteractiveHeight");
				if (0.0 == this.m_interactiveHeightValue)
				{
					this.m_interactiveHeightValue = 1.7976931348623157E+308;
				}
				else if (this.m_interactiveHeightValue < 0.0)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsInvalidSize, Severity.Error, context.ObjectType, context.ObjectName, "InteractiveHeight", this.m_interactiveHeightValue.ToString(CultureInfo.InvariantCulture));
				}
			}
			if (this.m_interactiveWidth != null)
			{
				this.m_interactiveWidthValue = context.ValidateSize(ref this.m_interactiveWidth, false, "InteractiveWidth");
				if (0.0 == this.m_interactiveWidthValue)
				{
					this.m_interactiveWidthValue = 1.7976931348623157E+308;
				}
				else if (this.m_interactiveWidthValue < 0.0)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsInvalidSize, Severity.Error, context.ObjectType, context.ObjectName, "InteractiveWidth", this.m_interactiveWidthValue.ToString(CultureInfo.InvariantCulture));
				}
			}
			this.m_leftMarginValue = context.ValidateSize(ref this.m_leftMargin, "LeftMargin");
			this.m_rightMarginValue = context.ValidateSize(ref this.m_rightMargin, "RightMargin");
			this.m_topMarginValue = context.ValidateSize(ref this.m_topMargin, "TopMargin");
			this.m_bottomMarginValue = context.ValidateSize(ref this.m_bottomMargin, "BottomMargin");
			this.m_columnSpacingValue = context.ValidateSize(ref this.m_columnSpacing, "ColumnSpacing");
			if (this.m_dataSources != null)
			{
				for (int i = 0; i < this.m_dataSources.Count; i++)
				{
					Global.Tracer.Assert(null != this.m_dataSources[i]);
					this.m_dataSources[i].Initialize(context);
				}
			}
			this.BodyInitialize(context);
			this.PageHeaderFooterInitialize(context);
			if (context.ExprHostBuilder.CustomCode)
			{
				context.ExprHostBuilder.CustomCodeProxyStart();
				if (this.m_codeClasses != null && this.m_codeClasses.Count > 0)
				{
					for (int num = this.m_codeClasses.Count - 1; num >= 0; num--)
					{
						CodeClass codeClass = this.m_codeClasses[num];
						context.ExprHostBuilder.CustomCodeClassInstance(codeClass.ClassName, codeClass.InstanceName, num);
					}
				}
				if (this.m_code != null && this.m_code.Length > 0)
				{
					context.ExprHostBuilder.ReportCode(this.m_code);
				}
				context.ExprHostBuilder.CustomCodeProxyEnd();
			}
			return false;
		}

		public void BodyInitialize(InitializationContext context)
		{
			context.RegisterReportItems(this.m_reportItems);
			this.m_reportItems.Initialize(context, true);
			context.ValidateUserSortInnerScope("0_ReportScope");
			context.TextboxesWithDetailSortExpressionInitialize();
			context.CalculateSortFilterGroupingLists();
			context.UnRegisterReportItems(this.m_reportItems);
		}

		public void PageHeaderFooterInitialize(InitializationContext context)
		{
			context.RegisterPageSectionScope(this.m_pageAggregates);
			if (this.m_pageHeader != null)
			{
				this.m_pageHeader.Initialize(context);
			}
			if (this.m_pageFooter != null)
			{
				this.m_pageFooter.Initialize(context);
			}
			context.UnRegisterPageSectionScope();
		}

		DataAggregateInfoList[] IAggregateHolder.GetAggregateLists()
		{
			return new DataAggregateInfoList[1]
			{
				this.m_pageAggregates
			};
		}

		DataAggregateInfoList[] IAggregateHolder.GetPostSortAggregateLists()
		{
			return null;
		}

		void IAggregateHolder.ClearIfEmpty()
		{
			Global.Tracer.Assert(null != this.m_pageAggregates);
			if (this.m_pageAggregates.Count == 0)
			{
				this.m_pageAggregates = null;
			}
		}

		public override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null, "(reportExprHost != null && reportObjectModel != null)");
			this.m_exprHost = reportExprHost;
			base.ReportItemSetExprHost(this.m_exprHost, reportObjectModel);
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.IntermediateFormatVersion, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.IntermediateFormatVersion));
			memberInfoList.Add(new MemberInfo(MemberName.ReportVersion, Token.Guid));
			memberInfoList.Add(new MemberInfo(MemberName.Author, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.AutoRefresh, Token.Int32));
			memberInfoList.Add(new MemberInfo(MemberName.EmbeddedImages, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.EmbeddedImageHashtable));
			memberInfoList.Add(new MemberInfo(MemberName.PageHeader, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.PageSection));
			memberInfoList.Add(new MemberInfo(MemberName.PageFooter, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.PageSection));
			memberInfoList.Add(new MemberInfo(MemberName.ReportItems, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemCollection));
			memberInfoList.Add(new MemberInfo(MemberName.DataSources, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.DataSourceList));
			memberInfoList.Add(new MemberInfo(MemberName.PageHeight, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.PageHeightValue, Token.Double));
			memberInfoList.Add(new MemberInfo(MemberName.PageWidth, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.PageWidthValue, Token.Double));
			memberInfoList.Add(new MemberInfo(MemberName.LeftMargin, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.LeftMarginValue, Token.Double));
			memberInfoList.Add(new MemberInfo(MemberName.RightMargin, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.RightMarginValue, Token.Double));
			memberInfoList.Add(new MemberInfo(MemberName.TopMargin, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.TopMarginValue, Token.Double));
			memberInfoList.Add(new MemberInfo(MemberName.BottomMargin, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.BottomMarginValue, Token.Double));
			memberInfoList.Add(new MemberInfo(MemberName.Columns, Token.Int32));
			memberInfoList.Add(new MemberInfo(MemberName.ColumnSpacing, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.ColumnSpacingValue, Token.Double));
			memberInfoList.Add(new MemberInfo(MemberName.PageAggregates, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.DataAggregateInfoList));
			memberInfoList.Add(new MemberInfo(MemberName.CompiledCode, Token.TypedArray));
			memberInfoList.Add(new MemberInfo(MemberName.MergeOnePass, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.PageMergeOnePass, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.SubReportMergeTransactions, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.NeedPostGroupProcessing, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.HasPostSortAggregates, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.HasReportItemReferences, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.ShowHideType, Token.Enum));
			memberInfoList.Add(new MemberInfo(MemberName.Images, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ImageStreamNames));
			memberInfoList.Add(new MemberInfo(MemberName.LastID, Token.Int32));
			memberInfoList.Add(new MemberInfo(MemberName.BodyID, Token.Int32));
			memberInfoList.Add(new MemberInfo(MemberName.SubReports, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.SubReportList));
			memberInfoList.Add(new MemberInfo(MemberName.HasImageStreams, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.HasLabels, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.HasBookmarks, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.SnapshotProcessingEnabled, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.Parameters, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ParameterDefList));
			memberInfoList.Add(new MemberInfo(MemberName.DataSetName, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.CodeModules, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.StringList));
			memberInfoList.Add(new MemberInfo(MemberName.CodeClasses, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.CodeClassList));
			memberInfoList.Add(new MemberInfo(MemberName.HasSpecialRecursiveAggregates, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.Language, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ExpressionInfo));
			memberInfoList.Add(new MemberInfo(MemberName.DataTransform, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.DataSchema, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.DataElementStyleAttribute, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.Code, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.HasUserSortFilter, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.CompiledCodeGeneratedWithRefusedPermissions, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.InteractiveHeight, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.InteractiveHeightValue, Token.Double));
			memberInfoList.Add(new MemberInfo(MemberName.InteractiveWidth, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.InteractiveWidthValue, Token.Double));
			memberInfoList.Add(new MemberInfo(MemberName.NonDetailSortFiltersInScope, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.InScopeSortFilterHashtable));
			memberInfoList.Add(new MemberInfo(MemberName.DetailSortFiltersInScope, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.InScopeSortFilterHashtable));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItem, memberInfoList);
		}
	}
}

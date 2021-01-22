using AspNetCore.ReportingServices.DataExtensions;
using AspNetCore.ReportingServices.Diagnostics;
using AspNetCore.ReportingServices.ReportIntermediateFormat;
using AspNetCore.ReportingServices.ReportProcessing;
using System;

namespace AspNetCore.ReportingServices.ReportPublishing
{
	[Serializable]
	public abstract class PublishingContextBase
	{
		private readonly bool m_isRdlx;

		private readonly PublishingContextKind m_publishingContextKind;

		private readonly ICatalogItemContext m_catalogContext;

		private readonly IChunkFactory m_createChunkFactory;

		private readonly AppDomain m_compilationTempAppDomain;

		private readonly bool m_generateExpressionHostWithRefusedPermissions;

		private ReportProcessingFlags m_processingFlags;

		private readonly AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.CheckSharedDataSource m_checkDataSourceCallback;

		private readonly AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.CheckSharedDataSet m_checkDataSetCallback;

		private readonly AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.ResolveTemporaryDataSource m_resolveTemporaryDataSourceCallback;

		private readonly AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.ResolveTemporaryDataSet m_resolveTemporaryDataSetCallback;

		private readonly DataSourceInfoCollection m_originalDataSources;

		private readonly DataSetInfoCollection m_originalDataSets;

		private readonly IConfiguration m_configuration;

		private readonly IDataProtection m_dataProtection;

		private readonly bool m_isInternalRepublish;

		private readonly bool m_traceAtomicScopes;

		private readonly bool m_isPackagedReportArchive;

		private readonly PublishingVersioning m_publishingVersioning;

		public PublishingVersioning PublishingVersioning
		{
			get
			{
				return this.m_publishingVersioning;
			}
		}

		public bool IsInternalRepublish
		{
			get
			{
				return this.m_isInternalRepublish;
			}
		}

		public PublishingContextKind PublishingContextKind
		{
			get
			{
				return this.m_publishingContextKind;
			}
		}

		public ICatalogItemContext CatalogContext
		{
			get
			{
				return this.m_catalogContext;
			}
		}

		public IChunkFactory CreateChunkFactory
		{
			get
			{
				return this.m_createChunkFactory;
			}
		}

		public AppDomain CompilationTempAppDomain
		{
			get
			{
				return this.m_compilationTempAppDomain;
			}
		}

		public bool GenerateExpressionHostWithRefusedPermissions
		{
			get
			{
				return this.m_generateExpressionHostWithRefusedPermissions;
			}
		}

		public ReportProcessingFlags ProcessingFlags
		{
			get
			{
				return this.m_processingFlags;
			}
			set
			{
				this.m_processingFlags = value;
			}
		}

		public AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.CheckSharedDataSource CheckDataSourceCallback
		{
			get
			{
				return this.m_checkDataSourceCallback;
			}
		}

		public AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.CheckSharedDataSet CheckDataSetCallback
		{
			get
			{
				return this.m_checkDataSetCallback;
			}
		}

		public AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.ResolveTemporaryDataSource ResolveTemporaryDataSourceCallback
		{
			get
			{
				return this.m_resolveTemporaryDataSourceCallback;
			}
		}

		public AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.ResolveTemporaryDataSet ResolveTemporaryDataSetCallback
		{
			get
			{
				return this.m_resolveTemporaryDataSetCallback;
			}
		}

		public DataSourceInfoCollection OriginalDataSources
		{
			get
			{
				return this.m_originalDataSources;
			}
		}

		public DataSetInfoCollection OriginalDataSets
		{
			get
			{
				return this.m_originalDataSets;
			}
		}

		public IConfiguration Configuration
		{
			get
			{
				return this.m_configuration;
			}
		}

		public IDataProtection DataProtection
		{
			get
			{
				return this.m_dataProtection;
			}
		}

		public bool TraceAtomicScopes
		{
			get
			{
				return this.m_traceAtomicScopes;
			}
		}

		public bool IsRdlx
		{
			get
			{
				return this.m_isRdlx;
			}
		}

		public bool IsPackagedReportArchive
		{
			get
			{
				return this.m_isPackagedReportArchive;
			}
		}

		public bool IsRdlSandboxingEnabled
		{
			get
			{
				if (this.Configuration != null)
				{
					return this.Configuration.RdlSandboxing != null;
				}
				return false;
			}
		}

		protected PublishingContextBase(PublishingContextKind publishingContextKind, ICatalogItemContext catalogContext, IChunkFactory createChunkFactory, AppDomain compilationTempAppDomain, bool generateExpressionHostWithRefusedPermissions, ReportProcessingFlags processingFlags, AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.CheckSharedDataSource checkDataSourceCallback, AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.ResolveTemporaryDataSource resolveTemporaryDataSourceCallback, DataSourceInfoCollection originalDataSources, AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.CheckSharedDataSet checkDataSetCallback, AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.ResolveTemporaryDataSet resolveTemporaryDataSetCallback, DataSetInfoCollection originalDataSets, IConfiguration configuration, IDataProtection dataProtection, bool isInternalRepublish, bool isPackagedReportArchive, bool isRdlx, bool traceAtomicScopes)
		{
			this.m_publishingContextKind = publishingContextKind;
			this.m_catalogContext = catalogContext;
			this.m_createChunkFactory = createChunkFactory;
			this.m_compilationTempAppDomain = compilationTempAppDomain;
			this.m_generateExpressionHostWithRefusedPermissions = generateExpressionHostWithRefusedPermissions;
			this.m_processingFlags = processingFlags;
			this.m_checkDataSourceCallback = checkDataSourceCallback;
			this.m_checkDataSetCallback = checkDataSetCallback;
			this.m_resolveTemporaryDataSourceCallback = resolveTemporaryDataSourceCallback;
			this.m_resolveTemporaryDataSetCallback = resolveTemporaryDataSetCallback;
			this.m_originalDataSources = originalDataSources;
			this.m_originalDataSets = originalDataSets;
			this.m_configuration = configuration;
			this.m_dataProtection = dataProtection;
			this.m_isInternalRepublish = isInternalRepublish;
			this.m_traceAtomicScopes = traceAtomicScopes;
			this.m_isPackagedReportArchive = isPackagedReportArchive;
			this.m_isRdlx = isRdlx;
			this.m_publishingVersioning = new PublishingVersioning(this.m_configuration, this);
		}

		public bool IsRestrictedDataRegionSort(bool isDataRowSort)
		{
			if (isDataRowSort)
			{
				return this.m_publishingVersioning.IsRdlFeatureRestricted(RdlFeatures.Sort_DataRegion);
			}
			return false;
		}

		public bool IsRestrictedGroupSort(bool isDataRowSort, AspNetCore.ReportingServices.ReportIntermediateFormat.Sorting sorting)
		{
			if (!sorting.NaturalSort && !sorting.DeferredSort)
			{
				return this.m_publishingVersioning.IsRdlFeatureRestricted(RdlFeatures.Sort_Group_Applied);
			}
			return false;
		}

		public bool IsRestrictedNaturalGroupSort(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo)
		{
			if (expressionInfo.Type != AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Field)
			{
				return this.m_publishingVersioning.IsRdlFeatureRestricted(RdlFeatures.SortGroupExpression_OnlySimpleField);
			}
			return false;
		}
	}
}

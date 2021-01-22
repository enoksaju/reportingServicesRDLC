using AspNetCore.ReportingServices.DataExtensions;
using AspNetCore.ReportingServices.Diagnostics;
using AspNetCore.ReportingServices.Interfaces;
using System;
using System.Globalization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	public abstract class ProcessingContext
	{
		private ICatalogItemContext m_reportContext;

		private string m_requestUserName;

		private ParameterInfoCollection m_parameters;

		private ParameterInfoCollection m_queryParameters;

		private ReportProcessing.OnDemandSubReportCallback m_subReportCallback;

		private IGetResource m_getResourceFunction;

		private ReportProcessing.ExecutionType m_interactiveExecution;

		private CultureInfo m_userLanguage;

		private UserProfileState m_allowUserProfileState;

		private UserProfileState m_initialUserProfileState;

		private ReportRuntimeSetup m_reportRuntimeSetup;

		private bool m_isHistorySnapshot;

		private IChunkFactory m_chunkFactory;

		private CreateAndRegisterStream m_createStreamCallback;

		private IJobContext m_jobContext;

		private IExtensionFactory m_extFactory;

		private IDataProtection m_dataProtection;

		private ReportProcessing.CreateReportChunk m_createReportChunkCallback;

		public abstract bool EnableDataBackedParameters
		{
			get;
		}

		public ICatalogItemContext ReportContext
		{
			get
			{
				return this.m_reportContext;
			}
		}

		public string RequestUserName
		{
			get
			{
				return this.m_requestUserName;
			}
		}

		public ParameterInfoCollection Parameters
		{
			get
			{
				return this.m_parameters;
			}
		}

		public ReportProcessing.OnDemandSubReportCallback OnDemandSubReportCallback
		{
			get
			{
				return this.m_subReportCallback;
			}
		}

		public ReportProcessing.CreateReportChunk CreateReportChunkCallback
		{
			get
			{
				return this.m_createReportChunkCallback;
			}
			set
			{
				this.m_createReportChunkCallback = value;
			}
		}

		public IChunkFactory ChunkFactory
		{
			get
			{
				return this.m_chunkFactory;
			}
			set
			{
				this.m_chunkFactory = value;
				ChunkFactoryAdapter @object = new ChunkFactoryAdapter(this.m_chunkFactory);
				this.m_createReportChunkCallback = @object.CreateReportChunk;
			}
		}

		public IGetResource GetResourceCallback
		{
			get
			{
				return this.m_getResourceFunction;
			}
		}

		public ReportProcessing.ExecutionType InteractiveExecution
		{
			get
			{
				return this.m_interactiveExecution;
			}
		}

		public CultureInfo UserLanguage
		{
			get
			{
				return this.m_userLanguage;
			}
		}

		public UserProfileState AllowUserProfileState
		{
			get
			{
				return this.m_allowUserProfileState;
			}
		}

		public UserProfileState InitialUserProfileState
		{
			get
			{
				return this.m_initialUserProfileState;
			}
		}

		public ReportRuntimeSetup ReportRuntimeSetup
		{
			get
			{
				return this.m_reportRuntimeSetup;
			}
		}

		public bool IsHistorySnapshot
		{
			get
			{
				return this.m_isHistorySnapshot;
			}
		}

		public ReportProcessingFlags ReportProcessingFlags
		{
			get
			{
				if (this.m_chunkFactory == null)
				{
					return ReportProcessingFlags.NotSet;
				}
				return this.m_chunkFactory.ReportProcessingFlags;
			}
		}

		public abstract IProcessingDataExtensionConnection CreateAndSetupDataExtensionFunction
		{
			get;
		}

		public abstract RuntimeDataSourceInfoCollection DataSources
		{
			get;
		}

		public virtual RuntimeDataSetInfoCollection SharedDataSetReferences
		{
			get
			{
				return null;
			}
		}

		public abstract bool CanShareDataSets
		{
			get;
		}

		public CreateAndRegisterStream CreateStreamCallback
		{
			get
			{
				return this.m_createStreamCallback;
			}
		}

		public ParameterInfoCollection QueryParameters
		{
			get
			{
				return this.m_queryParameters;
			}
		}

		public IJobContext JobContext
		{
			get
			{
				return this.m_jobContext;
			}
		}

		public IExtensionFactory ExtFactory
		{
			get
			{
				return this.m_extFactory;
			}
		}

		public IDataProtection DataProtection
		{
			get
			{
				return this.m_dataProtection;
			}
		}

		public ProcessingContext(ICatalogItemContext reportContext, string requestUserName, ParameterInfoCollection parameters, ReportProcessing.OnDemandSubReportCallback subReportCallback, IGetResource getResourceFunction, IChunkFactory createChunkFactory, ReportProcessing.ExecutionType interactiveExecution, CultureInfo culture, UserProfileState allowUserProfileState, UserProfileState initialUserProfileState, ReportRuntimeSetup reportRuntimeSetup, CreateAndRegisterStream createStreamCallback, bool isHistorySnapshot, IJobContext jobContext, IExtensionFactory extFactory, IDataProtection dataProtection)
		{
			Global.Tracer.Assert(null != reportContext, "(null != reportContext)");
			this.m_reportContext = reportContext;
			this.m_requestUserName = requestUserName;
			this.m_parameters = parameters;
			this.m_queryParameters = this.m_parameters.GetQueryParameters();
			this.m_subReportCallback = subReportCallback;
			this.m_getResourceFunction = getResourceFunction;
			this.m_chunkFactory = createChunkFactory;
			this.m_interactiveExecution = interactiveExecution;
			this.m_userLanguage = culture;
			this.m_allowUserProfileState = allowUserProfileState;
			this.m_initialUserProfileState = initialUserProfileState;
			this.m_reportRuntimeSetup = reportRuntimeSetup;
			this.m_createStreamCallback = createStreamCallback;
			this.m_isHistorySnapshot = isHistorySnapshot;
			ChunkFactoryAdapter @object = new ChunkFactoryAdapter(this.m_chunkFactory);
			this.m_createReportChunkCallback = @object.CreateReportChunk;
			this.m_jobContext = jobContext;
			this.m_extFactory = extFactory;
			this.m_dataProtection = dataProtection;
		}

		public abstract ReportProcessing.ProcessingContext CreateInternalProcessingContext(string chartName, Report report, ErrorContext errorContext, DateTime executionTime, UserProfileState allowUserProfileState, bool isHistorySnapshot, bool snapshotProcessing, bool processWithCachedData, ReportProcessing.GetReportChunk getChunkCallback, ReportProcessing.CreateReportChunk cacheDataCallback);

		public abstract ReportProcessing.ProcessingContext ParametersInternalProcessingContext(ErrorContext errorContext, DateTime executionTimeStamp, bool isSnapshot);
	}
}

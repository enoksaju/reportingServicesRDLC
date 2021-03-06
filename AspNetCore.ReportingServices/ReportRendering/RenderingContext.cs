using AspNetCore.ReportingServices.Diagnostics;
using AspNetCore.ReportingServices.OnDemandReportRendering;
using AspNetCore.ReportingServices.ReportProcessing;
using System;
using System.Collections;
using System.Collections.Specialized;

namespace AspNetCore.ReportingServices.ReportRendering
{
	public sealed class RenderingContext
	{
		private sealed class CommonInfo
		{
			private string m_rendererID;

			private DateTime m_executionTime;

			private string m_replacementRoot;

			private RenderingInfoManager m_renderingInfoManager;

			private ChunkManager.RenderingChunkManager m_chunkManager;

			private IGetResource m_getResourceCallback;

			private bool m_cacheState;

			private ICatalogItemContext m_reportContext;

			private AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.GetReportChunk m_getChunkCallback;

			private AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.GetChunkMimeType m_getChunkMimeType;

			private AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.StoreServerParameters m_storeServerParameters;

			private UserProfileState m_allowUserProfileState;

			private UserProfileState m_usedUserProfileState;

			private ReportRuntimeSetup m_reportRuntimeSetup;

			private IntermediateFormatVersion m_intermediateFormatVersion;

			public AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.GetReportChunk GetChunkCallback
			{
				get
				{
					return this.m_getChunkCallback;
				}
			}

			public string RendererID
			{
				get
				{
					return this.m_rendererID;
				}
			}

			public DateTime ExecutionTime
			{
				get
				{
					return this.m_executionTime;
				}
			}

			public string ReplacementRoot
			{
				get
				{
					return this.m_replacementRoot;
				}
			}

			public RenderingInfoManager RenderingInfoManager
			{
				get
				{
					return this.m_renderingInfoManager;
				}
			}

			public bool CacheState
			{
				get
				{
					return this.m_cacheState;
				}
				set
				{
					this.m_cacheState = value;
				}
			}

			public ChunkManager.RenderingChunkManager ChunkManager
			{
				get
				{
					return this.m_chunkManager;
				}
			}

			public IGetResource GetResourceCallback
			{
				get
				{
					return this.m_getResourceCallback;
				}
			}

			public AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.GetChunkMimeType GetChunkMimeType
			{
				get
				{
					return this.m_getChunkMimeType;
				}
			}

			public AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.StoreServerParameters StoreServerParameters
			{
				get
				{
					return this.m_storeServerParameters;
				}
			}

			public ICatalogItemContext TopLevelReportContext
			{
				get
				{
					return this.m_reportContext;
				}
			}

			public UserProfileState AllowUserProfileState
			{
				get
				{
					return this.m_allowUserProfileState;
				}
			}

			public UserProfileState UsedUserProfileState
			{
				get
				{
					return this.m_usedUserProfileState;
				}
				set
				{
					this.m_usedUserProfileState = value;
				}
			}

			public ReportRuntimeSetup ReportRuntimeSetup
			{
				get
				{
					return this.m_reportRuntimeSetup;
				}
			}

			public IntermediateFormatVersion IntermediateFormatVersion
			{
				get
				{
					return this.m_intermediateFormatVersion;
				}
			}

			public CommonInfo(string rendererID, DateTime executionTime, ICatalogItemContext reportContext, NameValueCollection reportParameters, AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.GetReportChunk getChunkCallback, ChunkManager.RenderingChunkManager chunkManager, IGetResource getResourceCallback, AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.GetChunkMimeType getChunkMimeType, AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.StoreServerParameters storeServerParameters, bool retrieveRenderingInfo, UserProfileState allowUserProfileState, ReportRuntimeSetup reportRuntimeSetup, IntermediateFormatVersion intermediateFormatVersion)
			{
				this.m_rendererID = rendererID;
				this.m_executionTime = executionTime;
				this.m_reportContext = reportContext;
				if (reportParameters != null)
				{
					this.m_replacementRoot = reportParameters["ReplacementRoot"];
				}
				this.m_renderingInfoManager = new RenderingInfoManager(rendererID, getChunkCallback, retrieveRenderingInfo);
				this.m_chunkManager = chunkManager;
				this.m_getResourceCallback = getResourceCallback;
				this.m_getChunkCallback = getChunkCallback;
				this.m_getChunkMimeType = getChunkMimeType;
				this.m_storeServerParameters = storeServerParameters;
				this.m_allowUserProfileState = allowUserProfileState;
				this.m_reportRuntimeSetup = reportRuntimeSetup;
				this.m_intermediateFormatVersion = intermediateFormatVersion;
			}
		}

		private CommonInfo m_commonInfo;

		private bool m_inPageSection;

		private string m_prefix;

		private EventInformation m_eventInfo;

		private ReportSnapshot m_reportSnapshot;

		private Hashtable m_processedItems;

		private Hashtable m_cachedHiddenInfo;

		private Uri m_contextUri;

		private EmbeddedImageHashtable m_embeddedImages;

		private ImageStreamNames m_imageStreamNames;

		private MatrixHeadingInstance m_headingInstance;

		private ICatalogItemContext m_currentReportICatalogItemContext;

		private bool m_nativeAllCRITypes;

		private Hashtable m_nativeCRITypes;

		private IJobContext m_jobContext;

		private IDataProtection m_dataProtection;

		public ICatalogItemContext TopLevelReportContext
		{
			get
			{
				return this.m_commonInfo.TopLevelReportContext;
			}
		}

		public AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.GetReportChunk GetChunkCallback
		{
			get
			{
				return this.m_commonInfo.GetChunkCallback;
			}
		}

		public AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.GetChunkMimeType GetChunkMimeType
		{
			get
			{
				return this.m_commonInfo.GetChunkMimeType;
			}
		}

		public AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.StoreServerParameters StoreServerParameters
		{
			get
			{
				return this.m_commonInfo.StoreServerParameters;
			}
		}

		public string RendererID
		{
			get
			{
				return this.m_commonInfo.RendererID;
			}
		}

		public DateTime ExecutionTime
		{
			get
			{
				return this.m_commonInfo.ExecutionTime;
			}
		}

		public string ReplacementRoot
		{
			get
			{
				return this.m_commonInfo.ReplacementRoot;
			}
		}

		public bool CacheState
		{
			get
			{
				return this.m_commonInfo.CacheState;
			}
			set
			{
				this.m_commonInfo.CacheState = value;
			}
		}

		public RenderingInfoManager RenderingInfoManager
		{
			get
			{
				return this.m_commonInfo.RenderingInfoManager;
			}
		}

		public ChunkManager.RenderingChunkManager ChunkManager
		{
			get
			{
				return this.m_commonInfo.ChunkManager;
			}
		}

		public IGetResource GetResourceCallback
		{
			get
			{
				return this.m_commonInfo.GetResourceCallback;
			}
		}

		public ReportRuntimeSetup ReportRuntimeSetup
		{
			get
			{
				return this.m_commonInfo.ReportRuntimeSetup;
			}
		}

		public IntermediateFormatVersion IntermediateFormatVersion
		{
			get
			{
				return this.m_commonInfo.IntermediateFormatVersion;
			}
		}

		public ImageStreamNames ImageStreamNames
		{
			get
			{
				return this.m_imageStreamNames;
			}
		}

		public EmbeddedImageHashtable EmbeddedImages
		{
			get
			{
				return this.m_embeddedImages;
			}
		}

		public bool InPageSection
		{
			get
			{
				return this.m_inPageSection;
			}
		}

		public string UniqueNamePrefix
		{
			get
			{
				Global.Tracer.Assert(this.m_inPageSection);
				return this.m_prefix;
			}
		}

		public Uri ContextUri
		{
			get
			{
				return this.m_contextUri;
			}
		}

		public ReportSnapshot ReportSnapshot
		{
			get
			{
				Global.Tracer.Assert(null != this.m_reportSnapshot);
				return this.m_reportSnapshot;
			}
		}

		private SenderInformationHashtable ShowHideSenderInfo
		{
			get
			{
				if (this.m_reportSnapshot != null)
				{
					return this.m_reportSnapshot.GetShowHideSenderInfo(this.ChunkManager);
				}
				return null;
			}
		}

		private ReceiverInformationHashtable ShowHideReceiverInfo
		{
			get
			{
				if (this.m_reportSnapshot != null)
				{
					return this.m_reportSnapshot.GetShowHideReceiverInfo(this.ChunkManager);
				}
				return null;
			}
		}

		public MatrixHeadingInstance HeadingInstance
		{
			get
			{
				return this.m_headingInstance;
			}
			set
			{
				this.m_headingInstance = value;
			}
		}

		public UserProfileState AllowUserProfileState
		{
			get
			{
				return this.m_commonInfo.AllowUserProfileState;
			}
		}

		public UserProfileState UsedUserProfileState
		{
			get
			{
				return this.m_commonInfo.UsedUserProfileState;
			}
			set
			{
				this.m_commonInfo.UsedUserProfileState = value;
			}
		}

		public ICatalogItemContext CurrentReportContext
		{
			get
			{
				return this.m_currentReportICatalogItemContext;
			}
		}

		public bool NativeAllCRITypes
		{
			get
			{
				return this.m_nativeAllCRITypes;
			}
			set
			{
				this.m_nativeAllCRITypes = value;
			}
		}

		public Hashtable NativeCRITypes
		{
			get
			{
				return this.m_nativeCRITypes;
			}
			set
			{
				this.m_nativeCRITypes = value;
			}
		}

		public IJobContext JobContext
		{
			get
			{
				return this.m_jobContext;
			}
		}

		public IDataProtection DataProtection
		{
			get
			{
				return this.m_dataProtection;
			}
		}

		public bool ShowHideStateChanged
		{
			get
			{
				if (this.m_eventInfo != null && this.m_eventInfo.ToggleStateInfo != null && this.m_eventInfo.HiddenInfo != null)
				{
					return true;
				}
				return false;
			}
		}

		public RenderingContext(ReportSnapshot reportSnapshot, string rendererID, DateTime executionTime, EmbeddedImageHashtable embeddedImages, ImageStreamNames imageStreamNames, EventInformation eventInfo, ICatalogItemContext reportContext, Uri contextUri, NameValueCollection reportParameters, AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.GetReportChunk getChunkCallback, ChunkManager.RenderingChunkManager chunkManager, IGetResource getResourceCallback, AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.GetChunkMimeType getChunkMimeType, AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.StoreServerParameters storeServerParameters, bool retrieveRenderingInfo, UserProfileState allowUserProfileState, ReportRuntimeSetup reportRuntimeSetup, IJobContext jobContext, IDataProtection dataProtection)
		{
			this.m_commonInfo = new CommonInfo(rendererID, executionTime, reportContext, reportParameters, getChunkCallback, chunkManager, getResourceCallback, getChunkMimeType, storeServerParameters, retrieveRenderingInfo, allowUserProfileState, reportRuntimeSetup, reportSnapshot.Report.IntermediateFormatVersion);
			this.m_inPageSection = false;
			this.m_prefix = null;
			this.m_eventInfo = eventInfo;
			this.m_reportSnapshot = reportSnapshot;
			this.m_processedItems = null;
			this.m_cachedHiddenInfo = null;
			this.m_contextUri = contextUri;
			this.m_embeddedImages = embeddedImages;
			this.m_imageStreamNames = imageStreamNames;
			this.m_currentReportICatalogItemContext = this.m_commonInfo.TopLevelReportContext;
			this.m_jobContext = jobContext;
			this.m_dataProtection = dataProtection;
		}

		public RenderingContext(RenderingContext copy, Uri contextUri, EmbeddedImageHashtable embeddedImages, ImageStreamNames imageStreamNames, ICatalogItemContext subreportICatalogItemContext)
		{
			this.m_commonInfo = copy.m_commonInfo;
			this.m_inPageSection = false;
			this.m_prefix = null;
			this.m_eventInfo = copy.m_eventInfo;
			this.m_reportSnapshot = copy.ReportSnapshot;
			this.m_processedItems = null;
			this.m_cachedHiddenInfo = copy.m_cachedHiddenInfo;
			this.m_contextUri = contextUri;
			this.m_embeddedImages = embeddedImages;
			this.m_imageStreamNames = imageStreamNames;
			this.m_currentReportICatalogItemContext = subreportICatalogItemContext;
			this.m_jobContext = copy.m_jobContext;
			this.m_dataProtection = copy.m_dataProtection;
		}

		public RenderingContext(RenderingContext copy, string prefix)
		{
			this.m_commonInfo = copy.m_commonInfo;
			this.m_inPageSection = true;
			this.m_prefix = prefix;
			this.m_eventInfo = null;
			this.m_reportSnapshot = null;
			this.m_processedItems = null;
			this.m_cachedHiddenInfo = null;
			this.m_contextUri = copy.m_contextUri;
			this.m_embeddedImages = copy.EmbeddedImages;
			this.m_imageStreamNames = copy.ImageStreamNames;
			this.m_currentReportICatalogItemContext = this.m_commonInfo.TopLevelReportContext;
			this.m_jobContext = copy.m_jobContext;
			this.m_dataProtection = copy.m_dataProtection;
		}

		public ReportItem FindReportItemInBody(int uniqueName)
		{
			object obj = null;
			NonComputedUniqueNames nonComputedUniqueNames = null;
			QuickFindHashtable quickFind = this.ReportSnapshot.GetQuickFind(this.ChunkManager);
			if (quickFind != null)
			{
				obj = quickFind[uniqueName];
			}
			if (obj == null)
			{
				Global.Tracer.Assert(null != this.ReportSnapshot.ReportInstance);
				obj = ((ISearchByUniqueName)this.ReportSnapshot.ReportInstance).Find(uniqueName, ref nonComputedUniqueNames, this.ChunkManager);
				if (obj == null)
				{
					return null;
				}
			}
			if (obj is AspNetCore.ReportingServices.ReportProcessing.ReportItem)
			{
				AspNetCore.ReportingServices.ReportProcessing.ReportItem reportItemDef = (AspNetCore.ReportingServices.ReportProcessing.ReportItem)obj;
				return ReportItem.CreateItem(-1, reportItemDef, null, this, nonComputedUniqueNames);
			}
			AspNetCore.ReportingServices.ReportProcessing.ReportItemInstance reportItemInstance = (AspNetCore.ReportingServices.ReportProcessing.ReportItemInstance)obj;
			return ReportItem.CreateItem(-1, reportItemInstance.ReportItemDef, reportItemInstance, this, nonComputedUniqueNames);
		}

		public bool IsItemHidden(int uniqueName, bool potentialSender)
		{
			try
			{
				if (this.ShowHideReceiverInfo != null && this.ShowHideSenderInfo != null)
				{
					if (this.m_processedItems == null)
					{
						this.m_processedItems = new Hashtable();
					}
					return this.RecursiveIsItemHidden(uniqueName, potentialSender);
				}
				return false;
			}
			finally
			{
				if (this.m_processedItems != null)
				{
					this.m_processedItems.Clear();
				}
			}
		}

		public bool IsToggleStateNegated(int uniqueName)
		{
			if (this.m_eventInfo != null && this.m_eventInfo.ToggleStateInfo != null && this.m_eventInfo.HiddenInfo != null)
			{
				return this.m_eventInfo.ToggleStateInfo.ContainsKey(uniqueName);
			}
			return false;
		}

		public bool IsToggleParent(int uniqueName)
		{
			if (this.ShowHideSenderInfo == null)
			{
				return false;
			}
			return this.ShowHideSenderInfo.ContainsKey(uniqueName);
		}

		public bool IsToggleChild(int uniqueName)
		{
			if (this.ShowHideReceiverInfo == null)
			{
				return false;
			}
			return this.ShowHideReceiverInfo.ContainsKey(uniqueName);
		}

		public TextBox GetToggleParent(int uniqueName)
		{
			if (this.ShowHideReceiverInfo != null)
			{
				ReceiverInformation receiverInformation = this.ShowHideReceiverInfo[uniqueName];
				if (receiverInformation != null)
				{
					ReportItem reportItem = this.FindReportItemInBody(receiverInformation.SenderUniqueName);
					Global.Tracer.Assert(null != reportItem);
					Global.Tracer.Assert(reportItem is TextBox);
					return (TextBox)reportItem;
				}
			}
			return null;
		}

		public static bool GetDefinitionHidden(AspNetCore.ReportingServices.ReportProcessing.Visibility visibility)
		{
			if (visibility == null)
			{
				return false;
			}
			if (visibility.Hidden == null)
			{
				return false;
			}
			if (ExpressionInfo.Types.Constant == visibility.Hidden.Type)
			{
				return visibility.Hidden.BoolValue;
			}
			return false;
		}

		public SortOptions GetSortState(int uniqueName)
		{
			if (this.m_eventInfo != null && this.m_eventInfo.SortInfo != null)
			{
				return this.m_eventInfo.SortInfo.GetSortState(uniqueName);
			}
			return SortOptions.None;
		}

		private bool RecursiveIsItemHidden(int uniqueName, bool potentialSender)
		{
			Global.Tracer.Assert(null != this.m_processedItems);
			if (this.m_processedItems.ContainsKey(uniqueName))
			{
				return false;
			}
			this.m_processedItems.Add(uniqueName, null);
			if (this.m_cachedHiddenInfo == null)
			{
				this.m_cachedHiddenInfo = new Hashtable();
			}
			else
			{
				object obj = this.m_cachedHiddenInfo[uniqueName];
				if (obj != null)
				{
					return (bool)obj;
				}
			}
			ReceiverInformation receiverInformation = this.ShowHideReceiverInfo[uniqueName];
			if (receiverInformation != null)
			{
				if (this.IsHidden(uniqueName, receiverInformation.StartHidden))
				{
					this.m_cachedHiddenInfo[uniqueName] = true;
					return true;
				}
				if (this.RecursiveIsItemHidden(receiverInformation.SenderUniqueName, true))
				{
					this.m_cachedHiddenInfo[uniqueName] = true;
					return true;
				}
			}
			if (potentialSender)
			{
				SenderInformation senderInformation = this.ShowHideSenderInfo[uniqueName];
				if (senderInformation != null)
				{
					if (this.IsHidden(uniqueName, senderInformation.StartHidden))
					{
						this.m_cachedHiddenInfo[uniqueName] = true;
						return true;
					}
					if (senderInformation.ContainerUniqueNames != null)
					{
						for (int num = senderInformation.ContainerUniqueNames.Length - 1; num >= 0; num--)
						{
							if (this.RecursiveIsItemHidden(senderInformation.ContainerUniqueNames[num], false))
							{
								this.m_cachedHiddenInfo[uniqueName] = true;
								return true;
							}
						}
					}
				}
			}
			this.m_cachedHiddenInfo[uniqueName] = false;
			return false;
		}

		private bool IsHidden(int uniqueName, bool startHidden)
		{
			if (this.IsHiddenNegated(uniqueName))
			{
				return !startHidden;
			}
			return startHidden;
		}

		private bool IsHiddenNegated(int uniqueName)
		{
			if (this.m_eventInfo != null && this.m_eventInfo.ToggleStateInfo != null && this.m_eventInfo.HiddenInfo != null)
			{
				return this.m_eventInfo.HiddenInfo.ContainsKey(uniqueName);
			}
			return false;
		}

		public static void FindRange(RenderingPagesRangesList pagesRangesList, int startIndex, int endIndex, int page, ref int startChild, ref int endChild)
		{
			RenderingContext.FindRange(pagesRangesList, startIndex, endIndex, page, true, true, ref startChild, ref endChild);
		}

		public static void FindRange(RenderingPagesRangesList pagesRangesList, int startIndex, int endIndex, int page, bool checkStart, bool checkEnd, ref int startChild, ref int endChild)
		{
			int num = 0;
			bool flag = false;
			int num2 = 0;
			while (!flag && endIndex >= startIndex)
			{
				num = startIndex + (endIndex - startIndex) / 2;
				RenderingPagesRanges renderingPagesRanges = pagesRangesList[num];
				if (renderingPagesRanges.StartPage > page)
				{
					endIndex = num - 1;
				}
				else if (renderingPagesRanges.EndPage < page)
				{
					startIndex = num + 1;
				}
				else
				{
					flag = true;
					startChild = num;
					endChild = num;
					if (checkStart && renderingPagesRanges.StartPage == page)
					{
						RenderingContext.FindRange(pagesRangesList, startIndex, num - 1, page, true, false, ref startChild, ref num2);
					}
					if (checkEnd && renderingPagesRanges.EndPage == page)
					{
						RenderingContext.FindRange(pagesRangesList, num + 1, endIndex, page, false, true, ref num2, ref endChild);
					}
				}
			}
		}
	}
}

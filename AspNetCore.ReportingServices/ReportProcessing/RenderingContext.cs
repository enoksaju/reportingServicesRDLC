using AspNetCore.ReportingServices.Diagnostics;
using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	public sealed class RenderingContext
	{
		private ICatalogItemContext m_reportContext;

		private string m_reportDescription;

		private EventInformation m_eventInfo;

		public ReportProcessing.GetReportChunk m_getReportChunkCallback;

		public ReportProcessing.GetChunkMimeType m_getChunkMimeType;

		private ReportProcessing.StoreServerParameters m_storeServerParameters;

		private UserProfileState m_allowUserProfileState;

		private ReportRuntimeSetup m_reportRuntimeSetup;

		private PaginationMode m_clientPaginationMode;

		private int m_previousTotalPages;

		public string Format
		{
			get
			{
				return this.m_reportContext.RSRequestParameters.FormatParamValue;
			}
		}

		public Uri ReportUri
		{
			get
			{
				string hostRootUri = this.m_reportContext.HostRootUri;
				if (string.IsNullOrEmpty(hostRootUri))
				{
					return null;
				}
				string uriString = new CatalogItemUrlBuilder(this.m_reportContext).ToString();
				return new Uri(uriString);
			}
		}

		public string ShowHideToggle
		{
			get
			{
				return this.m_reportContext.RSRequestParameters.ShowHideToggleParamValue;
			}
		}

		public ICatalogItemContext ReportContext
		{
			get
			{
				return this.m_reportContext;
			}
		}

		public string ReportDescription
		{
			get
			{
				return this.m_reportDescription;
			}
		}

		public EventInformation EventInfo
		{
			get
			{
				return this.m_eventInfo;
			}
			set
			{
				this.m_eventInfo = value;
			}
		}

		public ReportProcessing.StoreServerParameters StoreServerParametersCallback
		{
			get
			{
				return this.m_storeServerParameters;
			}
		}

		public UserProfileState AllowUserProfileState
		{
			get
			{
				return this.m_allowUserProfileState;
			}
		}

		public ReportRuntimeSetup ReportRuntimeSetup
		{
			get
			{
				return this.m_reportRuntimeSetup;
			}
		}

		public PaginationMode ClientPaginationMode
		{
			get
			{
				return this.m_clientPaginationMode;
			}
		}

		public int PreviousTotalPages
		{
			get
			{
				return this.m_previousTotalPages;
			}
		}

		public RenderingContext(ICatalogItemContext reportContext, string reportDescription, EventInformation eventInfo, ReportRuntimeSetup reportRuntimeSetup, ReportProcessing.StoreServerParameters storeServerParameters, UserProfileState allowUserProfileState, PaginationMode clientPaginationMode, int previousTotalPages)
		{
			Global.Tracer.Assert(null != reportContext, "(null != reportContext)");
			this.m_reportContext = reportContext;
			this.m_reportDescription = reportDescription;
			this.m_eventInfo = eventInfo;
			this.m_storeServerParameters = storeServerParameters;
			this.m_allowUserProfileState = allowUserProfileState;
			this.m_reportRuntimeSetup = reportRuntimeSetup;
			this.m_clientPaginationMode = clientPaginationMode;
			this.m_previousTotalPages = previousTotalPages;
		}

		public Hashtable GetRenderProperties(bool reprocessSnapshot)
		{
			Hashtable hashtable = new Hashtable(4);
			if (reprocessSnapshot)
			{
				hashtable.Add("ClientPaginationMode", this.m_clientPaginationMode);
				hashtable.Add("PreviousTotalPages", 0);
			}
			else
			{
				hashtable.Add("ClientPaginationMode", this.m_clientPaginationMode);
				hashtable.Add("PreviousTotalPages", this.m_previousTotalPages);
			}
			return hashtable;
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace AspNetCore.Reporting
{

	public abstract class Report
	{
		private string m_displayName = "";

		private int m_drillthroughDepth = 1;

		public object m_syncObject = new object();

		[NotifyParentProperty(true)]		
		[DefaultValue("")]		
		public string DisplayName
		{
			get
			{
				return this.m_displayName;
			}
			set
			{
				this.m_displayName = value;
			}
		}
        public event EventHandler<ReportChangedEventArgs> Change;
        public abstract string DisplayNameForUse
		{
			get;
		}

		[Browsable(false)]
		public bool IsDrillthroughReport
		{
			get
			{
				return this.DrillthroughDepth > 1;
			}
		}

		public int DrillthroughDepth
		{
			get
			{
				return this.m_drillthroughDepth;
			}
			set
			{
				this.m_drillthroughDepth = value;
			}
		}

		public abstract bool IsReadyForConnection
		{
			get;
		}

		[Browsable(false)]
		public bool IsReadyForRendering
		{
			get
			{
				try
				{
					return this.PrepareForRender();
				}
				catch
				{
					return false;
				}
			}
		}

		public abstract bool IsPreparedReportReadyForRendering
		{
			get;
		}

		public abstract bool HasDocMap
		{
			get;
		}

		public abstract int AutoRefreshInterval
		{
			get;
		}

		public abstract bool CanSelfCancel
		{
			get;
		}


		public Report()
		{
		}

		public abstract ReportParameterInfoCollection GetParameters();

		public abstract ParametersPaneLayout GetParametersPaneLayout();

		public abstract void SetParameters(IEnumerable<ReportParameter> parameters);

		public abstract int GetTotalPages(out PageCountMode pageCountMode);

		public abstract RenderingExtension[] ListRenderingExtensions();

		public abstract void LoadReportDefinition(TextReader report);

		public abstract void Refresh();

		public abstract byte[] Render(string format, string deviceInfo, PageCountMode pageCountMode, out string mimeType, out Encoding encoding, out string fileNameExtension, out string[] streams, out Warning[] warnings);

		public abstract byte[] InternalRenderStream(string format, string streamID, string deviceInfo, out string mimeType, out Encoding encoding);

		public abstract void InternalDeliverReportItem(string format, string deviceInfo, ExtensionSettings settings, string description, string eventType, string matchData);

		public abstract int PerformSearch(string searchText, int startPage, int endPage);

		public abstract void PerformToggle(string toggleId);

		public abstract int PerformBookmarkNavigation(string bookmarkId, out string uniqueName);

		public abstract int PerformDocumentMapNavigation(string documentMapId);

		public abstract ReportPageSettings GetDefaultPageSettings();

		public int GetTotalPages()
		{
			PageCountMode pageCountMode = default(PageCountMode);
			return this.GetTotalPages(out pageCountMode);
		}

		public byte[] Render(string format)
		{
			return this.Render(format, null);
		}

		public byte[] Render(string format, string deviceInfo)
		{
			string text = default(string);
			string text3 = default(string);
			string[] array = default(string[]);
			Warning[] array2 = default(Warning[]);
			return this.Render(format, deviceInfo, out text, out Encoding text2, out text3, out array, out array2);
		}

		public byte[] Render(string format, string deviceInfo, out string mimeType, out Encoding encoding, out string fileNameExtension, out string[] streams, out Warning[] warnings)
		{
			return this.Render(format, deviceInfo, PageCountMode.Estimate, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
		}
 

		public abstract Report PerformDrillthrough(string drillthroughId, out string reportPath);

		public abstract int PerformSort(string sortId, SortOrder sortDirection, bool clearSort, PageCountMode pageCountMode, out string uniqueName);

		public bool PrepareForRender()
		{
			lock (this.m_syncObject)
			{
				if (this.IsReadyForConnection)
				{
					this.EnsureExecutionSession();
					return this.IsPreparedReportReadyForRendering;
				}
				return false;
			}
		}

		public abstract void EnsureExecutionSession();

		public abstract DocumentMapNode GetDocumentMap(string rootLabel);

		public abstract void SetCancelState(bool shouldCancel);

		public void LoadReportDefinition(Stream report)
		{
			if (report == null)
			{
				throw new ArgumentNullException("report");
			}
			this.LoadReportDefinition(new StreamReader(report));
		}

		public void OnChange(bool isRefreshOnly)
		{
			if (this.Change != null)
			{
				this.Change(this, new ReportChangedEventArgs(isRefreshOnly));
			}
		}

		public void OnChange(object sender, EventArgs e)
		{
			this.OnChange(false);
		}

		public virtual string CreatePrintRequestQuery(string InstanceID)
		{
			return PrintRequestOperation.CreateQuery(this, InstanceID);
		}

		public void SetParameters(ReportParameter parameter)
		{
			this.SetParameters(new ReportParameter[1]
			{
				parameter
			});
		}
	}
}

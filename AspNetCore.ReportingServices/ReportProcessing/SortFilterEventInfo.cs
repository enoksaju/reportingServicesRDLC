using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class SortFilterEventInfo
	{
		[Reference]
		private TextBox m_eventSource;

		private VariantList[] m_eventSourceScopeInfo;

		public TextBox EventSource
		{
			get
			{
				return this.m_eventSource;
			}
			set
			{
				this.m_eventSource = value;
			}
		}

		public VariantList[] EventSourceScopeInfo
		{
			get
			{
				return this.m_eventSourceScopeInfo;
			}
			set
			{
				this.m_eventSourceScopeInfo = value;
			}
		}

		public SortFilterEventInfo()
		{
		}

		public SortFilterEventInfo(TextBox eventSource)
		{
			this.m_eventSource = eventSource;
		}

		public static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.EventSource, Token.Reference, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.TextBox));
			memberInfoList.Add(new MemberInfo(MemberName.EventSourceScopeInfo, Token.Array, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.VariantList));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.None, memberInfoList);
		}
	}
}

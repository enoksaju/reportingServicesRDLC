using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ReportInstance : ReportItemInstance, IPageItem
	{
		private ReportItemColInstance m_reportItemColInstance;

		private string m_language;

		private int m_numberOfPages;

		[NonSerialized]
		private ReportInstanceInfo m_cachedInstanceInfo;

		[NonSerialized]
		private bool m_noRows;

		[NonSerialized]
		private int m_startPage = -1;

		[NonSerialized]
		private int m_endPage = -1;

		public ReportItemColInstance ReportItemColInstance
		{
			get
			{
				return this.m_reportItemColInstance;
			}
			set
			{
				this.m_reportItemColInstance = value;
			}
		}

		public string Language
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

		public int NumberOfPages
		{
			get
			{
				return this.m_numberOfPages;
			}
			set
			{
				this.m_numberOfPages = value;
			}
		}

		public bool NoRows
		{
			get
			{
				return this.m_noRows;
			}
		}

		int IPageItem.StartPage
		{
			get
			{
				return this.m_startPage;
			}
			set
			{
				this.m_startPage = value;
			}
		}

		int IPageItem.EndPage
		{
			get
			{
				return this.m_endPage;
			}
			set
			{
				this.m_endPage = value;
			}
		}

		public ReportInstance(ReportProcessing.ProcessingContext pc, Report reportItemDef, ParameterInfoCollection parameters, string reportlanguage, bool noRows)
			: base(pc.CreateUniqueName(), reportItemDef)
		{
			base.m_instanceInfo = new ReportInstanceInfo(pc, reportItemDef, this, parameters, noRows);
			pc.Pagination.EnterIgnoreHeight(reportItemDef.StartHidden);
			this.m_reportItemColInstance = new ReportItemColInstance(pc, reportItemDef.ReportItems);
			this.m_language = reportlanguage;
			this.m_noRows = noRows;
		}

		public ReportInstance()
		{
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.ReportItemColInstance, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemColInstance));
			memberInfoList.Add(new MemberInfo(MemberName.Language, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.NumberOfPages, Token.Int32));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemInstance, memberInfoList);
		}

		protected override object SearchChildren(int targetUniqueName, ref NonComputedUniqueNames nonCompNames, ChunkManager.RenderingChunkManager chunkManager)
		{
			return ((ISearchByUniqueName)this.m_reportItemColInstance).Find(targetUniqueName, ref nonCompNames, chunkManager);
		}

		public ReportInstanceInfo GetCachedReportInstanceInfo(ChunkManager.RenderingChunkManager chunkManager)
		{
			if (base.m_instanceInfo is OffsetInfo)
			{
				if (this.m_cachedInstanceInfo == null)
				{
					IntermediateFormatReader reader = chunkManager.GetReader(((OffsetInfo)base.m_instanceInfo).Offset);
					this.m_cachedInstanceInfo = reader.ReadReportInstanceInfo((Report)base.m_reportItemDef);
				}
				return this.m_cachedInstanceInfo;
			}
			return (ReportInstanceInfo)base.m_instanceInfo;
		}

		public override ReportItemInstanceInfo ReadInstanceInfo(IntermediateFormatReader reader)
		{
			Global.Tracer.Assert(base.m_instanceInfo is OffsetInfo);
			return reader.ReadReportInstanceInfo((Report)base.m_reportItemDef);
		}
	}
}

using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ListInstance : ReportItemInstance, IPageItem
	{
		private ListContentInstanceList m_listContentInstances;

		private RenderingPagesRangesList m_renderingPages;

		[NonSerialized]
		private int m_numberOfContentsOnThisPage;

		[NonSerialized]
		private int m_startPage = -1;

		[NonSerialized]
		private int m_endPage = -1;

		public ListContentInstanceList ListContents
		{
			get
			{
				return this.m_listContentInstances;
			}
			set
			{
				this.m_listContentInstances = value;
			}
		}

		public RenderingPagesRangesList ChildrenStartAndEndPages
		{
			get
			{
				return this.m_renderingPages;
			}
			set
			{
				this.m_renderingPages = value;
			}
		}

		public int NumberOfContentsOnThisPage
		{
			get
			{
				return this.m_numberOfContentsOnThisPage;
			}
			set
			{
				this.m_numberOfContentsOnThisPage = value;
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

		public ListInstance(ReportProcessing.ProcessingContext pc, List reportItemDef)
			: base(pc.CreateUniqueName(), reportItemDef)
		{
			base.m_instanceInfo = new ListInstanceInfo(pc, reportItemDef, this);
			this.m_listContentInstances = new ListContentInstanceList();
			this.m_renderingPages = new RenderingPagesRangesList();
		}

		public ListInstance(ReportProcessing.ProcessingContext pc, List reportItemDef, ListContentInstanceList listContentInstances, RenderingPagesRangesList renderingPages)
			: base(pc.CreateUniqueName(), reportItemDef)
		{
			base.m_instanceInfo = new ListInstanceInfo(pc, reportItemDef, this);
			this.m_listContentInstances = listContentInstances;
			this.m_renderingPages = renderingPages;
		}

		public ListInstance()
		{
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.ListContentInstances, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ListContentInstanceList));
			memberInfoList.Add(new MemberInfo(MemberName.ChildrenStartAndEndPages, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.RenderingPagesRangesList));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemInstance, memberInfoList);
		}

		protected override object SearchChildren(int targetUniqueName, ref NonComputedUniqueNames nonCompNames, ChunkManager.RenderingChunkManager chunkManager)
		{
			int count = this.m_listContentInstances.Count;
			for (int i = 0; i < count; i++)
			{
				object obj = ((ISearchByUniqueName)this.m_listContentInstances[i]).Find(targetUniqueName, ref nonCompNames, chunkManager);
				if (obj != null)
				{
					return obj;
				}
			}
			return null;
		}

		public override ReportItemInstanceInfo ReadInstanceInfo(IntermediateFormatReader reader)
		{
			Global.Tracer.Assert(base.m_instanceInfo is OffsetInfo);
			return reader.ReadListInstanceInfo((List)base.m_reportItemDef);
		}
	}
}

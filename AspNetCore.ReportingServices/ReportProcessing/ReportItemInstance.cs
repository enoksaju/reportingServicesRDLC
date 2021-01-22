using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public abstract class ReportItemInstance : InstanceInfoOwner, ISearchByUniqueName
	{
		protected int m_uniqueName;

		[Reference]
		protected ReportItem m_reportItemDef;

		public int UniqueName
		{
			get
			{
				return this.m_uniqueName;
			}
			set
			{
				this.m_uniqueName = value;
			}
		}

		public ReportItem ReportItemDef
		{
			get
			{
				return this.m_reportItemDef;
			}
			set
			{
				this.m_reportItemDef = value;
			}
		}

		public ReportItemInstance(int uniqueName, ReportItem reportItemDef)
		{
			this.m_uniqueName = uniqueName;
			this.m_reportItemDef = reportItemDef;
		}

		public ReportItemInstance()
		{
		}

		object ISearchByUniqueName.Find(int targetUniqueName, ref NonComputedUniqueNames nonCompNames, ChunkManager.RenderingChunkManager chunkManager)
		{
			if (this.m_uniqueName == targetUniqueName)
			{
				nonCompNames = null;
				return this;
			}
			return this.SearchChildren(targetUniqueName, ref nonCompNames, chunkManager);
		}

		protected virtual object SearchChildren(int targetUniqueName, ref NonComputedUniqueNames nonCompNames, ChunkManager.RenderingChunkManager chunkManager)
		{
			return null;
		}

		public virtual int GetDocumentMapUniqueName()
		{
			return this.m_uniqueName;
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.UniqueName, Token.Int32));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.InstanceInfoOwner, memberInfoList);
		}

		public ReportItemInstanceInfo GetInstanceInfo(ChunkManager.RenderingChunkManager chunkManager)
		{
			return this.GetInstanceInfo(chunkManager, false);
		}

		public ReportItemInstanceInfo GetInstanceInfo(ChunkManager.RenderingChunkManager chunkManager, bool inPageSection)
		{
			if (base.m_instanceInfo is OffsetInfo)
			{
				Global.Tracer.Assert(null != chunkManager);
				IntermediateFormatReader intermediateFormatReader = null;
				intermediateFormatReader = ((!inPageSection) ? chunkManager.GetReader(((OffsetInfo)base.m_instanceInfo).Offset) : chunkManager.GetPageSectionInstanceReader(((OffsetInfo)base.m_instanceInfo).Offset));
				return this.ReadInstanceInfo(intermediateFormatReader);
			}
			return (ReportItemInstanceInfo)base.m_instanceInfo;
		}

		public abstract ReportItemInstanceInfo ReadInstanceInfo(IntermediateFormatReader reader);
	}
}

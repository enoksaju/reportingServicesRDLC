using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ChartHeadingInstance : InstanceInfoOwner
	{
		private int m_uniqueName;

		[Reference]
		private ChartHeading m_chartHeadingDef;

		private ChartHeadingInstanceList m_subHeadingInstances;

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

		public ChartHeading ChartHeadingDef
		{
			get
			{
				return this.m_chartHeadingDef;
			}
			set
			{
				this.m_chartHeadingDef = value;
			}
		}

		public ChartHeadingInstanceList SubHeadingInstances
		{
			get
			{
				return this.m_subHeadingInstances;
			}
			set
			{
				this.m_subHeadingInstances = value;
			}
		}

		public ChartHeadingInstanceInfo InstanceInfo
		{
			get
			{
				if (base.m_instanceInfo is OffsetInfo)
				{
					Global.Tracer.Assert(false, string.Empty);
					return null;
				}
				return (ChartHeadingInstanceInfo)base.m_instanceInfo;
			}
		}

		public ChartHeadingInstance(ReportProcessing.ProcessingContext pc, int headingCellIndex, ChartHeading chartHeadingDef, int labelIndex, VariantList groupExpressionValues)
		{
			this.m_uniqueName = pc.CreateUniqueName();
			if (chartHeadingDef.SubHeading != null)
			{
				this.m_subHeadingInstances = new ChartHeadingInstanceList();
			}
			base.m_instanceInfo = new ChartHeadingInstanceInfo(pc, headingCellIndex, chartHeadingDef, labelIndex, groupExpressionValues);
			this.m_chartHeadingDef = chartHeadingDef;
		}

		public ChartHeadingInstance()
		{
		}

		public ChartHeadingInstanceInfo GetInstanceInfo(ChunkManager.RenderingChunkManager chunkManager)
		{
			if (base.m_instanceInfo is OffsetInfo)
			{
				IntermediateFormatReader reader = chunkManager.GetReader(((OffsetInfo)base.m_instanceInfo).Offset);
				return reader.ReadChartHeadingInstanceInfo();
			}
			return (ChartHeadingInstanceInfo)base.m_instanceInfo;
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.UniqueName, Token.Int32));
			memberInfoList.Add(new MemberInfo(MemberName.SubHeadingInstances, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ChartHeadingInstanceList));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.InstanceInfoOwner, memberInfoList);
		}
	}
}

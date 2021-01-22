using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class OWCChartInstance : ReportItemInstance, IPageItem
	{
		[NonSerialized]
		private int m_startPage = -1;

		[NonSerialized]
		private int m_endPage = -1;

		public OWCChartInstanceInfo InstanceInfo
		{
			get
			{
				if (base.m_instanceInfo is OffsetInfo)
				{
					Global.Tracer.Assert(false, string.Empty);
					return null;
				}
				return (OWCChartInstanceInfo)base.m_instanceInfo;
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

		public OWCChartInstance(ReportProcessing.ProcessingContext pc, OWCChart reportItemDef)
			: base(pc.CreateUniqueName(), reportItemDef)
		{
			base.m_instanceInfo = new OWCChartInstanceInfo(pc, reportItemDef, this);
			pc.QuickFind.Add(base.UniqueName, this);
		}

		public OWCChartInstance(ReportProcessing.ProcessingContext pc, OWCChart reportItemDef, VariantList[] chartData)
			: base(pc.CreateUniqueName(), reportItemDef)
		{
			base.m_instanceInfo = new OWCChartInstanceInfo(pc, reportItemDef, this, chartData);
			pc.QuickFind.Add(base.UniqueName, this);
		}

		public OWCChartInstance()
		{
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList members = new MemberInfoList();
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemInstance, members);
		}

		public override ReportItemInstanceInfo ReadInstanceInfo(IntermediateFormatReader reader)
		{
			Global.Tracer.Assert(base.m_instanceInfo is OffsetInfo);
			return reader.ReadOWCChartInstanceInfo((OWCChart)base.m_reportItemDef);
		}
	}
}

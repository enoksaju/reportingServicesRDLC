using AspNetCore.ReportingServices.ReportIntermediateFormat;
using AspNetCore.ReportingServices.ReportProcessing;
using System.Globalization;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class BodyInstance : ReportElementInstance
	{
		public string UniqueName
		{
			get
			{
				if (base.m_reportElementDef.IsOldSnapshot)
				{
					ReportInstanceInfo instanceInfo = this.BodyDefinition.RenderReport.InstanceInfo;
					if (instanceInfo != null)
					{
						return instanceInfo.BodyUniqueName.ToString(CultureInfo.InvariantCulture);
					}
					return string.Empty;
				}
				AspNetCore.ReportingServices.ReportIntermediateFormat.ReportSection sectionDef = this.BodyDefinition.SectionDef;
				return InstancePathItem.GenerateUniqueNameString(sectionDef.ID, sectionDef.InstancePath) + "xB";
			}
		}

		public Body BodyDefinition
		{
			get
			{
				return (Body)base.m_reportElementDef;
			}
		}

		public BodyInstance(Body bodyDef)
			: base(bodyDef)
		{
		}
	}
}

using System.Collections.Generic;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public class SpatialElementInfoGroup
	{
		public List<SpatialElementInfo> Elements = new List<SpatialElementInfo>();

		public bool BoundToData;
	}
}

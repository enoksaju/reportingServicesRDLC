using AspNetCore.ReportingServices.RdlObjectModel;
using AspNetCore.ReportingServices.RdlObjectModel2005.Upgrade;

namespace AspNetCore.ReportingServices.RdlObjectModel2005
{
	public interface IReportItem2005 : IUpgradeable
	{
		Action Action
		{
			get;
			set;
		}
	}
}

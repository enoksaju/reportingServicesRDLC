using System.IO;

namespace AspNetCore.ReportingServices.ReportPublishing
{
	public abstract class ReportUpgradeStrategy
	{
		public abstract Stream Upgrade(Stream definitionStream);
	}
}

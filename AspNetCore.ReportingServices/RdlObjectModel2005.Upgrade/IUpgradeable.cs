namespace AspNetCore.ReportingServices.RdlObjectModel2005.Upgrade
{
	public interface IUpgradeable
	{
		void Upgrade(UpgradeImpl2005 upgrader);
	}
}

namespace AspNetCore.ReportingServices.RdlObjectModel2008.Upgrade
{
	public interface IUpgradeable2008
	{
		void Upgrade(UpgradeImpl2008 upgrader);
	}
}

namespace AspNetCore.ReportingServices.RdlObjectModel2010.Upgrade
{
	public interface IUpgradeable2010
	{
		void Upgrade(UpgradeImpl2010 upgrader);
	}
}

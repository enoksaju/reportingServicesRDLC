namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public interface ITransferable
	{
		void TransferTo(IScalabilityCache scaleCache);
	}
}

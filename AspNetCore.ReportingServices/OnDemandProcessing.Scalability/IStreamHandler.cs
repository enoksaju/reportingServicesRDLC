using System.IO;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public interface IStreamHandler
	{
		Stream OpenStream();
	}
}

using System.IO;

namespace AspNetCore.Reporting
{
	public interface ITemporaryStorage
	{
		Stream CreateTemporaryStream();
	}
}

using System.IO;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	public interface IChunkFactory
	{
		ReportProcessingFlags ReportProcessingFlags
		{
			get;
		}

		Stream CreateChunk(string chunkName, ReportProcessing.ReportChunkTypes type, string mimeType);

		Stream GetChunk(string chunkName, ReportProcessing.ReportChunkTypes type, ChunkMode mode, out string mimeType);

		bool Erase(string chunkName, ReportProcessing.ReportChunkTypes type);
	}
}

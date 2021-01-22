namespace AspNetCore.ReportingServices.ReportProcessing
{
	public interface ISearchByUniqueName
	{
		object Find(int targetUniqueName, ref NonComputedUniqueNames nonCompNames, ChunkManager.RenderingChunkManager chunkManager);
	}
}

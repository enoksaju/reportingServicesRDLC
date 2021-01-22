namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public struct Space
	{
		public long Offset;

		public long Size;

		public Space(long freeOffset, long freeSize)
		{
			this.Offset = freeOffset;
			this.Size = freeSize;
		}
	}
}

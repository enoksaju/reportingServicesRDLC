namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public interface IIndexStrategy
	{
		ReferenceID MaxId
		{
			get;
		}

		ReferenceID GenerateId(ReferenceID tempId);

		ReferenceID GenerateTempId();

		long Retrieve(ReferenceID id);

		void Update(ReferenceID id, long value);

		void Close();
	}
}

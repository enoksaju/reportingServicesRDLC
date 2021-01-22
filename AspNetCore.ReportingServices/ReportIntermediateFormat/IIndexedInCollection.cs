namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	public interface IIndexedInCollection
	{
		int IndexInCollection
		{
			get;
			set;
		}

		IndexedInCollectionType IndexedInCollectionType
		{
			get;
		}
	}
}

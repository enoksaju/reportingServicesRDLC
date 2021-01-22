namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class RdlCollection<T> : RdlCollectionBase<T>
	{
		public RdlCollection()
		{
		}

		public RdlCollection(IContainedObject parent)
			: base(parent)
		{
		}
	}
}

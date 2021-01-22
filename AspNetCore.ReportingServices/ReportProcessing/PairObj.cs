namespace AspNetCore.ReportingServices.ReportProcessing
{
	public class PairObj<T, U>
	{
		public T First;

		public U Second;

		public PairObj(T first, U second)
		{
			this.First = first;
			this.Second = second;
		}
	}
}

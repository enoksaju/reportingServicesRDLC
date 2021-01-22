namespace AspNetCore.ReportingServices.ReportPublishing
{
	public class Holder<T> where T : struct
	{
		private T m_t = default(T);

		public T Value
		{
			get
			{
				return this.m_t;
			}
			set
			{
				this.m_t = value;
			}
		}
	}
}

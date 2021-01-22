namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.ExcelGenerator.BIFF8.Records
{
	public struct Pair<T, U>
	{
		private T m_first;

		private U m_second;

		public T First
		{
			get
			{
				return this.m_first;
			}
		}

		public U Second
		{
			get
			{
				return this.m_second;
			}
		}

		public Pair(T first, U second)
		{
			this.m_first = first;
			this.m_second = second;
		}
	}
}

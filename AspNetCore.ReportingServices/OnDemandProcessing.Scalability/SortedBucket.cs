namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public sealed class SortedBucket
	{
		public int Limit;

		public int Minimum;

		public Heap<long, Space> m_spaces;

		public int Count
		{
			get
			{
				return this.m_spaces.Count;
			}
		}

		public int Maximum
		{
			get
			{
				return (int)this.Peek().Size;
			}
		}

		public SortedBucket(int maxSpacesPerBucket)
		{
			int num = 500;
			if (num > maxSpacesPerBucket)
			{
				num = maxSpacesPerBucket;
			}
			this.m_spaces = new Heap<long, Space>(num, maxSpacesPerBucket);
			this.Minimum = 2147483647;
		}

		public SortedBucket Split(int maxSpacesPerBucket)
		{
			SortedBucket sortedBucket = new SortedBucket(maxSpacesPerBucket);
			int num = this.Count / 2;
			for (int i = 0; i < num; i++)
			{
				sortedBucket.Insert(this.ExtractMax());
			}
			sortedBucket.Limit = sortedBucket.Minimum;
			return sortedBucket;
		}

		public void Insert(Space space)
		{
			if (space.Size < this.Minimum)
			{
				this.Minimum = (int)space.Size;
			}
			this.m_spaces.Insert(space.Size, space);
		}

		public Space Peek()
		{
			return this.m_spaces.Peek();
		}

		public Space ExtractMax()
		{
			Space result = this.m_spaces.ExtractMax();
			if (this.m_spaces.Count == 0)
			{
				this.Minimum = 2147483647;
			}
			return result;
		}
	}
}

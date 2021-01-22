using System;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public class ItemHolder
	{
		public ItemHolder Previous;

		public ItemHolder Next;

		public IStorable Item;

		[NonSerialized]
		public BaseReference Reference;

		[NonSerialized]
		public InQueueState InQueue;

		public ItemHolder()
		{
		}

		public virtual int ComputeSizeForReference()
		{
			return this.BaseSize() + ItemSizes.ReferenceSize;
		}

		public int BaseSize()
		{
			return ItemSizes.ReferenceSize + ItemSizes.ReferenceSize + 1 + ItemSizes.ReferenceSize;
		}
	}
}

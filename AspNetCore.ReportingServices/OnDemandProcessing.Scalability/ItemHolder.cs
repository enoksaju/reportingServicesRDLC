using System;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	internal class ItemHolder
	{
		internal ItemHolder Previous;

		internal ItemHolder Next;

		internal IStorable Item;

		[NonSerialized]
		internal BaseReference Reference;

		[NonSerialized]
		internal InQueueState InQueue;

		internal ItemHolder()
		{
		}

		internal virtual int ComputeSizeForReference()
		{
			return this.BaseSize() + ItemSizes.ReferenceSize;
		}

		internal int BaseSize()
		{
			return ItemSizes.ReferenceSize + ItemSizes.ReferenceSize + 1 + ItemSizes.ReferenceSize;
		}
	}
}

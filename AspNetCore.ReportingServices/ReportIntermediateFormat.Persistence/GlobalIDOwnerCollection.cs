using System.Collections.Generic;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence
{
	public class GlobalIDOwnerCollection
	{
		private int m_currentID = -1;

		private Dictionary<int, IGloballyReferenceable> m_globallyReferenceableItems;

		public int LastAssignedID
		{
			get
			{
				return this.m_currentID;
			}
		}

		public GlobalIDOwnerCollection()
		{
			this.m_globallyReferenceableItems = new Dictionary<int, IGloballyReferenceable>(EqualityComparers.Int32ComparerInstance);
		}

		public int GetGlobalID()
		{
			return ++this.m_currentID;
		}

		public void Add(IGloballyReferenceable globallyReferenceableItem)
		{
			this.m_globallyReferenceableItems.Add(this.m_currentID, globallyReferenceableItem);
		}

		public bool TryGetValue(int refID, out IGloballyReferenceable referenceableItem)
		{
			return this.m_globallyReferenceableItems.TryGetValue(refID, out referenceableItem);
		}
	}
}

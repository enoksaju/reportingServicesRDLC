using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class BookmarksHashtable : HashtableInstanceInfo
	{
		public BookmarkInformation this[string key]
		{
			get
			{
				return (BookmarkInformation)base.m_hashtable[key];
			}
			set
			{
				base.m_hashtable[key] = value;
			}
		}

		public BookmarksHashtable()
		{
		}

		public BookmarksHashtable(int capacity)
			: base(capacity)
		{
		}

		public void Add(string bookmark, BookmarkInformation bookmarkInfo)
		{
			base.m_hashtable.Add(bookmark, bookmarkInfo);
		}

		public void Add(string bookmark, int page, string id)
		{
			BookmarkInformation bookmarkInformation = null;
			if (base.m_hashtable.Contains(bookmark))
			{
				bookmarkInformation = this[bookmark];
				if (bookmarkInformation.Page > page)
				{
					bookmarkInformation.Page = page;
					bookmarkInformation.Id = id;
				}
			}
			else
			{
				bookmarkInformation = new BookmarkInformation(id, page);
				base.m_hashtable.Add(bookmark, bookmarkInformation);
			}
		}
	}
}

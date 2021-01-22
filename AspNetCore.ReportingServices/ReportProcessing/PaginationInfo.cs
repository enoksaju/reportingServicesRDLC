using AspNetCore.ReportingServices.ReportRendering;
using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class PaginationInfo
	{
		private ArrayList m_pages;

		private int m_totalPageNumber;

		public int TotalPageNumber
		{
			get
			{
				return this.m_totalPageNumber;
			}
			set
			{
				this.m_totalPageNumber = value;
			}
		}

		public Page this[int pageNumber]
		{
			get
			{
				return (Page)this.m_pages[pageNumber];
			}
			set
			{
				this.m_pages[pageNumber] = value;
			}
		}

		public int CurrentPageCount
		{
			get
			{
				return this.m_pages.Count;
			}
		}

		public PaginationInfo()
		{
			this.m_pages = new ArrayList();
		}

		public void AddPage(Page page)
		{
			this.m_pages.Add(page);
		}

		public void Clear()
		{
			this.m_pages.Clear();
		}

		public void InsertPage(int pageNumber, Page page)
		{
			this.m_pages.Insert(pageNumber, page);
		}

		public void RemovePage(int pageNumber)
		{
			this.m_pages.RemoveAt(pageNumber);
		}
	}
}

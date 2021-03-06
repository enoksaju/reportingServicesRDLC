using AspNetCore.ReportingServices.Rendering.RichText;
using AspNetCore.ReportingServices.Rendering.RPLProcessing;
using System.Collections.Generic;
using System.IO;

namespace AspNetCore.ReportingServices.Rendering.HPBProcessing
{
	public sealed class RPLWriter
	{
		private RPLReport m_rplReport;

		private BinaryWriter m_binaryWriter;

		private RPLTablixRow m_tablixRow;

		private int m_delayedTBLevels;

		private List<Dictionary<string, List<object>>> m_currentDelayedTB;

		private byte[] m_copyBuffer;

		private bool m_cacheRichData;

		private SectionItemizedData m_sectionItemizedData;

		private Dictionary<string, List<TextRunItemizedData>> m_pageParagraphsData;

		private List<SectionItemizedData> m_glyphCache;

		public RPLReport Report
		{
			get
			{
				return this.m_rplReport;
			}
			set
			{
				this.m_rplReport = value;
			}
		}

		public RPLTablixRow TablixRow
		{
			get
			{
				return this.m_tablixRow;
			}
			set
			{
				this.m_tablixRow = value;
			}
		}

		public BinaryWriter BinaryWriter
		{
			get
			{
				return this.m_binaryWriter;
			}
			set
			{
				this.m_binaryWriter = value;
			}
		}

		public int DelayedTBLevels
		{
			get
			{
				return this.m_delayedTBLevels;
			}
			set
			{
				this.m_delayedTBLevels = value;
			}
		}

		public byte[] CopyBuffer
		{
			get
			{
				if (this.m_copyBuffer == null)
				{
					this.m_copyBuffer = new byte[1024];
				}
				return this.m_copyBuffer;
			}
		}

		public Dictionary<string, List<TextRunItemizedData>> PageParagraphsItemizedData
		{
			get
			{
				if (this.m_cacheRichData)
				{
					return this.m_pageParagraphsData;
				}
				return null;
			}
			set
			{
				this.m_pageParagraphsData = value;
			}
		}

		public List<SectionItemizedData> GlyphCache
		{
			get
			{
				return this.m_glyphCache;
			}
		}

		public void AddTextBoxes(Dictionary<string, List<object>> delayedTextBoxes)
		{
			if (this.m_currentDelayedTB == null)
			{
				this.m_currentDelayedTB = new List<Dictionary<string, List<object>>>();
			}
			this.m_currentDelayedTB.Add(delayedTextBoxes);
		}

		public void AddTextBox(string name, object value)
		{
			if (this.m_currentDelayedTB == null)
			{
				this.m_currentDelayedTB = new List<Dictionary<string, List<object>>>();
			}
			Dictionary<string, List<object>> dictionary = new Dictionary<string, List<object>>(1);
			List<object> list = new List<object>(1);
			list.Add(value);
			dictionary.Add(name, list);
			this.m_currentDelayedTB.Add(dictionary);
		}

		public void EnterDelayedTBLevel(bool isLTR, ref RTLTextBoxes delayedTB)
		{
			if (!isLTR)
			{
				delayedTB = new RTLTextBoxes(this.m_currentDelayedTB);
				this.m_currentDelayedTB = null;
				this.m_delayedTBLevels++;
			}
		}

		public void RegisterCellTextBoxes(bool isLTR, RTLTextBoxes delayedTB)
		{
			if (!isLTR)
			{
				delayedTB.Push(this.m_currentDelayedTB);
				this.m_currentDelayedTB = null;
			}
		}

		public void LeaveDelayedTBLevel(bool isLTR, RTLTextBoxes delayedTB, PageContext pageContext)
		{
			if (!isLTR)
			{
				this.m_delayedTBLevels--;
				if (this.m_delayedTBLevels > 0)
				{
					this.m_currentDelayedTB = delayedTB.RegisterRTLLevel();
				}
				else
				{
					delayedTB.RegisterTextBoxes(pageContext);
				}
			}
		}

		public void RegisterCacheRichData(bool cacheRichData)
		{
			this.m_cacheRichData = cacheRichData;
			if (this.m_cacheRichData && this.m_pageParagraphsData == null)
			{
				this.m_pageParagraphsData = new Dictionary<string, List<TextRunItemizedData>>();
			}
		}

		public void RegisterSectionItemizedData()
		{
			if (this.m_sectionItemizedData == null)
			{
				this.m_sectionItemizedData = new SectionItemizedData();
			}
			if (this.m_pageParagraphsData != null && this.m_pageParagraphsData.Count == 0)
			{
				this.m_pageParagraphsData = null;
			}
			this.m_sectionItemizedData.Columns.Add(this.m_pageParagraphsData);
			this.m_pageParagraphsData = null;
		}

		public void RegisterSectionHeaderFooter()
		{
			if (this.m_sectionItemizedData == null)
			{
				this.m_sectionItemizedData = new SectionItemizedData();
			}
			if (this.m_pageParagraphsData != null && this.m_pageParagraphsData.Count == 0)
			{
				this.m_pageParagraphsData = null;
			}
			this.m_sectionItemizedData.HeaderFooter = this.m_pageParagraphsData;
			this.m_pageParagraphsData = null;
		}

		public void RegisterPageItemizedData()
		{
			if (this.m_glyphCache == null)
			{
				this.m_glyphCache = new List<SectionItemizedData>();
			}
			if (this.m_sectionItemizedData != null && this.m_sectionItemizedData.Columns.Count == 0 && this.m_sectionItemizedData.HeaderFooter == null)
			{
				this.m_sectionItemizedData = null;
			}
			this.m_glyphCache.Add(this.m_sectionItemizedData);
			this.m_sectionItemizedData = null;
		}
	}
}

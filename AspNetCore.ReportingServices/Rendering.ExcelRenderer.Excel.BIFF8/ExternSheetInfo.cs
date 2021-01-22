using System.Collections.Generic;

namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.Excel.BIFF8
{
	public class ExternSheetInfo
	{
		public class XTI
		{
			public const int LENGTH = 6;

			private ushort m_firstTab;

			private ushort m_lastTab;

			private ushort m_supBookIndex;

			public ushort SupBookIndex
			{
				get
				{
					return this.m_supBookIndex;
				}
			}

			public ushort FirstTab
			{
				get
				{
					return this.m_firstTab;
				}
			}

			public ushort LastTab
			{
				get
				{
					return this.m_lastTab;
				}
			}

			public XTI(ushort supBookIndex, ushort firstTab, ushort lastTab)
			{
				this.m_firstTab = firstTab;
				this.m_lastTab = lastTab;
				this.m_supBookIndex = supBookIndex;
			}
		}

		private List<XTI> m_xtiStructures;

		public List<XTI> XTIStructures
		{
			get
			{
				return this.m_xtiStructures;
			}
		}

		public ExternSheetInfo()
		{
			this.m_xtiStructures = new List<XTI>();
		}

		public int AddXTI(ushort supBookIndex, ushort firstTab, ushort lastTab)
		{
			this.m_xtiStructures.Add(new XTI(supBookIndex, firstTab, lastTab));
			return this.m_xtiStructures.Count - 1;
		}
	}
}

using System.Collections.Generic;

namespace AspNetCore.ReportingServices.Rendering.SPBProcessing
{
	public class PageTablixHelper : PageItemHelper
	{
		private int m_levelForRepeat;

		private List<int> m_tablixCreateState;

		private List<int> m_membersInstanceIndex;

		private bool m_ignoreTotalsOnLastLevel;

		public int LevelForRepeat
		{
			get
			{
				return this.m_levelForRepeat;
			}
			set
			{
				this.m_levelForRepeat = value;
			}
		}

		public bool IgnoreTotalsOnLastLevel
		{
			get
			{
				return this.m_ignoreTotalsOnLastLevel;
			}
			set
			{
				this.m_ignoreTotalsOnLastLevel = value;
			}
		}

		public List<int> TablixCreateState
		{
			get
			{
				return this.m_tablixCreateState;
			}
			set
			{
				this.m_tablixCreateState = value;
			}
		}

		public List<int> MembersInstanceIndex
		{
			get
			{
				return this.m_membersInstanceIndex;
			}
			set
			{
				this.m_membersInstanceIndex = value;
			}
		}

		public PageTablixHelper(byte type)
			: base(type)
		{
		}
	}
}

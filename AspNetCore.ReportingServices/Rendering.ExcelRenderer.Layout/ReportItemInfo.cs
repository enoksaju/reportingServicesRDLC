using AspNetCore.ReportingServices.Rendering.ExcelRenderer.SPBIFReader.Callbacks;
using AspNetCore.ReportingServices.Rendering.RPLProcessing;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.Layout
{
	public sealed class ReportItemInfo
	{
		private object m_rplSource;

		private int m_top;

		private int m_left;

		private int m_right;

		private int m_alignmentPoint;

		private RPLTextBoxProps m_textBox;

		private Dictionary<string, ToggleParent> m_toggleParents;

		private bool m_isHidden;

		public object RPLSource
		{
			get
			{
				return this.m_rplSource;
			}
		}

		public int Top
		{
			get
			{
				return this.m_top;
			}
		}

		public int Left
		{
			get
			{
				return this.m_left;
			}
		}

		public int Right
		{
			get
			{
				return this.m_right;
			}
		}

		public RPLTextBoxProps Values
		{
			get
			{
				return this.m_textBox;
			}
			set
			{
				this.m_textBox = value;
			}
		}

		public int AlignmentPoint
		{
			get
			{
				return this.m_alignmentPoint;
			}
			set
			{
				this.m_alignmentPoint = value;
			}
		}

		public bool IsHidden
		{
			get
			{
				return this.m_isHidden;
			}
		}

		public Dictionary<string, ToggleParent> ToggleParents
		{
			get
			{
				return this.m_toggleParents;
			}
		}

		public ReportItemInfo(object aRplSource, int aTop, int aLeft, int aRight, bool aIsHidden, Dictionary<string, ToggleParent> aToggleParents)
		{
			this.m_rplSource = aRplSource;
			this.m_top = aTop;
			this.m_left = aLeft;
			this.m_right = aRight;
			this.m_isHidden = aIsHidden;
			this.m_toggleParents = aToggleParents;
		}

		public static int CompareTopsThenLefts(ReportItemInfo aLeft, ReportItemInfo aRight)
		{
			int num = aLeft.Top.CompareTo(aRight.Top);
			if (num == 0)
			{
				return aLeft.Left.CompareTo(aRight.Left);
			}
			return num;
		}
	}
}

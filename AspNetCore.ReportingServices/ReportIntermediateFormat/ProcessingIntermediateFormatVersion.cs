using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	[Serializable]
	public sealed class ProcessingIntermediateFormatVersion
	{
		private IntermediateFormatVersion m_version;

		public int Major
		{
			get
			{
				return this.m_version.Major;
			}
			set
			{
				this.m_version.Major = value;
			}
		}

		public int Minor
		{
			get
			{
				return this.m_version.Minor;
			}
			set
			{
				this.m_version.Minor = value;
			}
		}

		public int Build
		{
			get
			{
				return this.m_version.Build;
			}
			set
			{
				this.m_version.Build = value;
			}
		}

		public bool IsOldVersion
		{
			get
			{
				return this.m_version.IsOldVersion;
			}
		}

		public bool IsRIF11_orOlder
		{
			get
			{
				return this.m_version.CompareTo(11, 0, 0) <= 0;
			}
		}

		public bool IsRIF11_orNewer
		{
			get
			{
				return this.m_version.CompareTo(11, 0, 0) >= 0;
			}
		}

		public bool IsRS2000_Beta2_orOlder
		{
			get
			{
				return this.m_version.CompareTo(8, 0, 673) <= 0;
			}
		}

		public bool IsRS2000_WithSpecialRecursiveAggregates
		{
			get
			{
				return this.m_version.CompareTo(8, 0, 700) >= 0;
			}
		}

		public bool IsRS2000_WithNewChartYAxis
		{
			get
			{
				return this.m_version.CompareTo(8, 0, 713) >= 0;
			}
		}

		public bool IsRS2000_WithOtherPageChunkSplit
		{
			get
			{
				return this.m_version.CompareTo(8, 0, 716) >= 0;
			}
		}

		public bool IsRS2000_RTM_orOlder
		{
			get
			{
				return this.m_version.CompareTo(8, 0, 743) <= 0;
			}
		}

		public bool IsRS2000_RTM_orNewer
		{
			get
			{
				return this.m_version.CompareTo(8, 0, 743) >= 0;
			}
		}

		public bool IsRS2000_WithUnusedFieldsOptimization
		{
			get
			{
				return this.m_version.CompareTo(8, 0, 801) >= 0;
			}
		}

		public bool IsRS2000_WithImageInfo
		{
			get
			{
				return this.m_version.CompareTo(8, 0, 843) >= 0;
			}
		}

		public bool IsRS2005_Beta2_orOlder
		{
			get
			{
				return this.m_version.CompareTo(9, 0, 852) <= 0;
			}
		}

		public bool IsRS2005_WithMultipleActions
		{
			get
			{
				return this.m_version.CompareTo(9, 0, 937) >= 0;
			}
		}

		public bool IsRS2005_WithSpecialChunkSplit
		{
			get
			{
				return this.m_version.CompareTo(9, 0, 937) >= 0;
			}
		}

		public bool IsRS2005_IDW9_orOlder
		{
			get
			{
				return this.m_version.CompareTo(9, 0, 951) <= 0;
			}
		}

		public bool IsRS2005_WithTableDetailFix
		{
			get
			{
				return this.m_version.CompareTo(10, 2, 0) >= 0;
			}
		}

		public bool IsRS2005_WithPHFChunks
		{
			get
			{
				return this.m_version.CompareTo(10, 3, 0) >= 0;
			}
		}

		public bool IsRS2005_WithTableOptimizations
		{
			get
			{
				return this.m_version.CompareTo(10, 4, 0) >= 0;
			}
		}

		public bool IsRS2005_WithSharedDrillthroughParams
		{
			get
			{
				return this.m_version.CompareTo(10, 8, 0) >= 0;
			}
		}

		public bool IsRS2005_WithSimpleTextBoxOptimizations
		{
			get
			{
				return this.m_version.CompareTo(10, 5, 0) >= 0;
			}
		}

		public bool IsRS2005_WithChartHeadingInstanceFix
		{
			get
			{
				return this.m_version.CompareTo(10, 6, 0) >= 0;
			}
		}

		public bool IsRS2005_WithXmlDataElementOutputChange
		{
			get
			{
				return this.m_version.CompareTo(10, 7, 0) >= 0;
			}
		}

		public bool Is_WithUserSort
		{
			get
			{
				return this.m_version.CompareTo(9, 0, 970) >= 0;
			}
		}

		public ProcessingIntermediateFormatVersion(IntermediateFormatVersion version)
		{
			this.m_version = version;
		}

		public override string ToString()
		{
			return this.m_version.ToString();
		}
	}
}

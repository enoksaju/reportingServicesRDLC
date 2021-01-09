using System;

namespace AspNetCore.ReportingServices.ReportPublishing
{
	[Flags]
	internal enum LocationFlags
	{
		None = 1,
		InDataSet = 2,
		InDataRegion = 4,
		InGrouping = 8,
		InDetail = 0x10,
		InDynamicTablixCell = 0x20,
		InPageSection = 0x40,
		InTablixSubtotal = 0x80,
		InDataRegionCellTopLevelItem = 0x100,
		InTablix = 0x200,
		InDataRegionGroupHeader = 0x400,
		InNonToggleableHiddenStaticTablixMember = 0x1000,
		InParameter = 0x2000,
		InTablixCell = 0x4000,
		InTablixRowHierarchy = 0x8000,
		InTablixColumnHierarchy = 0x10000
	}
}

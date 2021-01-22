using System;
using System.Data;

namespace AspNetCore.Reporting.Map.WebForms
{
	public class DesignDataTableSource : DataTable, IDesignTimeDataSource, IDisposable
	{
		public DesignDataTableSource(MapCore mapCore, object originalDataSource)
		{
			DataBindingHelper.InitDesignDataTable(originalDataSource, string.Empty, this);
		}
	}
}

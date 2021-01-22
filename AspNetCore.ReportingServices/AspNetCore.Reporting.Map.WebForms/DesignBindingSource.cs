using System;
using System.Windows.Forms;

namespace AspNetCore.Reporting.Map.WebForms
{
	public class DesignBindingSource : BindingSource, IDesignTimeDataSource, IDisposable
	{
		public DesignBindingSource(object dataSource, string dataMember)
			: base(dataSource, dataMember)
		{
		}
	}
}

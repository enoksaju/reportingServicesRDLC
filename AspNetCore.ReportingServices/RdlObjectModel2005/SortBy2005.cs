using AspNetCore.ReportingServices.RdlObjectModel;

namespace AspNetCore.ReportingServices.RdlObjectModel2005
{
	public class SortBy2005 : SortExpression
	{
		public ReportExpression SortExpression
		{
			get
			{
				return base.Value;
			}
			set
			{
				base.Value = value;
			}
		}

		public SortBy2005()
		{
		}

		public SortBy2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}

using AspNetCore.ReportingServices.ReportProcessing.ExprHostObjectModel;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	internal interface ISortFilterScope
	{
		int ID
		{
			get;
		}

		string ScopeName
		{
			get;
		}

		bool[] IsSortFilterTarget
		{
			get;
			set;
		}

		bool[] IsSortFilterExpressionScope
		{
			get;
			set;
		}

		ExpressionInfoList UserSortExpressions
		{
			get;
			set;
		}

		IndexedExprHost UserSortExpressionsHost
		{
			get;
		}
	}
}

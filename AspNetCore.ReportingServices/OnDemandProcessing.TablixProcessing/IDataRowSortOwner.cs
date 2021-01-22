using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing
{
	public interface IDataRowSortOwner
	{
		OnDemandProcessingContext OdpContext
		{
			get;
		}

		Sorting SortingDef
		{
			get;
		}

		void PostDataRowSortNextRow();

		void DataRowSortTraverse();

		object EvaluateDataRowSortExpression(RuntimeExpressionInfo expression);
	}
}

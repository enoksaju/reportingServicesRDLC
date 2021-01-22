using AspNetCore.ReportingServices.OnDemandProcessing.Scalability;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using AspNetCore.ReportingServices.ReportProcessing;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing
{
	public interface IHierarchyObj : IStorable, IPersistable
	{
		IReference<IHierarchyObj> HierarchyRoot
		{
			get;
		}

		OnDemandProcessingContext OdpContext
		{
			get;
		}

		BTree SortTree
		{
			get;
		}

		int ExpressionIndex
		{
			get;
		}

		int Depth
		{
			get;
		}

		List<int> SortFilterInfoIndices
		{
			get;
		}

		bool IsDetail
		{
			get;
		}

		bool InDataRowSortPhase
		{
			get;
		}

		IHierarchyObj CreateHierarchyObjForSortTree();

		ProcessingMessageList RegisterComparisonError(string propertyName);

		void NextRow(IHierarchyObj owner);

		void Traverse(ProcessingStages operation, ITraversalContext traversalContext);

		void ReadRow();

		void ProcessUserSort();

		void MarkSortInfoProcessed(List<IReference<RuntimeSortFilterEventInfo>> runtimeSortFilterInfo);

		void AddSortInfoIndex(int sortInfoIndex, IReference<RuntimeSortFilterEventInfo> sortInfo);
	}
}

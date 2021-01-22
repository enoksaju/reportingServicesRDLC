using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using AspNetCore.ReportingServices.ReportProcessing;
using System;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing
{
	[PersistedWithinRequestOnly]
	public class RuntimeGroupingObjDetailUserSort : RuntimeGroupingObj
	{
		[NonSerialized]
		private static Declaration m_declaration = RuntimeGroupingObjDetailUserSort.GetDeclaration();

		public override int Size
		{
			get
			{
				return base.Size;
			}
		}

		public RuntimeGroupingObjDetailUserSort()
		{
		}

		public RuntimeGroupingObjDetailUserSort(RuntimeHierarchyObj owner, AspNetCore.ReportingServices.ReportProcessing.ObjectType objectType)
			: base(owner, objectType)
		{
		}

		public override void Cleanup()
		{
		}

		public override void NextRow(object keyValue, bool hasParent, object parentKey)
		{
			Global.Tracer.Assert(false, "This implementation of RuntimeGroupingObj does not support NextRow");
		}

		public override void Traverse(ProcessingStages operation, bool ascending, ITraversalContext traversalContext)
		{
			RuntimeGroupRootObj runtimeGroupRootObj = base.m_owner as RuntimeGroupRootObj;
			Global.Tracer.Assert(null != runtimeGroupRootObj, "(null != groupRootOwner)");
			runtimeGroupRootObj.GroupOrDetailSortTree.Traverse(operation, ascending, traversalContext);
		}

		public override void CopyDomainScopeGroupInstances(RuntimeGroupRootObj destination)
		{
			Global.Tracer.Assert(false, "Domain Scope should only be applied to Hash groups");
		}

		public override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RuntimeGroupingObjDetailUserSort.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		public override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RuntimeGroupingObjDetailUserSort.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		public override void ResolveReferences(Dictionary<AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
		}

		public override AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObjDetailUserSort;
		}

		public new static Declaration GetDeclaration()
		{
			if (RuntimeGroupingObjDetailUserSort.m_declaration == null)
			{
				List<MemberInfo> memberInfoList = new List<MemberInfo>();
				return new Declaration(AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObjDetailUserSort, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObj, memberInfoList);
			}
			return RuntimeGroupingObjDetailUserSort.m_declaration;
		}
	}
}

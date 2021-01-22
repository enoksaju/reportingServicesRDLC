using AspNetCore.ReportingServices.OnDemandProcessing.Scalability;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using AspNetCore.ReportingServices.ReportProcessing;
using System;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing
{
	[PersistedWithinRequestOnly]
	public abstract class RuntimeGroupingObj : IStorable, IPersistable
	{
		public enum GroupingTypes
		{
			None,
			Hash,
			Sort,
			Detail,
			DetailUserSort,
			NaturalGroup
		}

		[NonSerialized]
		protected RuntimeHierarchyObj m_owner;

		protected AspNetCore.ReportingServices.ReportProcessing.ObjectType m_objectType;

		[NonSerialized]
		private static Declaration m_declaration = RuntimeGroupingObj.GetDeclaration();

		public virtual BTree Tree
		{
			get
			{
				Global.Tracer.Assert(false, "Tree is only available for sort based groupings.");
				return null;
			}
		}

		public virtual int Size
		{
			get
			{
				return ItemSizes.ReferenceSize + 4;
			}
		}

		public RuntimeGroupingObj()
		{
		}

		public RuntimeGroupingObj(RuntimeHierarchyObj owner, AspNetCore.ReportingServices.ReportProcessing.ObjectType objectType)
		{
			this.m_owner = owner;
			this.m_objectType = objectType;
		}

		public static RuntimeGroupingObj CreateGroupingObj(GroupingTypes type, RuntimeHierarchyObj owner, AspNetCore.ReportingServices.ReportProcessing.ObjectType objectType)
		{
			switch (type)
			{
			case GroupingTypes.None:
				return new RuntimeGroupingObjLinkedList(owner, objectType);
			case GroupingTypes.Hash:
				return new RuntimeGroupingObjHash(owner, objectType);
			case GroupingTypes.Sort:
				return new RuntimeGroupingObjTree(owner, objectType);
			case GroupingTypes.Detail:
				return new RuntimeGroupingObjDetail(owner, objectType);
			case GroupingTypes.DetailUserSort:
				return new RuntimeGroupingObjDetailUserSort(owner, objectType);
			case GroupingTypes.NaturalGroup:
				return new RuntimeGroupingObjNaturalGroup(owner, objectType);
			default:
				Global.Tracer.Assert(false, "Unexpected GroupingTypes");
				throw new InvalidOperationException();
			}
		}

		public abstract void Cleanup();

		public void NextRow(object keyValue)
		{
			this.NextRow(keyValue, false, null);
		}

		public abstract void NextRow(object keyValue, bool hasParent, object parentKey);

		public abstract void Traverse(ProcessingStages operation, bool ascending, ITraversalContext traversalContext);

		public abstract void CopyDomainScopeGroupInstances(RuntimeGroupRootObj destination);

		public void SetOwner(RuntimeHierarchyObj owner)
		{
			this.m_owner = owner;
		}

		public virtual void Serialize(IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(RuntimeGroupingObj.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName == MemberName.ObjectType)
				{
					writer.WriteEnum((int)this.m_objectType);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		public virtual void Deserialize(IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(RuntimeGroupingObj.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName == MemberName.ObjectType)
				{
					this.m_objectType = (AspNetCore.ReportingServices.ReportProcessing.ObjectType)reader.ReadEnum();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		public virtual void ResolveReferences(Dictionary<AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
		}

		public virtual AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObj;
		}

		public static Declaration GetDeclaration()
		{
			if (RuntimeGroupingObj.m_declaration == null)
			{
				List<MemberInfo> list = new List<MemberInfo>();
				list.Add(new MemberInfo(MemberName.ObjectType, Token.Enum));
				return new Declaration(AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObj, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, list);
			}
			return RuntimeGroupingObj.m_declaration;
		}
	}
}

using AspNetCore.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using AspNetCore.ReportingServices.ReportProcessing;
using AspNetCore.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using AspNetCore.ReportingServices.ReportPublishing;
using System;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	public abstract class JoinInfo : IPersistable
	{
		protected List<IdcRelationship> m_relationships;

		[NonSerialized]
		private static readonly Declaration m_Declaration = JoinInfo.GetDeclaration();

		public List<IdcRelationship> Relationships
		{
			get
			{
				return this.m_relationships;
			}
		}

		public JoinInfo()
		{
		}

		public JoinInfo(IdcRelationship relationship)
		{
			this.m_relationships = new List<IdcRelationship>(1);
			this.m_relationships.Add(relationship);
		}

		public JoinInfo(List<IdcRelationship> relationships)
		{
			this.m_relationships = relationships;
		}

		public void SetJoinConditionExprHost(IList<JoinConditionExprHost> joinConditionExprHost, ObjectModelImpl reportObjectModel)
		{
			if (this.m_relationships != null)
			{
				foreach (IdcRelationship relationship in this.m_relationships)
				{
					relationship.SetExprHost(joinConditionExprHost, reportObjectModel);
				}
			}
		}

		protected Relationship GetActiveRelationship(DataSet ourDataSet, DataSet parentDataSet)
		{
			if (this.m_relationships == null && (ourDataSet == null || ourDataSet.DefaultRelationships == null))
			{
				return null;
			}
			Relationship relationship = JoinInfo.FindActiveRelationship(this.m_relationships, parentDataSet);
			if (relationship == null && ourDataSet != null)
			{
				relationship = ourDataSet.GetDefaultRelationship(parentDataSet);
			}
			return relationship;
		}

		public static T FindActiveRelationship<T>(List<T> relationships, DataSet parentDataSet) where T : Relationship
		{
			if (relationships != null)
			{
				foreach (T relationship in relationships)
				{
					T current = relationship;
					if (DataSet.AreEqualById(current.RelatedDataSet, parentDataSet))
					{
						return current;
					}
				}
			}
			return null;
		}

		public void Initialize(InitializationContext context)
		{
			if (this.m_relationships != null && 0 < this.m_relationships.Count)
			{
				foreach (IdcRelationship relationship in this.m_relationships)
				{
					relationship.Initialize(context);
				}
			}
		}

		public JoinInfo PublishClone(AutomaticSubtotalContext context)
		{
			Global.Tracer.Assert(false, "IDC does not support automatic subtotals");
			throw new InvalidOperationException();
		}

		public abstract bool ValidateRelationships(ScopeTree scopeTree, ErrorContext errorContext, DataSet ourDataSet, ParentDataSetContainer parentDataSets, IRIFReportDataScope currentScope);

		public abstract void CheckContainerJoinForNaturalJoin(IRIFDataScope startScope, ErrorContext errorContext, IRIFDataScope scope);

		protected void CheckContainerRelationshipForNaturalJoin(IRIFDataScope startScope, ErrorContext errorContext, IRIFDataScope scope, Relationship outerRelationship)
		{
			if (outerRelationship != null && !outerRelationship.NaturalJoin)
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidRelationshipContainerNotNaturalJoin, Severity.Error, startScope.DataScopeObjectType, startScope.Name, "Relationship", scope.DataScopeObjectType.ToString(), scope.Name);
			}
		}

		public abstract void ValidateScopeRulesForIdcNaturalJoin(InitializationContext context, IRIFDataScope scope);

		protected void ValidateScopeRulesForIdcNaturalJoin(InitializationContext context, IRIFDataScope startScopeForValidation, Relationship relationship)
		{
			if (relationship != null && relationship.NaturalJoin)
			{
				context.ValidateScopeRulesForIdcNaturalJoin(startScopeForValidation);
			}
		}

		public abstract void AddMappedFieldIndices(List<int> parentFieldIndices, DataSet parentDataSet, DataSet ourDataSet, List<int> ourFieldIndices);

		protected static void AddMappedFieldIndices(Relationship relationship, List<int> parentFieldIndices, List<int> ourFieldIndices)
		{
			foreach (int parentFieldIndex in parentFieldIndices)
			{
				int item = default(int);
				if (relationship.TryMapFieldIndex(parentFieldIndex, out item))
				{
					ourFieldIndices.Add(item);
				}
			}
		}

		public static Declaration GetDeclaration()
		{
			List<MemberInfo> list = new List<MemberInfo>();
			list.Add(new MemberInfo(MemberName.Relationships, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IdcRelationship));
			return new Declaration(AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.JoinInfo, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, list);
		}

		public virtual void Serialize(IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(JoinInfo.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName == MemberName.Relationships)
				{
					writer.Write(this.m_relationships);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		public virtual void Deserialize(IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(JoinInfo.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName == MemberName.Relationships)
				{
					this.m_relationships = reader.ReadGenericListOfRIFObjects<IdcRelationship>();
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
			return AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.JoinInfo;
		}
	}
}

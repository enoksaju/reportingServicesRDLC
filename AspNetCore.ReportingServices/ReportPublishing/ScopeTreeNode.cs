using AspNetCore.ReportingServices.ReportIntermediateFormat;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.ReportPublishing
{
	public abstract class ScopeTreeNode
	{
		protected readonly IRIFDataScope m_scope;

		private readonly List<IRIFDataScope> m_childScopes = new List<IRIFDataScope>();

		public IRIFDataScope Scope
		{
			get
			{
				return this.m_scope;
			}
		}

		public List<IRIFDataScope> ChildScopes
		{
			get
			{
				return this.m_childScopes;
			}
		}

		public abstract string ScopeName
		{
			get;
		}

		public ScopeTreeNode(IRIFDataScope scope)
		{
			this.m_scope = scope;
		}

		public void AddChildScope(IRIFDataScope child)
		{
			this.m_childScopes.Add(child);
		}

		public abstract bool IsSameOrParentScope(IRIFDataScope parentScope, bool isProperParent);

		public abstract void Traverse(ScopeTree.ScopeTreeVisitor visitor, IRIFDataScope outerScope, bool visitOuterScope);

		public abstract bool Traverse(ScopeTree.DirectedScopeTreeVisitor visitor);

		protected static bool TraverseNode(ScopeTree.DirectedScopeTreeVisitor visitor, ScopeTreeNode node)
		{
			if (node != null)
			{
				return node.Traverse(visitor);
			}
			return true;
		}
	}
}

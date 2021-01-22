using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.ReportPublishing
{
	public class SubScopeNode : ScopeTreeNode
	{
		private readonly ScopeTreeNode m_parentScope;

		public ScopeTreeNode ParentScope
		{
			get
			{
				return this.m_parentScope;
			}
		}

		public override string ScopeName
		{
			get
			{
				return base.m_scope.Name;
			}
		}

		public SubScopeNode(IRIFDataScope scope, ScopeTreeNode parentScope)
			: base(scope)
		{
			this.m_parentScope = parentScope;
			if (this.m_parentScope != null)
			{
				this.m_parentScope.AddChildScope(scope);
			}
		}

		public override bool IsSameOrParentScope(IRIFDataScope parentScope, bool isProperParent)
		{
			if (parentScope == base.Scope)
			{
				return true;
			}
			if (this.m_parentScope == null)
			{
				return false;
			}
			return this.m_parentScope.IsSameOrParentScope(parentScope, isProperParent);
		}

		public override void Traverse(ScopeTree.ScopeTreeVisitor visitor, IRIFDataScope outerScope, bool visitOuterScope)
		{
			bool flag = outerScope == base.Scope;
			if (visitOuterScope || !flag)
			{
				visitor(base.Scope);
			}
			if (!flag && this.m_parentScope != null)
			{
				this.m_parentScope.Traverse(visitor, outerScope, visitOuterScope);
			}
		}

		public override bool Traverse(ScopeTree.DirectedScopeTreeVisitor visitor)
		{
			if (visitor(base.Scope))
			{
				return ScopeTreeNode.TraverseNode(visitor, this.m_parentScope);
			}
			return false;
		}
	}
}

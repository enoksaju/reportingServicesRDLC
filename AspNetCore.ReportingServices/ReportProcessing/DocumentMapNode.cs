using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class DocumentMapNode : InstanceInfo
	{
		private string m_id;

		private string m_label;

		private int m_page = -1;

		private DocumentMapNode[] m_children;

		public string Label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
			}
		}

		public string Id
		{
			get
			{
				return this.m_id;
			}
			set
			{
				this.m_id = value;
			}
		}

		public int Page
		{
			get
			{
				return this.m_page;
			}
			set
			{
				this.m_page = value;
			}
		}

		public DocumentMapNode[] Children
		{
			get
			{
				return this.m_children;
			}
			set
			{
				this.m_children = value;
			}
		}

		public DocumentMapNode()
		{
		}

		public DocumentMapNode(string id, string label, int page, ArrayList children)
		{
			Global.Tracer.Assert(id != null, "The id of a document map node cannot be null.");
			this.m_id = id;
			this.m_label = label;
			this.m_page = page;
			if (children != null && children.Count > 0)
			{
				this.m_children = (DocumentMapNode[])children.ToArray(typeof(DocumentMapNode));
			}
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.Id, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.Label, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.DocMapPage, Token.Int32));
			memberInfoList.Add(new MemberInfo(MemberName.Children, Token.Array, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.DocumentMapNode));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.InstanceInfo, memberInfoList);
		}
	}
}

using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using AspNetCore.ReportingServices.ReportProcessing;
using System;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	[Serializable]
	public sealed class DocumentMapNode : IPersistable
	{
		public const char IdLevelSeparator = '_';

		private string m_id;

		private string m_label;

		[NonSerialized]
		private static readonly Declaration m_Declaration = DocumentMapNode.GetDeclaration();

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
				Global.Tracer.Assert(value != null, "The id of a document map node cannot be null.");
				this.m_id = value;
			}
		}

		public DocumentMapNode()
		{
		}

		public static Declaration GetDeclaration()
		{
			List<MemberInfo> list = new List<MemberInfo>();
			list.Add(new MemberInfo(MemberName.ID, Token.String));
			list.Add(new MemberInfo(MemberName.Label, Token.String));
			return new Declaration(AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DocumentMapNode, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, list);
		}

		public void Serialize(IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(DocumentMapNode.m_Declaration);
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case MemberName.ID:
					writer.Write(this.m_id);
					break;
				case MemberName.Label:
					writer.Write(this.m_label);
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		public void Deserialize(IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(DocumentMapNode.m_Declaration);
			while (reader.NextMember())
			{
				switch (reader.CurrentMember.MemberName)
				{
				case MemberName.ID:
					this.m_id = reader.ReadString();
					break;
				case MemberName.Label:
					this.m_label = reader.ReadString();
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		public void ResolveReferences(Dictionary<AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		public AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DocumentMapNode;
		}
	}
}

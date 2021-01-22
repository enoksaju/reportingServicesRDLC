using AspNetCore.ReportingServices.Diagnostics.Utilities;
using AspNetCore.ReportingServices.OnDemandProcessing.Scalability;
using AspNetCore.ReportingServices.OnDemandReportRendering;
using AspNetCore.ReportingServices.Rendering.RPLProcessing;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.Rendering.HPBProcessing
{
	public sealed class ReportBody : PageItemContainer
	{
		[StaticReference]
		private new Body m_source;

		private double m_originalWidth;

		private static Declaration m_declaration = ReportBody.GetDeclaration();

		public override string SourceID
		{
			get
			{
				return this.m_source.ID;
			}
		}

		public override string SourceUniqueName
		{
			get
			{
				return this.m_source.Instance.UniqueName;
			}
		}

		public override double OriginalLeft
		{
			get
			{
				return 0.0;
			}
		}

		public override double OriginalWidth
		{
			get
			{
				return this.m_originalWidth;
			}
		}

		public override Style SharedStyle
		{
			get
			{
				return this.m_source.Style;
			}
		}

		public override StyleInstance NonSharedStyle
		{
			get
			{
				return this.m_source.Instance.Style;
			}
		}

		public override byte RPLFormatType
		{
			get
			{
				return 6;
			}
		}

		public override int Size
		{
			get
			{
				return base.Size + AspNetCore.ReportingServices.OnDemandProcessing.Scalability.ItemSizes.ReferenceSize + 8;
			}
		}

		public ReportBody()
		{
		}

		public ReportBody(Body source, ReportSize width)
			: base(null)
		{
			base.m_itemPageSizes = new ItemSizes(0.0, 0.0, width.ToMillimeters(), source.Height.ToMillimeters());
			this.m_originalWidth = base.m_itemPageSizes.Width;
			this.m_source = source;
			base.KeepTogetherHorizontal = false;
			base.KeepTogetherVertical = false;
			bool unresolvedKTV = base.UnresolvedKTH = false;
			base.UnresolvedKTV = unresolvedKTV;
		}

		public override RPLElement CreateRPLElement()
		{
			return new RPLBody();
		}

		public override RPLElement CreateRPLElement(RPLElementProps props, PageContext pageContext)
		{
			RPLItemProps props2 = props as RPLItemProps;
			return new RPLBody(props2);
		}

		public override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ReportBody.m_declaration);
			IScalabilityCache scalabilityCache = writer.PersistenceHelper as IScalabilityCache;
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case MemberName.BodySource:
				{
					int value = scalabilityCache.StoreStaticReference(this.m_source);
					writer.Write(value);
					break;
				}
				case MemberName.Width:
					writer.Write(this.m_originalWidth);
					break;
				default:
					RSTrace.RenderingTracer.Assert(false, string.Empty);
					break;
				}
			}
		}

		public override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ReportBody.m_declaration);
			IScalabilityCache scalabilityCache = reader.PersistenceHelper as IScalabilityCache;
			while (reader.NextMember())
			{
				switch (reader.CurrentMember.MemberName)
				{
				case MemberName.BodySource:
				{
					int id = reader.ReadInt32();
					this.m_source = (Body)scalabilityCache.FetchStaticReference(id);
					break;
				}
				case MemberName.Width:
					this.m_originalWidth = reader.ReadDouble();
					break;
				default:
					RSTrace.RenderingTracer.Assert(false, string.Empty);
					break;
				}
			}
		}

		public override ObjectType GetObjectType()
		{
			return ObjectType.ReportBody;
		}

		public new static Declaration GetDeclaration()
		{
			if (ReportBody.m_declaration == null)
			{
				List<MemberInfo> list = new List<MemberInfo>();
				list.Add(new MemberInfo(MemberName.BodySource, Token.Int32));
				list.Add(new MemberInfo(MemberName.Width, Token.Double));
				return new Declaration(ObjectType.ReportBody, ObjectType.PageItemContainer, list);
			}
			return ReportBody.m_declaration;
		}

		protected override void CreateChildren(PageContext pageContext)
		{
			base.CreateChildren(this.m_source.ReportItemCollection, pageContext);
		}
	}
}

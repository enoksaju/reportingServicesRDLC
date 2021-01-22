using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using AspNetCore.ReportingServices.ReportProcessing;
using System;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class ReportElementInstance : BaseInstance, IPersistable
	{
		[NonSerialized]
		protected ReportElement m_reportElementDef;

		protected StyleInstance m_style;

		private static readonly Declaration m_Declaration = ReportElementInstance.GetDeclaration();

		public virtual StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_reportElementDef, this.m_reportElementDef.ReportScope, this.m_reportElementDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		public ReportElement ReportElementDef
		{
			get
			{
				return this.m_reportElementDef;
			}
		}

		public ReportElementInstance(ReportElement reportElementDef)
			: base(reportElementDef.ReportScope)
		{
			this.m_reportElementDef = reportElementDef;
		}

		public override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
		}

		protected override void ResetInstanceCache()
		{
		}

		void IPersistable.Serialize(IntermediateFormatWriter writer)
		{
			this.Serialize(writer);
		}

		void IPersistable.Deserialize(IntermediateFormatReader reader)
		{
			this.Deserialize(reader);
		}

		void IPersistable.ResolveReferences(Dictionary<AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType IPersistable.GetObjectType()
		{
			return this.GetObjectType();
		}

		public virtual void Serialize(IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ReportElementInstance.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName == MemberName.Style)
				{
					writer.Write(this.Style);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		public virtual void Deserialize(IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ReportElementInstance.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName == MemberName.Style)
				{
					reader.ReadRIFObject();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		public virtual AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportElementInstance;
		}

		private static Declaration GetDeclaration()
		{
			List<MemberInfo> list = new List<MemberInfo>();
			list.Add(new MemberInfo(MemberName.Style, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StyleInstance));
			return new Declaration(AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportElementInstance, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, list);
		}
	}
}

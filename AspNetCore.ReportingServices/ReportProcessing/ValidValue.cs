using AspNetCore.ReportingServices.Diagnostics;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	internal sealed class ValidValue : IPersistable
	{
		private object m_value;

		private string m_label;

		[NonSerialized]
		private string m_stringValue;

		[NonSerialized]
		private string m_cachedAutogenLabel;

		[NonSerialized]
		private bool m_labelAutoGenerated;

		[NonSerialized]
		private static readonly AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ValidValue.GetNewDeclaration();

		public object Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
				this.m_cachedAutogenLabel = null;
			}
		}

		public string Label
		{
			get
			{
				if (this.m_label == null)
				{
					if (!this.m_labelAutoGenerated)
					{
						this.m_labelAutoGenerated = true;
						if (this.m_value != null)
						{
							this.m_cachedAutogenLabel = ParameterInfo.CastValueToLabelString(this.m_value, Localization.ClientPrimaryCulture);
						}
					}
					return this.m_cachedAutogenLabel;
				}
				return this.m_label;
			}
			set
			{
				this.m_label = value;
				this.m_cachedAutogenLabel = null;
				this.m_labelAutoGenerated = false;
			}
		}

		public string LabelRaw
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.Label = value;
			}
		}

		public string StringValue
		{
			get
			{
				return this.m_stringValue;
			}
		}

		public ValidValue()
		{
		}

		public ValidValue(string validValue, string label)
		{
			this.m_stringValue = validValue;
			this.m_label = label;
		}

		public ValidValue(object validValue, string label)
		{
			this.m_value = validValue;
			this.m_label = label;
		}

		internal void EnsureLabelIsGenerated()
		{
			if (this.m_label == null && this.m_cachedAutogenLabel == null && this.m_value != null)
			{
				this.m_cachedAutogenLabel = ParameterInfo.CastValueToLabelString(this.m_value, Thread.CurrentThread.CurrentCulture);
			}
			this.m_labelAutoGenerated = true;
		}

		internal static AspNetCore.ReportingServices.ReportProcessing.Persistence.Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new AspNetCore.ReportingServices.ReportProcessing.Persistence.MemberInfo(AspNetCore.ReportingServices.ReportProcessing.Persistence.MemberName.Label, AspNetCore.ReportingServices.ReportProcessing.Persistence.Token.String));
			memberInfoList.Add(new AspNetCore.ReportingServices.ReportProcessing.Persistence.MemberInfo(AspNetCore.ReportingServices.ReportProcessing.Persistence.MemberName.Value, AspNetCore.ReportingServices.ReportProcessing.Persistence.Token.Object));
			return new AspNetCore.ReportingServices.ReportProcessing.Persistence.Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.None, memberInfoList);
		}

		internal static AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetNewDeclaration()
		{
			List<AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo> list = new List<AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo>();
			list.Add(new AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Label, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.Token.String));
			list.Add(new AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.MemberInfo(AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Value, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.Token.Object));
			return new AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ValidValue, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, list);
		}

		void IPersistable.Serialize(AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ValidValue.m_Declaration);
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Label:
					writer.Write(this.m_label);
					break;
				case AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Value:
					writer.Write(this.m_value);
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		void IPersistable.Deserialize(AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ValidValue.m_Declaration);
			while (reader.NextMember())
			{
				switch (reader.CurrentMember.MemberName)
				{
				case AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Label:
					this.m_label = reader.ReadString();
					break;
				case AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.MemberName.Value:
					this.m_value = reader.ReadVariant();
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		void IPersistable.ResolveReferences(Dictionary<AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType IPersistable.GetObjectType()
		{
			return AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ValidValue;
		}
	}
}

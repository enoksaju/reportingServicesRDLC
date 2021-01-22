using AspNetCore.ReportingServices.OnDemandProcessing.Scalability;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using AspNetCore.ReportingServices.ReportProcessing;
using System;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	public class LookupTable : IStorable, IPersistable
	{
		private ScalableDictionary<object, LookupMatches> m_table;

		[NonSerialized]
		private static readonly Declaration m_Declaration = LookupTable.GetDeclaration();

		public int Size
		{
			get
			{
				return this.m_table.Size;
			}
		}

		public LookupTable()
		{
		}

		public LookupTable(IScalabilityCache scalabilityCache, IEqualityComparer<object> comparer, bool mustStoreDataRows)
		{
			this.m_table = new ScalableDictionary<object, LookupMatches>(0, scalabilityCache, 100, 10, comparer);
		}

		public bool TryGetValue(object key, out LookupMatches matches)
		{
			return this.m_table.TryGetValue(key, out matches);
		}

		public bool TryGetAndPinValue(object key, out LookupMatches matches, out IDisposable cleanupRef)
		{
			return this.m_table.TryGetAndPin(key, out matches, out cleanupRef);
		}

		public IDisposable AddAndPin(object key, LookupMatches matches)
		{
			return this.m_table.AddAndPin(key, matches);
		}

		public void TransferTo(IScalabilityCache scaleCache)
		{
			this.m_table.TransferTo(scaleCache);
		}

		public void SetEqualityComparer(IEqualityComparer<object> comparer)
		{
			this.m_table.UpdateComparer(comparer);
		}

		public void Serialize(IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(LookupTable.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName == MemberName.LookupTable)
				{
					writer.Write(this.m_table);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		public void Deserialize(IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(LookupTable.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName == MemberName.LookupTable)
				{
					this.m_table = reader.ReadRIFObject<ScalableDictionary<object, LookupMatches>>();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		public void ResolveReferences(Dictionary<AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
		}

		public AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupTable;
		}

		public static Declaration GetDeclaration()
		{
			if (LookupTable.m_Declaration == null)
			{
				List<MemberInfo> list = new List<MemberInfo>();
				list.Add(new MemberInfo(MemberName.LookupTable, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionary, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupMatches));
				return new Declaration(AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupTable, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, list);
			}
			return LookupTable.m_Declaration;
		}
	}
}

using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using System;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	public class DataAggregateInfoBucket : AggregateBucket<DataAggregateInfo>
	{
		[NonSerialized]
		private static readonly Declaration m_Declaration = DataAggregateInfoBucket.GetDeclaration();

		public DataAggregateInfoBucket()
		{
		}

		public DataAggregateInfoBucket(int level)
			: base(level)
		{
		}

		protected override Declaration GetSpecificDeclaration()
		{
			return DataAggregateInfoBucket.m_Declaration;
		}

		public static Declaration GetDeclaration()
		{
			List<MemberInfo> list = new List<MemberInfo>();
			list.Add(new MemberInfo(MemberName.Aggregates, ObjectType.RIFObjectList, ObjectType.DataAggregateInfo));
			list.Add(new MemberInfo(MemberName.Level, Token.Int32));
			return new Declaration(ObjectType.DataAggregateInfoBucket, ObjectType.None, list);
		}

		public override ObjectType GetObjectType()
		{
			return ObjectType.DataAggregateInfoBucket;
		}
	}
}

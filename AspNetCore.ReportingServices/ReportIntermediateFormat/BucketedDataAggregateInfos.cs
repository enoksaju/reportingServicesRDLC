using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using System;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	internal class BucketedDataAggregateInfos : BucketedAggregatesCollection<DataAggregateInfo>
	{
		[NonSerialized]
		private static readonly Declaration m_Declaration = BucketedDataAggregateInfos.GetDeclaration();

		protected override AggregateBucket<DataAggregateInfo> CreateBucket(int level)
		{
			return new DataAggregateInfoBucket(level);
		}

		protected override Declaration GetSpecificDeclaration()
		{
			return BucketedDataAggregateInfos.m_Declaration;
		}

		public override ObjectType GetObjectType()
		{
			return ObjectType.BucketedDataAggregateInfos;
		}

		internal static Declaration GetDeclaration()
		{
			List<MemberInfo> list = new List<MemberInfo>();
			list.Add(new MemberInfo(MemberName.Buckets, ObjectType.RIFObjectList, ObjectType.DataAggregateInfoBucket));
			return new Declaration(ObjectType.BucketedDataAggregateInfos, ObjectType.None, list);
		}
	}
}

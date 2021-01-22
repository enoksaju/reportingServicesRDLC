using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using System;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	public class BucketedDataAggregateObjs : BucketedAggregatesCollection<DataAggregateObj>
	{
		[NonSerialized]
		private static readonly Declaration m_Declaration = BucketedDataAggregateObjs.GetDeclaration();

		protected override AggregateBucket<DataAggregateObj> CreateBucket(int level)
		{
			return new DataAggregateObjBucket(level);
		}

		protected override Declaration GetSpecificDeclaration()
		{
			return BucketedDataAggregateObjs.m_Declaration;
		}

		public override ObjectType GetObjectType()
		{
			return ObjectType.BucketedDataAggregateObjs;
		}

		public static Declaration GetDeclaration()
		{
			List<MemberInfo> list = new List<MemberInfo>();
			list.Add(new MemberInfo(MemberName.Buckets, ObjectType.RIFObjectList, ObjectType.DataAggregateObjBucket));
			return new Declaration(ObjectType.BucketedDataAggregateObjs, ObjectType.None, list);
		}
	}
}

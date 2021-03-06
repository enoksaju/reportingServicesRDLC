using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using System;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	public class DataAggregateObjBucket : AggregateBucket<DataAggregateObj>
	{
		[NonSerialized]
		private static readonly Declaration m_Declaration = DataAggregateObjBucket.GetDeclaration();

		public DataAggregateObjBucket()
		{
		}

		public DataAggregateObjBucket(int level)
			: base(level)
		{
		}

		protected override Declaration GetSpecificDeclaration()
		{
			return DataAggregateObjBucket.m_Declaration;
		}

		public static Declaration GetDeclaration()
		{
			List<MemberInfo> list = new List<MemberInfo>();
			list.Add(new MemberInfo(MemberName.Aggregates, ObjectType.RIFObjectList, ObjectType.DataAggregateObj));
			list.Add(new MemberInfo(MemberName.Level, Token.Int32));
			return new Declaration(ObjectType.DataAggregateObjBucket, ObjectType.None, list);
		}

		public override ObjectType GetObjectType()
		{
			return ObjectType.DataAggregateObjBucket;
		}
	}
}

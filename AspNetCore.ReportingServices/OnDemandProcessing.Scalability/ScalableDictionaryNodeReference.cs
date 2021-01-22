using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public sealed class ScalableDictionaryNodeReference : Reference<ScalableDictionaryNode>, IScalableDictionaryEntry, IStorable, IPersistable
	{
		public ScalableDictionaryNodeReference()
		{
		}

		public override ObjectType GetObjectType()
		{
			return ObjectType.ScalableDictionaryNodeReference;
		}

		public ScalableDictionaryNode Value()
		{
			return ((IReference<ScalableDictionaryNode>)this).Value();
		}
	}
}

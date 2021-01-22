using AspNetCore.ReportingServices.OnDemandProcessing.Scalability;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;

namespace AspNetCore.ReportingServices.Rendering.SPBProcessing
{
	public class SPBReferenceCreator : IReferenceCreator
	{
		private static SPBReferenceCreator m_instance = new SPBReferenceCreator();

		public static SPBReferenceCreator Instance
		{
			get
			{
				return SPBReferenceCreator.m_instance;
			}
		}

		private SPBReferenceCreator()
		{
		}

		public bool TryCreateReference(IStorable refTarget, out BaseReference reference)
		{
			reference = null;
			return false;
		}

		public bool TryCreateReference(ObjectType referenceObjectType, out BaseReference reference)
		{
			reference = null;
			return false;
		}
	}
}

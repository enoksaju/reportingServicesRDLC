using AspNetCore.ReportingServices.OnDemandProcessing.Scalability;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;

namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer
{
	public class ExcelReferenceCreator : IReferenceCreator
	{
		private static ExcelReferenceCreator m_instance = new ExcelReferenceCreator();

		public static ExcelReferenceCreator Instance
		{
			get
			{
				return ExcelReferenceCreator.m_instance;
			}
		}

		private ExcelReferenceCreator()
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

using AspNetCore.ReportingServices.OnDemandProcessing.Scalability;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer.WordOpenXmlRenderer.Models
{
	public sealed class WordRendererReferenceCreator : IReferenceCreator
	{
		private static WordRendererReferenceCreator _instance = new WordRendererReferenceCreator();

		public static WordRendererReferenceCreator Instance
		{
			get
			{
				return WordRendererReferenceCreator._instance;
			}
		}

		private WordRendererReferenceCreator()
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

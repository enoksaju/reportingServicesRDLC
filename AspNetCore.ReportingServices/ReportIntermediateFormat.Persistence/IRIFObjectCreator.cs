namespace AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence
{
	public interface IRIFObjectCreator
	{
		IPersistable CreateRIFObject(ObjectType objectType, ref IntermediateFormatReader context);
	}
}

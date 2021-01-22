namespace AspNetCore.ReportingServices.RdlObjectModel.Serialization
{
	public interface IShouldSerialize
	{
		bool ShouldSerializeThis();

		SerializationMethod ShouldSerializeProperty(string name);
	}
}

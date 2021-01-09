namespace AspNetCore.ReportingServices.RdlObjectModel.Serialization
{
	internal interface IShouldSerialize
	{
		bool ShouldSerializeThis();

		SerializationMethod ShouldSerializeProperty(string name);
	}
}

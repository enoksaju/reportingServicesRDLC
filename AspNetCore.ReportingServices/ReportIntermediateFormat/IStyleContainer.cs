using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	public interface IStyleContainer
	{
		Style StyleClass
		{
			get;
		}

		IInstancePath InstancePath
		{
			get;
		}

		ObjectType ObjectType
		{
			get;
		}

		string Name
		{
			get;
		}
	}
}

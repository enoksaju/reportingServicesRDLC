using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	public interface IPageBreakOwner
	{
		PageBreak PageBreak
		{
			get;
			set;
		}

		ObjectType ObjectType
		{
			get;
		}

		string ObjectName
		{
			get;
		}

		IInstancePath InstancePath
		{
			get;
		}
	}
}

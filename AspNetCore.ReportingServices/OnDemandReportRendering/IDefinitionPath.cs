namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	internal interface IDefinitionPath
	{
		string DefinitionPath
		{
			get;
		}

		IDefinitionPath ParentDefinitionPath
		{
			get;
		}
	}
}

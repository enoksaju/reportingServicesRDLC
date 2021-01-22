using System;

namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.ExcelGenerator.OXML
{
	public class FatalException : Exception
	{
		public FatalException()
			: base(ExcelRenderRes.ArgumentInvalid)
		{
		}
	}
}

using AspNetCore.ReportingServices.Diagnostics.Utilities;
using System;
using System.Runtime.Serialization;

namespace AspNetCore.Reporting
{
	[Serializable]
	public sealed class DefinitionInvalidException : RSException
	{
		public DefinitionInvalidException(string reportPath, Exception inner)
			: base(ErrorCode.pvInvalidDefinition, ProcessingStrings.pvInvalidDefinition(reportPath), inner, null, null)
		{
		}

		private DefinitionInvalidException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

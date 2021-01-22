using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	[Serializable]
	public class ArgumentTooLargeException : ArgumentConstraintException
	{
		public ArgumentTooLargeException(object component, string property, object value, object maximum)
			: base(component, property, value, maximum, SRErrors.InvalidParamLessThan(property, maximum))
		{
		}

		protected ArgumentTooLargeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

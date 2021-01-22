using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ReportProcessingException_SpatialTypeComparisonError : Exception
	{
		private const string TypeSerializationID = "type";

		private string m_type;

		public string Type
		{
			get
			{
				return this.m_type;
			}
		}

		public ReportProcessingException_SpatialTypeComparisonError(string type)
		{
			this.m_type = type;
		}

		public ReportProcessingException_SpatialTypeComparisonError(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.m_type = info.GetString("type");
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("type", this.m_type);
		}
	}
}

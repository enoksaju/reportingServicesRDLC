using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	[Serializable]
	public class ChartNoDataMessage : ChartTitle
	{
		public ChartNoDataMessage()
		{
		}

		public ChartNoDataMessage(Chart chart)
			: base(chart)
		{
		}

		public override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ChartNoDataMessageStart();
			base.InitializeInternal(context);
			context.ExprHostBuilder.ChartNoDataMessageEnd();
		}

		public override ObjectType GetObjectType()
		{
			return AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartNoDataMessage;
		}
	}
}

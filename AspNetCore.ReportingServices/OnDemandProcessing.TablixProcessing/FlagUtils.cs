namespace AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing
{
	public static class FlagUtils
	{
		public static bool HasFlag(DataActions value, DataActions flagToTest)
		{
			return (value & flagToTest) != DataActions.None;
		}

		public static bool HasFlag(AggregateUpdateFlags value, AggregateUpdateFlags flagToTest)
		{
			return (value & flagToTest) != AggregateUpdateFlags.None;
		}
	}
}

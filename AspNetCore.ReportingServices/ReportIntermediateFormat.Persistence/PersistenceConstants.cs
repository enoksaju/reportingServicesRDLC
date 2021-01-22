using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence
{
	public class PersistenceConstants
	{
		public const int NullReferenceID = -2;

		public const int UndefinedCompatVersion = 0;

		public static readonly int MajorVersion = 12;

		public static readonly int MinorVersion = 3;

		public static readonly int CurrentCompatVersion = ReportProcessingCompatibilityVersion.CurrentVersion;
	}
}

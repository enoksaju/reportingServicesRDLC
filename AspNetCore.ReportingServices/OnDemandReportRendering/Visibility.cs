using AspNetCore.ReportingServices.ReportIntermediateFormat;
using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class Visibility
	{
		protected ReportBoolProperty m_startHidden;

		public abstract ReportBoolProperty Hidden
		{
			get;
		}

		public abstract string ToggleItem
		{
			get;
		}

		public abstract SharedHiddenState HiddenState
		{
			get;
		}

		public abstract bool RecursiveToggleReceiver
		{
			get;
		}

		public static ReportBoolProperty GetStartHidden(AspNetCore.ReportingServices.ReportProcessing.Visibility visibility)
		{
			ReportBoolProperty reportBoolProperty = null;
			if (visibility == null)
			{
				return new ReportBoolProperty();
			}
			return new ReportBoolProperty(visibility.Hidden);
		}

		public static ReportBoolProperty GetStartHidden(AspNetCore.ReportingServices.ReportIntermediateFormat.Visibility visibility)
		{
			ReportBoolProperty reportBoolProperty = null;
			if (visibility == null)
			{
				return new ReportBoolProperty();
			}
			return new ReportBoolProperty(visibility.Hidden);
		}

		public static SharedHiddenState GetHiddenState(AspNetCore.ReportingServices.ReportProcessing.Visibility visibility)
		{
			return (SharedHiddenState)AspNetCore.ReportingServices.ReportProcessing.Visibility.GetSharedHidden(visibility);
		}

		public static SharedHiddenState GetHiddenState(AspNetCore.ReportingServices.ReportIntermediateFormat.Visibility visibility)
		{
			return AspNetCore.ReportingServices.ReportIntermediateFormat.Visibility.GetSharedHidden(visibility);
		}
	}
}

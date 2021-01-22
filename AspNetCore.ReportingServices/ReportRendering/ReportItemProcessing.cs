using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.ReportRendering
{
	public sealed class ReportItemProcessing : MemberBase
	{
		public string DefinitionName;

		public string Label;

		public string Bookmark;

		public string Tooltip;

		public ReportSize Height;

		public ReportSize Width;

		public ReportSize Top;

		public ReportSize Left;

		public int ZIndex;

		public bool Hidden;

		public SharedHiddenState SharedHidden = SharedHiddenState.Never;

		public DataValueInstanceList SharedStyles;

		public DataValueInstanceList NonSharedStyles;

		public ReportItemProcessing()
			: base(true)
		{
		}

		public ReportItemProcessing DeepClone()
		{
			ReportItemProcessing reportItemProcessing = new ReportItemProcessing();
			if (this.DefinitionName != null)
			{
				reportItemProcessing.DefinitionName = string.Copy(this.DefinitionName);
			}
			if (this.Label != null)
			{
				reportItemProcessing.Label = string.Copy(this.Label);
			}
			if (this.Bookmark != null)
			{
				reportItemProcessing.Bookmark = string.Copy(this.Bookmark);
			}
			if (this.Tooltip != null)
			{
				reportItemProcessing.Tooltip = string.Copy(this.Tooltip);
			}
			if (this.Height != null)
			{
				reportItemProcessing.Height = this.Height.DeepClone();
			}
			if (this.Width != null)
			{
				reportItemProcessing.Width = this.Width.DeepClone();
			}
			if (this.Top != null)
			{
				reportItemProcessing.Top = this.Top.DeepClone();
			}
			if (this.Left != null)
			{
				reportItemProcessing.Left = this.Left.DeepClone();
			}
			reportItemProcessing.ZIndex = this.ZIndex;
			reportItemProcessing.Hidden = this.Hidden;
			reportItemProcessing.SharedHidden = this.SharedHidden;
			Global.Tracer.Assert(this.SharedStyles == null && null == this.NonSharedStyles);
			return reportItemProcessing;
		}
	}
}

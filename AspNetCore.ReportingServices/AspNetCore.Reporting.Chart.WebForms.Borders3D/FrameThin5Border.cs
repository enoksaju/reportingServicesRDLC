namespace AspNetCore.Reporting.Chart.WebForms.Borders3D
{
	public class FrameThin5Border : FrameThin1Border
	{
		public override string Name
		{
			get
			{
				return "FrameThin5";
			}
		}

		public FrameThin5Border()
		{
			base.drawScrews = true;
		}
	}
}

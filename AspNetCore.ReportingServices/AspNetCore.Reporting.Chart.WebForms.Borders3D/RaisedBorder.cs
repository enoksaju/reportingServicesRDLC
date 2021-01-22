namespace AspNetCore.Reporting.Chart.WebForms.Borders3D
{
	public class RaisedBorder : SunkenBorder
	{
		public override string Name
		{
			get
			{
				return "Raised";
			}
		}

		public RaisedBorder()
		{
			base.sunken = false;
		}
	}
}

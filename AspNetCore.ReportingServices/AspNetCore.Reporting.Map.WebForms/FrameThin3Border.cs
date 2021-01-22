namespace AspNetCore.Reporting.Map.WebForms
{
	public class FrameThin3Border : FrameThin1Border
	{
		public override string Name
		{
			get
			{
				return "FrameThin3";
			}
		}

		public FrameThin3Border()
		{
			base.innerCorners = (base.cornerRadius = new float[8]
			{
				1f,
				1f,
				1f,
				1f,
				1f,
				1f,
				1f,
				1f
			});
		}
	}
}

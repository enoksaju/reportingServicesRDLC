namespace AspNetCore.Reporting.Map.WebForms
{
	public class FrameThin2Border : FrameThin1Border
	{
		public override string Name
		{
			get
			{
				return "FrameThin2";
			}
		}

		public FrameThin2Border()
		{
			base.innerCorners = (base.cornerRadius = new float[8]
			{
				15f,
				15f,
				15f,
				1f,
				1f,
				1f,
				1f,
				15f
			});
		}
	}
}

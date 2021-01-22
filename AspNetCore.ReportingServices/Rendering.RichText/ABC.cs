namespace AspNetCore.ReportingServices.Rendering.RichText
{
	public struct ABC
	{
		public int abcA;

		public uint abcB;

		public int abcC;

		public int Width
		{
			get
			{
				return (int)(this.abcA + this.abcB + this.abcC);
			}
		}

		public void SetToZeroWidth()
		{
			this.abcA = 0;
			this.abcB = 0u;
			this.abcC = 0;
		}
	}
}

namespace AspNetCore.Reporting.Gauge.WebForms
{
	public struct HSV
	{
		public int Hue;

		public int Saturation;

		public int value;

		public HSV(int H, int S, int V)
		{
			this.Hue = H;
			this.Saturation = S;
			this.value = V;
		}
	}
}

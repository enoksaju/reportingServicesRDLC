using System;

namespace AspNetCore.Reporting.Map.WebForms
{
	public struct RGB
	{
		public int Red;

		public int Green;

		public int Blue;

		public RGB(int R, int G, int B)
		{
			this.Red = Math.Max(Math.Min(R, 255), 0);
			this.Green = Math.Max(Math.Min(G, 255), 0);
			this.Blue = Math.Max(Math.Min(B, 255), 0);
		}
	}
}

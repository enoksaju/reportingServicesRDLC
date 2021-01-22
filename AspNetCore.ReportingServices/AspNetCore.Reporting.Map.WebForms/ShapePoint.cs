using System.IO;

namespace AspNetCore.Reporting.Map.WebForms
{
	public class ShapePoint
	{
		public double X;

		public double Y;

		public void Read(BinaryReader reader)
		{
			this.X = reader.ReadDouble();
			this.Y = reader.ReadDouble();
		}
	}
}

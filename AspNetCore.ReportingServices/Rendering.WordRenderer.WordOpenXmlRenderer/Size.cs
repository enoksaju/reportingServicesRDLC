using System.Globalization;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer.WordOpenXmlRenderer
{
	public struct Size
	{
		public enum Strategy
		{
			AutoSize,
			Fit,
			FitProportional,
			Clip
		}

		public int Width
		{
			get;
			set;
		}

		public int Height
		{
			get;
			set;
		}

		public static bool operator ==(Size a, Size b)
		{
			if (a.Height == b.Height)
			{
				return a.Width == b.Width;
			}
			return false;
		}

		public static bool operator !=(Size a, Size b)
		{
			if (a.Height == b.Height)
			{
				return a.Width != b.Width;
			}
			return true;
		}

		public override bool Equals(object obj)
		{
			if (obj is Size)
			{
				return (Size)obj == this;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return this.Height.GetHashCode() ^ this.Width.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} wide by {1} tall.", this.Width, this.Height);
		}
	}
}

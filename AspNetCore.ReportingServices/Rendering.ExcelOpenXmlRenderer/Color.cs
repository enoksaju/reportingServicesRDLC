using AspNetCore.ReportingServices.Rendering.ExcelOpenXmlRenderer.Model;
using AspNetCore.ReportingServices.Rendering.ExcelRenderer.Excel;

namespace AspNetCore.ReportingServices.Rendering.ExcelOpenXmlRenderer
{
	public sealed class Color : IColor
	{
		private readonly ColorModel mModel;

		public byte Blue
		{
			get
			{
				return (byte)this.mModel.getBlue();
			}
		}

		public byte Green
		{
			get
			{
				return (byte)this.mModel.getGreen();
			}
		}

		public ColorModel Model
		{
			get
			{
				return this.mModel;
			}
		}

		public byte Red
		{
			get
			{
				return (byte)this.mModel.getRed();
			}
		}

		public Color(ColorModel model)
		{
			this.mModel = model;
		}

		public override bool Equals(object obj)
		{
			if (obj != null && obj is Color)
			{
				if (obj == this)
				{
					return true;
				}
				Color color = (Color)obj;
				return color.mModel.Equals(this.mModel);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return this.mModel.GetHashCode();
		}
	}
}

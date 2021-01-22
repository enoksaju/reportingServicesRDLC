using AspNetCore.ReportingServices.Rendering.RPLProcessing;

namespace AspNetCore.ReportingServices.Rendering.ImageRenderer
{
	public sealed class PDFImage
	{
		public int ImageId = -1;

		public byte[] ImageData;

		public GDIImageProps GdiProperties;

		private bool m_isMonochromeJpeg;

		public bool IsMonochromeJpeg
		{
			get
			{
				if (this.ImageData == null)
				{
					return this.m_isMonochromeJpeg;
				}
				this.m_isMonochromeJpeg = false;
				int num = 0;
				int num2 = 2;
				if (this.ImageData[0] == 255 && this.ImageData[1] == 216)
				{
					do
					{
						byte b = this.ImageData[num2 + 1];
						num2 += 2;
						if (b != 1 && (b < 208 || b > 217))
						{
							if (b == 222 || (b >= 192 && b <= 195) || (b >= 197 && b <= 203) || (b >= 205 && b <= 207))
							{
								num2 += 7;
								if (this.ImageData[num2] == 1)
								{
									this.m_isMonochromeJpeg = true;
								}
								break;
							}
							num = this.ImageData[num2] << 8;
							num += this.ImageData[num2 + 1];
							num2 += num;
						}
					}
					while (num2 < this.ImageData.Length);
					return this.m_isMonochromeJpeg;
				}
				return this.m_isMonochromeJpeg;
			}
		}
	}
}

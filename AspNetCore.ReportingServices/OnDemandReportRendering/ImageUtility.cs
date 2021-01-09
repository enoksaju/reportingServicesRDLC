using AspNetCore.ReportingServices.Common;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	internal static class ImageUtility
	{
		private static ImageFormat GetTargetImageFormat(ImageFormat imageFormat)
		{
			ImageFormat result = ImageFormat.Png;
			if (imageFormat.Guid == ImageFormat.Jpeg.Guid)
			{
				result = ImageFormat.Jpeg;
			}
			return result;
		}

		private static bool IsSupportedBySilverlight(ImageFormat imageFormat)
		{
			if (!(imageFormat.Guid == ImageFormat.Jpeg.Guid))
			{
				return imageFormat.Guid == ImageFormat.Png.Guid;
			}
			return true;
		}

		private static double ConvertToPixels(RVUnit unit)
		{
			double num = unit.ToMillimeters();
			return num * 96.0 / 25.4;
		}

		internal static byte[] ScaleImage(byte[] sourceImageBytes, RVUnit frameWidth, RVUnit frameHeight)
		{
			ImageFormat imageFormat = default(ImageFormat);
			return ImageUtility.ScaleImage(sourceImageBytes, (int)ImageUtility.ConvertToPixels(frameWidth), (int)ImageUtility.ConvertToPixels(frameHeight), out imageFormat);
		}

		internal static byte[] ScaleImage(byte[] sourceImageBytes, int frameWidth, int frameHeight, out ImageFormat imageFormat)
		{
			System.Drawing.Image image;
			try
			{
				image = System.Drawing.Image.FromStream(new MemoryStream(sourceImageBytes), true, false);
				imageFormat = ImageUtility.GetTargetImageFormat(image.RawFormat);
			}
			catch
			{
				imageFormat = null;
				return null;
			}
			int num = Math.Max(image.Width / frameWidth, image.Height / frameHeight);
			System.Drawing.Image image2;
			if (num > 1)
			{
				int width = image.Width / num;
				int height = image.Height / num;
				image2 = new Bitmap(width, height);
				using (Graphics graphics = Graphics.FromImage(image2))
				{
					graphics.DrawImage(image, 0, 0, width, height);
				}
				image.Dispose();
			}
			else
			{
				if (ImageUtility.IsSupportedBySilverlight(image.RawFormat))
				{
					return sourceImageBytes;
				}
				image2 = image;
			}
			MemoryStream memoryStream = new MemoryStream();
			image2.Save(memoryStream, imageFormat);
			image2.Dispose();
			return memoryStream.GetBuffer();
		}
	}
}

namespace AspNetCore.ReportingServices.ReportRendering
{
	public sealed class ImageProcessing : ImageBase
	{
		public byte[] m_imageData;

		public string m_mimeType;

		public Image.Sizings m_sizing;

		public ImageProcessing DeepClone()
		{
			ImageProcessing imageProcessing = new ImageProcessing();
			if (this.m_imageData != null)
			{
				imageProcessing.m_imageData = new byte[this.m_imageData.Length];
				this.m_imageData.CopyTo(imageProcessing.m_imageData, 0);
			}
			if (this.m_mimeType != null)
			{
				imageProcessing.m_mimeType = string.Copy(this.m_mimeType);
			}
			imageProcessing.m_sizing = this.m_sizing;
			return imageProcessing;
		}
	}
}

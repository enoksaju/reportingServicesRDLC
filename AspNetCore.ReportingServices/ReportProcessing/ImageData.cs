using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ImageData
	{
		private byte[] m_data;

		private string m_MIMEType;

		public string MIMEType
		{
			get
			{
				return this.m_MIMEType;
			}
		}

		public byte[] Data
		{
			get
			{
				return this.m_data;
			}
		}

		public ImageData(byte[] data, string mimeType)
		{
			this.m_data = data;
			this.m_MIMEType = mimeType;
		}
	}
}

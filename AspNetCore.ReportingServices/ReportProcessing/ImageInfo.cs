using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ImageInfo
	{
		private string m_streamName;

		private string m_mimeType;

		[NonSerialized]
		private WeakReference m_imageDataRef;

		public string StreamName
		{
			get
			{
				return this.m_streamName;
			}
			set
			{
				this.m_streamName = value;
			}
		}

		public string MimeType
		{
			get
			{
				return this.m_mimeType;
			}
			set
			{
				this.m_mimeType = value;
			}
		}

		public WeakReference ImageDataRef
		{
			get
			{
				return this.m_imageDataRef;
			}
			set
			{
				this.m_imageDataRef = value;
			}
		}

		public ImageInfo()
		{
		}

		public ImageInfo(string streamName, string mimeType)
		{
			this.m_streamName = streamName;
			this.m_mimeType = mimeType;
		}

		public static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.StreamName, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.MIMEType, Token.String));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.None, memberInfoList);
		}
	}
}

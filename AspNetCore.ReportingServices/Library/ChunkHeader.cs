using System;

namespace AspNetCore.ReportingServices.Library
{
	[Serializable]
	public sealed class ChunkHeader
	{
		public static readonly short MissingVersion = 1;

		public static readonly short CurrentVersion = 2;

		private ChunkFlags m_chunkFlags;

		private int m_chunkType;

		private string m_chunkName;

		private string m_mimeType;

		private long m_chunkSize;

		private short m_version;

		public string MimeType
		{
			get
			{
				return this.m_mimeType;
			}
		}

		public string ChunkName
		{
			get
			{
				return this.m_chunkName;
			}
			set
			{
				this.m_chunkName = value;
			}
		}

		public int ChunkType
		{
			get
			{
				return this.m_chunkType;
			}
		}

		public ChunkFlags ChunkFlag
		{
			get
			{
				return this.m_chunkFlags;
			}
		}

		public long ChunkSize
		{
			get
			{
				return this.m_chunkSize;
			}
		}

		public short Version
		{
			get
			{
				return this.m_version;
			}
		}

		public ChunkHeader(string chunkName, int chunkType, ChunkFlags chunkFlag, string mimeType, short version, long chunkSize)
		{
			this.m_chunkName = chunkName;
			this.m_chunkType = chunkType;
			this.m_chunkFlags = chunkFlag;
			this.m_mimeType = mimeType;
			this.m_version = version;
			this.m_chunkSize = chunkSize;
		}

		public ChunkHeader(ChunkHeader baseHeader)
			: this(baseHeader.ChunkName, baseHeader.ChunkType, baseHeader.ChunkFlag, baseHeader.MimeType, baseHeader.Version, baseHeader.ChunkSize)
		{
		}
	}
}

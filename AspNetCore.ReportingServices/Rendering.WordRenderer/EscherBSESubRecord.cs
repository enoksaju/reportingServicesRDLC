using System.IO;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer
{
	public class EscherBSESubRecord : EscherRecord
	{
		public const int MD4HASH_LENGTH = 16;

		private byte[] mHash;

		private byte mBoundary = 255;

		private byte[] mImage;

		public override int RecordSize
		{
			get
			{
				return 8 + this.mHash.Length + ((this.mImage != null) ? (1 + this.mImage.Length) : 0);
			}
		}

		public override string RecordName
		{
			get
			{
				return "BSESub";
			}
		}

		public virtual byte[] Hash
		{
			get
			{
				return this.mHash;
			}
			set
			{
				this.mHash = value;
			}
		}

		public virtual byte[] Image
		{
			get
			{
				return this.mImage;
			}
			set
			{
				this.mImage = value;
			}
		}

		public EscherBSESubRecord()
		{
		}

		public override int Serialize(BinaryWriter dataWriter)
		{
			dataWriter.Write(this.getOptions());
			dataWriter.Write(this.GetRecordId());
			dataWriter.Write(this.RecordSize - 8);
			dataWriter.Write(this.mHash);
			dataWriter.Write(this.mBoundary);
			dataWriter.Write(this.mImage);
			return this.RecordSize;
		}
	}
}

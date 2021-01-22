namespace AspNetCore.ReportingServices.Rendering.WordRenderer
{
	public class EscherPropertyMetaData
	{
		public static byte TYPE_UNKNOWN = 0;

		public static byte TYPE_BOOLEAN = 1;

		public static byte TYPE_RGB = 2;

		public static byte TYPE_SHAPEPATH = 3;

		public static byte TYPE_SIMPLE = 4;

		public static byte TYPE_ARRAY = 5;

		private string description;

		private byte type;

		public virtual string Description
		{
			get
			{
				return this.description;
			}
		}

		public virtual byte Type
		{
			get
			{
				return this.type;
			}
		}

		public EscherPropertyMetaData(string description)
		{
			this.description = description;
		}

		public EscherPropertyMetaData(string description, byte type)
		{
			this.description = description;
			this.type = type;
		}
	}
}

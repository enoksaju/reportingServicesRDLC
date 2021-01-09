using System.Runtime.Serialization;

namespace AspNetCore.Reporting.Map.WebForms.BingMaps
{
	[DataContract(Namespace = "http://schemas.microsoft.com/search/local/ws/rest/v1")]
	internal class Pixel
	{
		[DataMember(Name = "x", EmitDefaultValue = false)]
		public string X
		{
			get;
			set;
		}

		[DataMember(Name = "y", EmitDefaultValue = false)]
		public string Y
		{
			get;
			set;
		}
	}
}

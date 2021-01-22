using System.Runtime.Serialization;

namespace AspNetCore.Reporting.Map.WebForms.BingMaps
{
	[DataContract]
	public class Point : Shape
	{
		[DataMember(Name = "type", EmitDefaultValue = false)]
		public string Type
		{
			get;
			set;
		}

		[DataMember(Name = "coordinates", EmitDefaultValue = false)]
		public double[] Coordinates
		{
			get;
			set;
		}

		[DataMember(Name = "calculationMethod", EmitDefaultValue = false)]
		public string CalculationMethod
		{
			get;
			set;
		}

		[DataMember(Name = "usageTypes", EmitDefaultValue = false)]
		public string[] UsageTypes
		{
			get;
			set;
		}

		public Coordinate GetCoordinate()
		{
			if (this.Coordinates.Length >= 2)
			{
				return new Coordinate(this.Coordinates[0], this.Coordinates[1]);
			}
			return null;
		}
	}
}

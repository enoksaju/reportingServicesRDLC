namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public interface IPropertyDefinition
	{
		string Name
		{
			get;
		}

		object Default
		{
			get;
		}

		object Maximum
		{
			get;
		}

		object Minimum
		{
			get;
		}

		void Validate(object component, object value);
	}
}

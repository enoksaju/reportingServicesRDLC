namespace AspNetCore.Reporting.Map.WebForms
{
	public interface ILayerElement
	{
		string Layer
		{
			get;
			set;
		}

		bool BelongsToLayer
		{
			get;
		}

		bool BelongsToAllLayers
		{
			get;
		}

		Layer LayerObject
		{
			get;
			set;
		}
	}
}

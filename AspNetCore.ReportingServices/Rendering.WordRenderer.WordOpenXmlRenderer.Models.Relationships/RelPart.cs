namespace AspNetCore.ReportingServices.Rendering.WordRenderer.WordOpenXmlRenderer.Models.Relationships
{
	public abstract class RelPart
	{
		private string _location;

		private string _contentType;

		public string Location
		{
			get
			{
				return this._location;
			}
			set
			{
				this._location = value;
			}
		}

		public string ContentType
		{
			get
			{
				return this._contentType;
			}
			set
			{
				this._contentType = value;
			}
		}
	}
}

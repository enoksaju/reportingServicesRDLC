namespace AspNetCore.ReportingServices.Rendering.WordRenderer
{
	public class EscherBoolProperty : EscherSimpleProperty
	{
		public virtual bool True
		{
			get
			{
				return base.m_propertyValue != 0;
			}
		}

		public virtual bool False
		{
			get
			{
				return base.m_propertyValue == 0;
			}
		}

		public EscherBoolProperty(ushort propertyNumber, int value_Renamed)
			: base(propertyNumber, false, false, value_Renamed)
		{
		}
	}
}

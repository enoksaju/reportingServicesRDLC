namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class KnobConverter : CollectionItemTypeConverter
	{
		public KnobConverter()
		{
			base.simpleType = typeof(Knob);
		}
	}
}

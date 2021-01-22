namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class StateIndicatorConverter : CollectionItemTypeConverter
	{
		public StateIndicatorConverter()
		{
			base.simpleType = typeof(StateIndicator);
		}
	}
}

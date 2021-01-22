using System.ComponentModel;

namespace AspNetCore.Reporting.Chart.WebForms.Design
{
	public class LegendConverter : NoNameExpandableObjectConverter
	{
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			if (context != null && context.Instance != null && context.Instance is Chart)
			{
				Chart.controlCurrentContext = context;
			}
			return base.GetPropertiesSupported(context);
		}
	}
}

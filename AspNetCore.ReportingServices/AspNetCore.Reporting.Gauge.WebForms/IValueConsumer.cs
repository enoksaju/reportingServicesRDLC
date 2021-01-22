namespace AspNetCore.Reporting.Gauge.WebForms
{
	public interface IValueConsumer
	{
		void ProviderRemoved(IValueProvider provider);

		void ProviderNameChanged(IValueProvider provider);

		void InputValueChanged(object sender, ValueChangedEventArgs e);

		void Reset();

		void Refresh();

		IValueProvider GetProvider();
	}
}

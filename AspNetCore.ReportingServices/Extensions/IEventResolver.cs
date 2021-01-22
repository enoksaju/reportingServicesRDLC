namespace AspNetCore.ReportingServices.Extensions
{
	public interface IEventResolver
	{
		IEventHandler ResolveEvent(string eventType);

		void ItemPlacedInEventQueue();
	}
}

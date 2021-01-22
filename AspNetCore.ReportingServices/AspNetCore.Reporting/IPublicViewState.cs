namespace AspNetCore.Reporting
{
	public interface IPublicViewState
	{
		void LoadViewState(object viewState);

		object SaveViewState();
	}
}

namespace AspNetCore.ReportingServices.Common
{
	public static class ThreadingUtil
	{
		public static T ReturnOnDemandValue<T>(ref T valueStorage, object valueLock, CreatorGetter<T> getValue)
		{
			if (valueStorage != null)
			{
				return valueStorage;
			}
			lock (valueLock)
			{
				if (valueStorage == null)
				{
					valueStorage = getValue();
				}
				return valueStorage;
			}
		}
	}
}

using System;
using System.Collections;

namespace AspNetCore.ReportingServices.Interfaces
{
	[Serializable]
	public sealed class FolderOperationsCollection : CollectionBase
	{
		public FolderOperation this[int index]
		{
			get
			{
				return (FolderOperation)base.InnerList[index];
			}
		}

		public int Add(FolderOperation operation)
		{
			return base.InnerList.Add(operation);
		}
	}
}

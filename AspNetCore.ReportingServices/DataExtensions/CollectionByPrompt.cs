using System;
using System.Collections;

namespace AspNetCore.ReportingServices.DataExtensions
{
	[Serializable]
	public sealed class CollectionByPrompt : ArrayList
	{
		public new PromptBucket this[int index]
		{
			get
			{
				return (PromptBucket)base[index];
			}
		}

		public bool NeedPrompt
		{
			get
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (this[i].NeedPrompt)
					{
						return true;
					}
				}
				return false;
			}
		}

		public CollectionByPrompt()
		{
		}

		public new CollectionByPrompt Clone()
		{
			CollectionByPrompt collectionByPrompt = new CollectionByPrompt();
			for (int i = 0; i < this.Count; i++)
			{
				collectionByPrompt.Add(this[i].Clone());
			}
			return collectionByPrompt;
		}

		public void CheckedAdd(DataSourceInfo dataSource)
		{
			if (dataSource.CredentialsRetrieval == DataSourceInfo.CredentialsRetrievalOption.Prompt)
			{
				PromptBucket bucketByLinkID = this.GetBucketByLinkID(dataSource.LinkID);
				PromptBucket bucketByOriginalName = this.GetBucketByOriginalName(dataSource.PromptIdentifier);
				if (bucketByLinkID == null)
				{
					if (bucketByOriginalName == null)
					{
						PromptBucket promptBucket = new PromptBucket();
						promptBucket.Add(dataSource);
						this.Add(promptBucket);
					}
					else
					{
						bucketByOriginalName.Add(dataSource);
					}
				}
				else if (bucketByOriginalName == null)
				{
					bucketByLinkID.Add(dataSource);
				}
				else if (bucketByLinkID == bucketByOriginalName)
				{
					bucketByLinkID.Add(dataSource);
				}
				else
				{
					bucketByLinkID.AddRange(bucketByOriginalName);
					this.Remove(bucketByOriginalName);
					bucketByLinkID.Add(dataSource);
				}
			}
		}

		private PromptBucket GetBucketByLinkID(Guid linkID)
		{
			if (linkID == Guid.Empty)
			{
				return null;
			}
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i].HasItemWithLinkID(linkID))
				{
					return this[i];
				}
			}
			return null;
		}

		public PromptBucket GetBucketByOriginalName(string originalName)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i].HasItemWithOriginalName(originalName))
				{
					return this[i];
				}
			}
			return null;
		}

		public DataSourcePromptCollection GetPromptRepresentatives(ServerDataSourceSettings serverDatasourceSettings)
		{
			DataSourcePromptCollection dataSourcePromptCollection = new DataSourcePromptCollection();
			for (int i = 0; i < this.Count; i++)
			{
				dataSourcePromptCollection.Add(this[i].GetRepresentative(), serverDatasourceSettings);
			}
			return dataSourcePromptCollection;
		}
	}
}

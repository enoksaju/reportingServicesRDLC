using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Internal
{
	[XmlRoot(IsNullable = false)]
	internal sealed class AdditionalInfo
	{
		private readonly object m_dataExtensionSync = new object();

		public string RdcePreparationTime
		{
			get;
			set;
		}

		public string RdceInvocationTime
		{
			get;
			set;
		}

		public string RdceSnapshotGenerationTime
		{
			get;
			set;
		}

		public string SharedDataSet
		{
			get;
			set;
		}

		public string ProcessingEngine
		{
			get;
			set;
		}

		public long? TimeQueryTranslation
		{
			get;
			set;
		}

		[XmlIgnore]
		public bool TimeQueryTranslationSpecified
		{
			get
			{
				return this.TimeQueryTranslation.HasValue;
			}
		}

		[XmlIgnore]
		public bool? HasCloudToOnPremiseConnection
		{
			get;
			set;
		}

		public ScaleTimeCategory ScalabilityTime
		{
			get;
			set;
		}

		public EstimatedMemoryUsageKBCategory EstimatedMemoryUsageKB
		{
			get;
			set;
		}

		public ExternalImageCategory ExternalImages
		{
			get;
			set;
		}

		public SerializableDictionary DataExtension
		{
			get;
			set;
		}

		public List<DataShape> DataShapes
		{
			get;
			set;
		}

		public List<Connection> Connections
		{
			get;
			set;
		}

		public string SourceReportUri
		{
			get;
			set;
		}

		public string SortItem
		{
			get;
			set;
		}

		public string Direction
		{
			get;
			set;
		}

		public string ClearExistingSort
		{
			get;
			set;
		}

		public string DrillthroughId
		{
			get;
			set;
		}

		public string ToggleId
		{
			get;
			set;
		}

		public string DocumentMapId
		{
			get;
			set;
		}

		public string BookmarkId
		{
			get;
			set;
		}

		public string StartPage
		{
			get;
			set;
		}

		public string EndPage
		{
			get;
			set;
		}

		public string FindValue
		{
			get;
			set;
		}

		internal AdditionalInfo()
		{
		}

		internal void IncrementDataExtensionOperationCounter(string operation)
		{
			if (!string.IsNullOrEmpty(operation))
			{
				lock (this.m_dataExtensionSync)
				{
					if (this.DataExtension == null)
					{
						this.DataExtension = new SerializableDictionary(StringComparer.OrdinalIgnoreCase);
					}
					if (this.DataExtension.ContainsKey(operation))
					{
						Dictionary<string, int> dataExtension;
						string key;
						(dataExtension = this.DataExtension)[key = operation] = dataExtension[key] + 1;
					}
					else
					{
						this.DataExtension.Add(operation, 1);
					}
				}
			}
		}
	}
}

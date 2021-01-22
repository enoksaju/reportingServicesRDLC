using AspNetCore.ReportingServices.OnDemandProcessing.Scalability;
using System;

namespace AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing
{
	public sealed class CommonRowCache : IDisposable
	{
		public const int UnInitializedRowIndex = -1;

		private ScalableList<DataFieldRow> m_rows;

		public int Count
		{
			get
			{
				return this.m_rows.Count;
			}
		}

		public int LastRowIndex
		{
			get
			{
				return this.Count - 1;
			}
		}

		public CommonRowCache(IScalabilityCache scaleCache)
		{
			this.m_rows = new ScalableList<DataFieldRow>(0, scaleCache, 1000, 100);
		}

		public int AddRow(DataFieldRow row)
		{
			int count = this.m_rows.Count;
			this.m_rows.Add(row);
			return count;
		}

		public DataFieldRow GetRow(int index)
		{
			return this.m_rows[index];
		}

		public void SetupRow(int index, OnDemandProcessingContext odpContext)
		{
			DataFieldRow row = this.GetRow(index);
			row.SetFields(odpContext.ReportObjectModel.FieldsImpl);
		}

		public void Dispose()
		{
			this.m_rows.Dispose();
			this.m_rows = null;
		}
	}
}

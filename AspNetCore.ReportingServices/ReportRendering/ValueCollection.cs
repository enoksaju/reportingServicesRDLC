using AspNetCore.ReportingServices.ReportProcessing;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportRendering
{
	public sealed class ValueCollection
	{
		private ArrayList m_values;

		public object this[int index]
		{
			get
			{
				if (index >= 0 && index < this.Count)
				{
					return this.m_values[index];
				}
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, index, 0, this.Count);
			}
		}

		public int Count
		{
			get
			{
				return this.m_values.Count;
			}
		}

		public ValueCollection()
		{
			this.m_values = new ArrayList();
		}

		public ValueCollection(int capacity)
		{
			this.m_values = new ArrayList(capacity);
		}

		public ValueCollection(ArrayList values)
		{
			this.m_values = values;
		}

		public void Add(object value)
		{
			this.m_values.Add(value);
		}
	}
}

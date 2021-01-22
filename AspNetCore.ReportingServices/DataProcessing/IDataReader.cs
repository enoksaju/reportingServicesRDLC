using System;

namespace AspNetCore.ReportingServices.DataProcessing
{
	public interface IDataReader : IDisposable
	{
		int FieldCount
		{
			get;
		}

		string GetName(int fieldIndex);

		int GetOrdinal(string fieldName);

		bool Read();

		Type GetFieldType(int fieldIndex);

		object GetValue(int fieldIndex);
	}
}

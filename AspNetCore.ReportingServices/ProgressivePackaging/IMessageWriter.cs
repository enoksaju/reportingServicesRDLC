using System;
using System.IO;

namespace AspNetCore.ReportingServices.ProgressivePackaging
{
	public interface IMessageWriter : IDisposable
	{
		void WriteMessage(string name, object value);

		Stream CreateWritableStream(string name);
	}
}

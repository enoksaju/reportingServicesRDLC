using System.Collections.Generic;

namespace AspNetCore.ReportingServices.Rendering.HPBProcessing
{
	public interface StyleWriter
	{
		void Write(byte rplId, string value);

		void Write(byte rplId, byte value);

		void Write(byte rplId, int value);

		void WriteAll(Dictionary<byte, object> styleList);
	}
}

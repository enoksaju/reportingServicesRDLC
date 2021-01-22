using System.IO;
using System.Text;

namespace AspNetCore.ReportingServices.Interfaces
{
	public delegate Stream CreateAndRegisterStream(string name, string extension, Encoding encoding, string mimeType, bool willSeek, StreamOper operation);
}

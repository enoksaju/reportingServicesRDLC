using System.Data;
using System.Xml;

namespace AspNetCore.ReportingServices.Interfaces
{
	public interface ISemanticModelGenerator : IExtension
	{
		void Generate(IDbConnection connection, XmlWriter newModelWriter);

		void ReGenerateModel(IDbConnection connection, XmlReader currentModelReader, XmlWriter newModelWriter);
	}
}

using System.Collections.Generic;

namespace AspNetCore.ReportingServices.Rendering.RichText
{
	public class TextRunItemizedData
	{
		public List<int> SplitIndexes;

		public List<TexRunShapeData> GlyphData;

		public TextRunItemizedData(List<int> splitIndexes, List<TexRunShapeData> textRunsShapeData)
		{
			this.SplitIndexes = splitIndexes;
			this.GlyphData = textRunsShapeData;
		}
	}
}

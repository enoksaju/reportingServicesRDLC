namespace AspNetCore.ReportingServices.Rendering.RichText
{
	public sealed class ScriptAnalysis
	{
		public int eScript;

		public int fRTL;

		public int fLayoutRTL;

		public int fLinkBefore;

		public int fLinkAfter;

		public int fLogicalOrder;

		public int fNoGlyphIndex;

		public ScriptState s;

		public ScriptAnalysis(ushort word1)
		{
			this.eScript = (word1 & 0x3FF);
			this.fRTL = (word1 >> 10 & 1);
			this.fLayoutRTL = (word1 >> 11 & 1);
			this.fLinkBefore = (word1 >> 12 & 1);
			this.fLinkAfter = (word1 >> 13 & 1);
			this.fLogicalOrder = (word1 >> 14 & 1);
			this.fNoGlyphIndex = (word1 >> 15 & 1);
		}

		public SCRIPT_ANALYSIS GetAs_SCRIPT_ANALYSIS()
		{
			SCRIPT_ANALYSIS result = default(SCRIPT_ANALYSIS);
			result.word1 = (ushort)((this.eScript & 0x3FF) | (this.fRTL & 1) << 10 | (this.fLayoutRTL & 1) << 11 | (this.fLinkBefore & 1) << 12 | (this.fLinkAfter & 1) << 13 | (this.fLogicalOrder & 1) << 14 | (this.fNoGlyphIndex & 1) << 15);
			result.state = this.s.GetAs_SCRIPT_STATE();
			return result;
		}
	}
}

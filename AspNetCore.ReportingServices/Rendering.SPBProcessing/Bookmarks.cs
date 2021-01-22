using AspNetCore.ReportingServices.OnDemandReportRendering;
using System.IO;

namespace AspNetCore.ReportingServices.Rendering.SPBProcessing
{
	public sealed class Bookmarks : InteractivityChunks
	{
		public Bookmarks(Stream stream, int page)
			: base(stream, page)
		{
		}

		public void WriteBookmark(ReportItemInstance itemInstance)
		{
			if (itemInstance != null && itemInstance.Bookmark != null)
			{
				base.m_writer.Write((byte)0);
				base.m_writer.Write(itemInstance.Bookmark);
				base.m_writer.Write(itemInstance.UniqueName);
				base.m_writer.Write(base.m_page);
				base.m_writer.Write((byte)4);
			}
		}
	}
}

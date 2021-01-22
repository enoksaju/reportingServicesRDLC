using System.Collections.Generic;

namespace AspNetCore.ReportingServices.Rendering.RPLProcessing
{
	public sealed class RPLTablixOmittedRow : RPLTablixRow
	{
		private List<RPLTablixMemberCell> m_omittedHeaders;

		public override int BodyStart
		{
			get
			{
				return -1;
			}
		}

		public override List<RPLTablixMemberCell> OmittedHeaders
		{
			get
			{
				return this.m_omittedHeaders;
			}
		}

		public RPLTablixOmittedRow()
		{
		}

		public RPLTablixOmittedRow(List<RPLTablixMemberCell> omittedHeaders)
			: base(null)
		{
			this.m_omittedHeaders = omittedHeaders;
		}

		public override void AddOmittedHeader(RPLTablixMemberCell cell)
		{
			if (this.m_omittedHeaders == null)
			{
				this.m_omittedHeaders = new List<RPLTablixMemberCell>();
			}
			this.m_omittedHeaders.Add(cell);
		}
	}
}

using AspNetCore.ReportingServices.Rendering.RPLProcessing;
using System.IO;

namespace AspNetCore.ReportingServices.Rendering.SPBProcessing
{
	public sealed class RPLWriter
	{
		private RPLReport m_rplReport;

		private BinaryWriter m_binaryWriter;

		private RPLTablixRow m_tablixRow;

		public RPLReport Report
		{
			get
			{
				return this.m_rplReport;
			}
			set
			{
				this.m_rplReport = value;
			}
		}

		public RPLTablixRow TablixRow
		{
			get
			{
				return this.m_tablixRow;
			}
			set
			{
				this.m_tablixRow = value;
			}
		}

		public BinaryWriter BinaryWriter
		{
			get
			{
				return this.m_binaryWriter;
			}
			set
			{
				this.m_binaryWriter = value;
			}
		}

		public RPLWriter()
		{
		}

		public RPLWriter(BinaryWriter binaryWriter, RPLReport report, RPLTablixRow tablixRow)
		{
			this.BinaryWriter = binaryWriter;
			this.Report = report;
			this.TablixRow = tablixRow;
		}
	}
}

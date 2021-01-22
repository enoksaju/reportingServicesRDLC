using System;

namespace AspNetCore.Reporting
{
	[Serializable]
	public sealed class ReportDataSourceInfo
	{
		private string m_name;

		private string m_prompt;

		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		public string Prompt
		{
			get
			{
				return this.m_prompt;
			}
		}

		public ReportDataSourceInfo(string name, string prompt)
		{
			this.m_name = name;
			this.m_prompt = prompt;
		}
	}
}

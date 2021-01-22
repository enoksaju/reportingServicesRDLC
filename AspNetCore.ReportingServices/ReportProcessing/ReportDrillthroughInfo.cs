using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ReportDrillthroughInfo
	{
		private TokensHashtable m_rewrittenCommands;

		private DrillthroughHashtable m_drillthroughHashtable;

		public DrillthroughHashtable DrillthroughInformation
		{
			get
			{
				return this.m_drillthroughHashtable;
			}
			set
			{
				this.m_drillthroughHashtable = value;
			}
		}

		public TokensHashtable RewrittenCommands
		{
			get
			{
				return this.m_rewrittenCommands;
			}
			set
			{
				this.m_rewrittenCommands = value;
			}
		}

		public int Count
		{
			get
			{
				if (this.m_drillthroughHashtable == null)
				{
					return 0;
				}
				return this.m_drillthroughHashtable.Count;
			}
		}

		public ReportDrillthroughInfo()
		{
		}

		public static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.RewrittenCommands, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.TokensHashtable));
			memberInfoList.Add(new MemberInfo(MemberName.DrillthroughHashtable, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.DrillthroughHashtable));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.None, memberInfoList);
		}

		public void AddDrillthrough(string drillthroughId, DrillthroughInformation drillthroughInfo)
		{
			if (this.m_drillthroughHashtable == null)
			{
				this.m_drillthroughHashtable = new DrillthroughHashtable();
			}
			this.m_drillthroughHashtable.Add(drillthroughId, drillthroughInfo);
		}

		public void AddRewrittenCommand(int id, object value)
		{
			lock (this)
			{
				if (this.m_rewrittenCommands == null)
				{
					this.m_rewrittenCommands = new TokensHashtable();
				}
				if (!this.m_rewrittenCommands.ContainsKey(id))
				{
					this.m_rewrittenCommands.Add(id, value);
				}
			}
		}
	}
}

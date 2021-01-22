using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class RunningValueInfo : DataAggregateInfo
	{
		private string m_scope;

		public string Scope
		{
			get
			{
				return this.m_scope;
			}
			set
			{
				this.m_scope = value;
			}
		}

		public new RunningValueInfo DeepClone(InitializationContext context)
		{
			RunningValueInfo runningValueInfo = new RunningValueInfo();
			base.DeepCloneInternal(runningValueInfo, context);
			runningValueInfo.m_scope = context.EscalateScope(this.m_scope);
			return runningValueInfo;
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.Scope, Token.String));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.DataAggregateInfo, memberInfoList);
		}
	}
}

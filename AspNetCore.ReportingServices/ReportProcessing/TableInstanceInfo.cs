using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class TableInstanceInfo : ReportItemInstanceInfo
	{
		private TableColumnInstance[] m_columnInstances;

		private string m_noRows;

		public TableColumnInstance[] ColumnInstances
		{
			get
			{
				return this.m_columnInstances;
			}
			set
			{
				this.m_columnInstances = value;
			}
		}

		public string NoRows
		{
			get
			{
				return this.m_noRows;
			}
			set
			{
				this.m_noRows = value;
			}
		}

		public TableInstanceInfo(ReportProcessing.ProcessingContext pc, Table reportItemDef, TableInstance owner)
			: base(pc, reportItemDef, owner, true)
		{
			this.m_columnInstances = new TableColumnInstance[reportItemDef.TableColumns.Count];
			reportItemDef.ColumnsStartHidden = new bool[reportItemDef.TableColumns.Count];
			for (int i = 0; i < reportItemDef.TableColumns.Count; i++)
			{
				this.m_columnInstances[i] = new TableColumnInstance(pc, reportItemDef.TableColumns[i], reportItemDef);
				reportItemDef.ColumnsStartHidden[i] = this.m_columnInstances[i].StartHidden;
			}
			this.m_noRows = pc.ReportRuntime.EvaluateDataRegionNoRowsExpression(reportItemDef, reportItemDef.ObjectType, reportItemDef.Name, "NoRows");
		}

		public TableInstanceInfo(Table reportItemDef)
			: base(reportItemDef)
		{
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.ColumnInstances, Token.Array, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.TableColumnInstance));
			memberInfoList.Add(new MemberInfo(MemberName.NoRows, Token.String));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemInstanceInfo, memberInfoList);
		}
	}
}

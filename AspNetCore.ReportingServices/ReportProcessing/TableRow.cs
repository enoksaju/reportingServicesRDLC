using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using AspNetCore.ReportingServices.ReportRendering;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class TableRow : IDOwner
	{
		private ReportItemCollection m_reportItems;

		private IntList m_IDs;

		private IntList m_colSpans;

		private string m_height;

		private double m_heightValue;

		private Visibility m_visibility;

		[NonSerialized]
		private bool m_startHidden;

		[NonSerialized]
		private string m_renderingModelID;

		[NonSerialized]
		private ReportSize m_heightForRendering;

		[NonSerialized]
		private string[] m_renderingModelIDs;

		public ReportItemCollection ReportItems
		{
			get
			{
				return this.m_reportItems;
			}
			set
			{
				this.m_reportItems = value;
			}
		}

		public IntList IDs
		{
			get
			{
				return this.m_IDs;
			}
			set
			{
				this.m_IDs = value;
			}
		}

		public IntList ColSpans
		{
			get
			{
				return this.m_colSpans;
			}
			set
			{
				this.m_colSpans = value;
			}
		}

		public string Height
		{
			get
			{
				return this.m_height;
			}
			set
			{
				this.m_height = value;
			}
		}

		public double HeightValue
		{
			get
			{
				return this.m_heightValue;
			}
			set
			{
				this.m_heightValue = value;
			}
		}

		public Visibility Visibility
		{
			get
			{
				return this.m_visibility;
			}
			set
			{
				this.m_visibility = value;
			}
		}

		public string RenderingModelID
		{
			get
			{
				return this.m_renderingModelID;
			}
			set
			{
				this.m_renderingModelID = value;
			}
		}

		public ReportSize HeightForRendering
		{
			get
			{
				return this.m_heightForRendering;
			}
			set
			{
				this.m_heightForRendering = value;
			}
		}

		public string[] RenderingModelIDs
		{
			get
			{
				return this.m_renderingModelIDs;
			}
			set
			{
				this.m_renderingModelIDs = value;
			}
		}

		public bool StartHidden
		{
			get
			{
				return this.m_startHidden;
			}
			set
			{
				this.m_startHidden = value;
			}
		}

		public TableRow()
		{
		}

		public TableRow(int id, int idForReportItems)
			: base(id)
		{
			this.m_reportItems = new ReportItemCollection(idForReportItems, false);
			this.m_colSpans = new IntList();
		}

		public bool Initialize(bool registerRunningValues, int numberOfColumns, InitializationContext context, ref double tableHeight, bool[] tableColumnVisibility)
		{
			int num = 0;
			for (int i = 0; i < this.m_colSpans.Count; i++)
			{
				num += this.m_colSpans[i];
			}
			if (numberOfColumns != num)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsWrongNumberOfTableCells, Severity.Error, context.ObjectType, context.ObjectName, "TableCells");
			}
			this.m_heightValue = context.ValidateSize(ref this.m_height, "Height");
			tableHeight = Math.Round(tableHeight + this.m_heightValue, Validator.DecimalPrecision);
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context, true, true);
			}
			bool result = this.m_reportItems.Initialize(context, registerRunningValues, tableColumnVisibility);
			if (this.m_visibility != null)
			{
				this.m_visibility.UnRegisterReceiver(context);
			}
			return result;
		}

		public void RegisterReceiver(InitializationContext context)
		{
			if (this.m_visibility != null)
			{
				this.m_visibility.RegisterReceiver(context, true);
			}
			this.m_reportItems.RegisterReceiver(context);
			if (this.m_visibility != null)
			{
				this.m_visibility.UnRegisterReceiver(context);
			}
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.ReportItems, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemCollection));
			memberInfoList.Add(new MemberInfo(MemberName.IDs, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.IntList));
			memberInfoList.Add(new MemberInfo(MemberName.ColSpans, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.IntList));
			memberInfoList.Add(new MemberInfo(MemberName.Height, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.HeightValue, Token.Double));
			memberInfoList.Add(new MemberInfo(MemberName.Visibility, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.Visibility));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.IDOwner, memberInfoList);
		}
	}
}

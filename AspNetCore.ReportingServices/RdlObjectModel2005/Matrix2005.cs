using AspNetCore.ReportingServices.RdlObjectModel;
using AspNetCore.ReportingServices.RdlObjectModel.Serialization;
using AspNetCore.ReportingServices.RdlObjectModel2005.Upgrade;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel2005
{
	public class Matrix2005 : Tablix, IReportItem2005, IPageBreakLocation2005, IUpgradeable
	{
		public new class Definition : DefinitionStore<Matrix2005, Definition.Properties>
		{
			public enum Properties
			{
				Action = 36,
				PageBreakAtStart,
				PageBreakAtEnd,
				Grouping,
				Sorting,
				ReportItems,
				DataInstanceName,
				DataInstanceElementOutput,
				Corner,
				ColumnGroupings,
				RowGroupings,
				MatrixRows,
				CellDataElementName,
				CellDataElementOutput,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		public Corner2005 Corner
		{
			get
			{
				return (Corner2005)base.PropertyStore.GetObject(44);
			}
			set
			{
				base.PropertyStore.SetObject(44, value);
			}
		}

		[XmlArrayItem("ColumnGrouping", typeof(ColumnGrouping2005))]
		[XmlElement(typeof(RdlCollection<ColumnGrouping2005>))]
		public IList<ColumnGrouping2005> ColumnGroupings
		{
			get
			{
				return (IList<ColumnGrouping2005>)base.PropertyStore.GetObject(45);
			}
			set
			{
				base.PropertyStore.SetObject(45, value);
			}
		}

		[XmlArrayItem("RowGrouping", typeof(RowGrouping2005))]
		[XmlElement(typeof(RdlCollection<RowGrouping2005>))]
		public IList<RowGrouping2005> RowGroupings
		{
			get
			{
				return (IList<RowGrouping2005>)base.PropertyStore.GetObject(46);
			}
			set
			{
				base.PropertyStore.SetObject(46, value);
			}
		}

		[XmlElement(typeof(RdlCollection<MatrixRow2005>))]
		[XmlArrayItem("MatrixRow", typeof(MatrixRow2005))]
		public IList<MatrixRow2005> MatrixRows
		{
			get
			{
				return (IList<MatrixRow2005>)base.PropertyStore.GetObject(47);
			}
			set
			{
				base.PropertyStore.SetObject(47, value);
			}
		}

		[XmlArrayItem("MatrixColumn", typeof(TablixColumn))]
		[XmlElement(typeof(RdlCollection<TablixColumn>))]
		public IList<TablixColumn> MatrixColumns
		{
			get
			{
				return base.TablixBody.TablixColumns;
			}
			set
			{
				base.TablixBody.TablixColumns = value;
			}
		}

		[DefaultValue("")]
		public string CellDataElementName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(48);
			}
			set
			{
				base.PropertyStore.SetObject(48, value);
			}
		}

		[ValidEnumValues(typeof(Constants2005), "Matrix2005CellDataElementOutputTypes")]
		[DefaultValue(DataElementOutputTypes.Output)]
		public DataElementOutputTypes CellDataElementOutput
		{
			get
			{
				return (DataElementOutputTypes)base.PropertyStore.GetInteger(49);
			}
			set
			{
				((EnumProperty)DefinitionStore<Matrix2005, Definition.Properties>.GetProperty(49)).Validate(this, (int)value);
				base.PropertyStore.SetInteger(49, (int)value);
			}
		}

		[DefaultValue(false)]
		public bool PageBreakAtStart
		{
			get
			{
				return base.PropertyStore.GetBoolean(37);
			}
			set
			{
				base.PropertyStore.SetBoolean(37, value);
			}
		}

		[ReportExpressionDefaultValue]
		public ReportExpression NoRows
		{
			get
			{
				return base.NoRowsMessage;
			}
			set
			{
				base.NoRowsMessage = value;
			}
		}

		[DefaultValue(false)]
		public bool PageBreakAtEnd
		{
			get
			{
				return base.PropertyStore.GetBoolean(38);
			}
			set
			{
				base.PropertyStore.SetBoolean(38, value);
			}
		}

		public Action Action
		{
			get
			{
				return (Action)base.PropertyStore.GetObject(36);
			}
			set
			{
				base.PropertyStore.SetObject(36, value);
			}
		}

		[ReportExpressionDefaultValue]
		public ReportExpression Label
		{
			get
			{
				return base.DocumentMapLabel;
			}
			set
			{
				base.DocumentMapLabel = value;
			}
		}

		[XmlChildAttribute("Label", "LocID", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		public string LabelLocID
		{
			get
			{
				return (string)base.PropertyStore.GetObject(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		public new Style2005 Style
		{
			get
			{
				return (Style2005)base.Style;
			}
			set
			{
				base.Style = value;
			}
		}

		public Matrix2005()
		{
		}

		public Matrix2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.ColumnGroupings = new RdlCollection<ColumnGrouping2005>();
			this.RowGroupings = new RdlCollection<RowGrouping2005>();
			this.MatrixRows = new RdlCollection<MatrixRow2005>();
			this.CellDataElementOutput = DataElementOutputTypes.Output;
		}

		public void Upgrade(UpgradeImpl2005 upgrader)
		{
			upgrader.UpgradeMatrix(this);
		}
	}
}

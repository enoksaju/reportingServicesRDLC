using AspNetCore.ReportingServices.RdlObjectModel;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel2005
{
	public class MatrixRow2005 : ReportObject
	{
		public class Definition : DefinitionStore<MatrixRow2005, Definition.Properties>
		{
			public enum Properties
			{
				Height,
				MatrixCells
			}

			private Definition()
			{
			}
		}

		public ReportSize Height
		{
			get
			{
				return base.PropertyStore.GetSize(0);
			}
			set
			{
				base.PropertyStore.SetSize(0, value);
			}
		}

		[XmlElement(typeof(RdlCollection<MatrixCell2005>))]
		[XmlArrayItem("MatrixCell", typeof(MatrixCell2005))]
		public IList<MatrixCell2005> MatrixCells
		{
			get
			{
				return (IList<MatrixCell2005>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		public MatrixRow2005()
		{
		}

		public MatrixRow2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.Height = Constants.DefaultZeroSize;
			this.MatrixCells = new RdlCollection<MatrixCell2005>();
		}
	}
}

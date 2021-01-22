using System.Collections.Generic;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class ChartSeriesHierarchy : ReportObject, IHierarchy
	{
		public class Definition : DefinitionStore<ChartSeriesHierarchy, Definition.Properties>
		{
			public enum Properties
			{
				ChartMembers,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		[XmlElement(typeof(RdlCollection<ChartMember>))]
		public IList<ChartMember> ChartMembers
		{
			get
			{
				return (IList<ChartMember>)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		IEnumerable<IHierarchyMember> IHierarchy.Members
		{
			get
			{
				foreach (ChartMember chartMember in this.ChartMembers)
				{
					yield return (IHierarchyMember)chartMember;
				}
			}
		}

		public ChartSeriesHierarchy()
		{
		}

		public ChartSeriesHierarchy(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.ChartMembers = new RdlCollection<ChartMember>();
		}
	}
}

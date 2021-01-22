using System.Collections.Generic;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class DataHierarchy : ReportObject, IHierarchy
	{
		public class Definition : DefinitionStore<DataHierarchy, Definition.Properties>
		{
			public enum Properties
			{
				DataMembers
			}

			private Definition()
			{
			}
		}

		[XmlElement(typeof(RdlCollection<DataMember>))]
		public IList<DataMember> DataMembers
		{
			get
			{
				return (IList<DataMember>)base.PropertyStore.GetObject(0);
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
				foreach (DataMember dataMember in this.DataMembers)
				{
					yield return (IHierarchyMember)dataMember;
				}
			}
		}

		public DataHierarchy()
		{
		}

		public DataHierarchy(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.DataMembers = new RdlCollection<DataMember>();
		}
	}
}

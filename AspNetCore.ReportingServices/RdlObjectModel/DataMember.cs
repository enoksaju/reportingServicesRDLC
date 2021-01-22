using System.Collections.Generic;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class DataMember : HierarchyMember, IHierarchyMember
	{
		public class Definition : DefinitionStore<DataMember, Definition.Properties>
		{
			public enum Properties
			{
				Group,
				SortExpressions,
				CustomProperties,
				DataMembers,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		public override Group Group
		{
			get
			{
				return (Group)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		[XmlElement(typeof(RdlCollection<SortExpression>))]
		public IList<SortExpression> SortExpressions
		{
			get
			{
				return (IList<SortExpression>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		[XmlElement(typeof(RdlCollection<CustomProperty>))]
		public IList<CustomProperty> CustomProperties
		{
			get
			{
				return (IList<CustomProperty>)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		[XmlElement(typeof(RdlCollection<DataMember>))]
		public IList<DataMember> DataMembers
		{
			get
			{
				return (IList<DataMember>)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		IEnumerable<IHierarchyMember> IHierarchyMember.Members
		{
			get
			{
				foreach (DataMember dataMember in this.DataMembers)
				{
					yield return (IHierarchyMember)dataMember;
				}
			}
		}

		public DataMember()
		{
		}

		public DataMember(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.SortExpressions = new RdlCollection<SortExpression>();
			this.CustomProperties = new RdlCollection<CustomProperty>();
			this.DataMembers = new RdlCollection<DataMember>();
		}
	}
}

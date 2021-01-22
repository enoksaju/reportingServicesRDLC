using System.Collections.Generic;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class TablixHierarchy : ReportObject, IHierarchy
	{
		public class Definition : DefinitionStore<TablixHierarchy, Definition.Properties>
		{
			public enum Properties
			{
				TablixMembers
			}

			private Definition()
			{
			}
		}

		[XmlElement(typeof(RdlCollection<TablixMember>))]
		public IList<TablixMember> TablixMembers
		{
			get
			{
				return (IList<TablixMember>)base.PropertyStore.GetObject(0);
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
				foreach (TablixMember tablixMember in this.TablixMembers)
				{
					yield return (IHierarchyMember)tablixMember;
				}
			}
		}

		public TablixHierarchy()
		{
		}

		public TablixHierarchy(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.TablixMembers = new RdlCollection<TablixMember>();
		}
	}
}

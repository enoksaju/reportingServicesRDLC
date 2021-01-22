using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public abstract class ReportObjectBase : IContainedObject
	{
		private IPropertyStore m_propertyStore;

		[XmlIgnore]
		public IPropertyStore PropertyStore
		{
			get
			{
				return this.m_propertyStore;
			}
		}

		[XmlIgnore]
		public IContainedObject Parent
		{
			get
			{
				return this.m_propertyStore.Parent;
			}
			set
			{
				this.m_propertyStore.Parent = value;
			}
		}

		protected ReportObjectBase()
		{
			this.m_propertyStore = this.WrapPropertyStore(new PropertyStore((ReportObject)this));
			this.Initialize();
		}

		public ReportObjectBase(IPropertyStore propertyStore)
		{
			this.m_propertyStore = this.WrapPropertyStore(propertyStore);
		}

		public virtual void Initialize()
		{
		}

		public virtual IPropertyStore WrapPropertyStore(IPropertyStore propertyStore)
		{
			return propertyStore;
		}
	}
}

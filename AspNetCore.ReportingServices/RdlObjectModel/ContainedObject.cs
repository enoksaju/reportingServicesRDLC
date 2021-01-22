using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public abstract class ContainedObject : IContainedObject
	{
		private IContainedObject m_parent;

		[XmlIgnore]
		public IContainedObject Parent
		{
			get
			{
				return this.m_parent;
			}
			set
			{
				this.m_parent = value;
			}
		}
	}
}

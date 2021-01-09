using System.Diagnostics;

namespace AspNetCore.ReportingServices.Diagnostics
{
	[DebuggerDisplay("ExternalItemPath: {m_value}")]
	internal class ExternalItemPath : ItemPathBase
	{
		private string m_cachedLocalPath;

		public static readonly ExternalItemPath Empty = new ExternalItemPath(string.Empty);

		public string NativeCatalogPath
		{
			get
			{
				if (this.m_cachedLocalPath == null)
				{
					this.m_cachedLocalPath = ItemPathBase.GetLocalPath(base.m_value);
				}
				return this.m_cachedLocalPath;
			}
		}

		public CatalogItemPath NativeCatalogItemPath
		{
			get
			{
				return new CatalogItemPath(this.NativeCatalogPath, base.EditSessionID);
			}
		}

		public static ExternalItemPath ConstructFromEditSessionPath(string path)
		{
			return new ExternalItemPath(path, 0);
		}

		private ExternalItemPath(string editSessionPath, int notUsed)
			: base(editSessionPath)
		{
		}

		public static ExternalItemPath CreateWithoutChecks(string value, string editSessionID)
		{
			return new ExternalItemPath(value, editSessionID, false);
		}

		protected ExternalItemPath(string value, string editSessionID, bool runChecks)
			: base(value, editSessionID)
		{
		}

		public ExternalItemPath(string value, string editSessionID)
			: this(value, editSessionID, true)
		{
		}

		public ExternalItemPath(string value)
			: this(value, ItemPathBase.GetEditSessionID(value))
		{
		}

		public CatalogItemPath ConvertToCatalogPath(IPathTranslator pathTrans)
		{
			if (pathTrans == null)
			{
				return new CatalogItemPath(this.NativeCatalogPath, base.EditSessionID);
			}
			return new CatalogItemPath(pathTrans.ExternalToCatalog(base.Value), base.EditSessionID);
		}
	}
}

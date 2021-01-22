using System;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class ImageInstance : ReportItemInstance, IImageInstance
	{
		public abstract byte[] ImageData
		{
			get;
			set;
		}

		public abstract string StreamName
		{
			get;
			internal set;
		}

		public abstract string MIMEType
		{
			get;
			set;
		}

		public abstract ActionInfoWithDynamicImageMapCollection ActionInfoWithDynamicImageMapAreas
		{
			get;
		}

		public abstract bool IsNullImage
		{
			get;
		}

		public abstract TypeCode TagDataType
		{
			get;
		}

		public abstract object Tag
		{
			get;
		}

		public abstract string ImageDataId
		{
			get;
		}

		public Image ImageDef
		{
			get
			{
				return (Image)base.m_reportElementDef;
			}
		}

		public ImageInstance(Image reportItemDef)
			: base(reportItemDef)
		{
		}

		public abstract List<string> GetFieldsUsedInValueExpression();

		public abstract ActionInfoWithDynamicImageMap CreateActionInfoWithDynamicImageMap();
	}
}

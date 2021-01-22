using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public class ImageInstanceInfo : ReportItemInstanceInfo
	{
		private object m_data;

		private ActionInstance m_action;

		private bool m_brokenImage;

		private ImageMapAreaInstanceList m_imageMapAreas;

		public string ImageValue
		{
			get
			{
				return this.m_data as string;
			}
			set
			{
				this.m_data = value;
			}
		}

		public ImageData Data
		{
			get
			{
				return this.m_data as ImageData;
			}
			set
			{
				this.m_data = value;
			}
		}

		public object ValueObject
		{
			get
			{
				return this.m_data;
			}
		}

		public ActionInstance Action
		{
			get
			{
				return this.m_action;
			}
			set
			{
				this.m_action = value;
			}
		}

		public bool BrokenImage
		{
			get
			{
				return this.m_brokenImage;
			}
			set
			{
				this.m_brokenImage = value;
			}
		}

		public ImageMapAreaInstanceList ImageMapAreas
		{
			get
			{
				return this.m_imageMapAreas;
			}
			set
			{
				this.m_imageMapAreas = value;
			}
		}

		public ImageInstanceInfo(ReportProcessing.ProcessingContext pc, Image reportItemDef, ReportItemInstance owner, int index, bool customCreated)
			: base(pc, reportItemDef, owner, index, customCreated)
		{
		}

		public ImageInstanceInfo(Image reportItemDef)
			: base(reportItemDef)
		{
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.ImageValue, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.Action, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ActionInstance));
			memberInfoList.Add(new MemberInfo(MemberName.BrokenImage, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.ImageMapAreas, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ImageMapAreaInstanceList));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemInstanceInfo, memberInfoList);
		}
	}
}

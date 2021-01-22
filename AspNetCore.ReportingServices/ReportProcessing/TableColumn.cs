using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using AspNetCore.ReportingServices.ReportRendering;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class TableColumn
	{
		private string m_width;

		private double m_widthValue;

		private Visibility m_visibility;

		private bool m_fixedHeader;

		[NonSerialized]
		private ReportSize m_widthForRendering;

		public string Width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				this.m_width = value;
			}
		}

		public double WidthValue
		{
			get
			{
				return this.m_widthValue;
			}
			set
			{
				this.m_widthValue = value;
			}
		}

		public Visibility Visibility
		{
			get
			{
				return this.m_visibility;
			}
			set
			{
				this.m_visibility = value;
			}
		}

		public ReportSize WidthForRendering
		{
			get
			{
				return this.m_widthForRendering;
			}
			set
			{
				this.m_widthForRendering = value;
			}
		}

		public bool FixedHeader
		{
			get
			{
				return this.m_fixedHeader;
			}
			set
			{
				this.m_fixedHeader = value;
			}
		}

		public void Initialize(InitializationContext context)
		{
			this.m_widthValue = context.ValidateSize(ref this.m_width, "Width");
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context, false, true);
			}
		}

		public void RegisterReceiver(InitializationContext context)
		{
			if (this.m_visibility != null)
			{
				this.m_visibility.RegisterReceiver(context, false);
			}
		}

		public static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.Width, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.WidthValue, Token.Double));
			memberInfoList.Add(new MemberInfo(MemberName.Visibility, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.Visibility));
			memberInfoList.Add(new MemberInfo(MemberName.FixedHeader, Token.Boolean));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.None, memberInfoList);
		}
	}
}

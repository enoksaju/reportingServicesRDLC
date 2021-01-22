using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class MatrixColumn
	{
		private string m_width;

		private double m_widthValue;

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

		public void Initialize(InitializationContext context)
		{
			this.m_widthValue = context.ValidateSize(ref this.m_width, "Width");
		}

		public static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.Width, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.WidthValue, Token.Double));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.None, memberInfoList);
		}
	}
}

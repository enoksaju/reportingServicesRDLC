using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	public sealed class ExpressionInfoExtended : ExpressionInfo
	{
		[NonSerialized]
		private bool m_isExtendedSimpleFieldReference;

		public bool IsExtendedSimpleFieldReference
		{
			get
			{
				return this.m_isExtendedSimpleFieldReference;
			}
			set
			{
				this.m_isExtendedSimpleFieldReference = value;
			}
		}
	}
}

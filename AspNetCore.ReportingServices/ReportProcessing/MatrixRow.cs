using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class MatrixRow
	{
		private string m_height;

		private double m_heightValue;

		[NonSerialized]
		private int m_numberOfMatrixCells;

		public string Height
		{
			get
			{
				return this.m_height;
			}
			set
			{
				this.m_height = value;
			}
		}

		public double HeightValue
		{
			get
			{
				return this.m_heightValue;
			}
			set
			{
				this.m_heightValue = value;
			}
		}

		public int NumberOfMatrixCells
		{
			get
			{
				return this.m_numberOfMatrixCells;
			}
			set
			{
				this.m_numberOfMatrixCells = value;
			}
		}

		public void Initialize(InitializationContext context)
		{
			this.m_heightValue = context.ValidateSize(ref this.m_height, "Height");
		}

		public static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.Height, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.HeightValue, Token.Double));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.None, memberInfoList);
		}
	}
}

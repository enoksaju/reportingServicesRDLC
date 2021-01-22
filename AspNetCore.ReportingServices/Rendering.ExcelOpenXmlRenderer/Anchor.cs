using AspNetCore.ReportingServices.Rendering.ExcelOpenXmlRenderer.Model;

namespace AspNetCore.ReportingServices.Rendering.ExcelOpenXmlRenderer
{
	public sealed class Anchor
	{
		private readonly AnchorModel mModel;

		public AnchorModel Model
		{
			get
			{
				return this.mModel;
			}
		}

		public Anchor(AnchorModel model)
		{
			this.mModel = model;
		}

		public override bool Equals(object obj)
		{
			if (obj != null && obj is Anchor)
			{
				if (obj == this)
				{
					return true;
				}
				Anchor anchor = (Anchor)obj;
				return anchor.mModel.Equals(this.mModel);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return this.mModel.GetHashCode();
		}
	}
}

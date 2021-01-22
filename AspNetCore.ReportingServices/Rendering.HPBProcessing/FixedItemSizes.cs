namespace AspNetCore.ReportingServices.Rendering.HPBProcessing
{
	public class FixedItemSizes : ItemSizes
	{
		public override double Left
		{
			set
			{
			}
		}

		public override double Top
		{
			set
			{
			}
		}

		public override double Width
		{
			set
			{
			}
		}

		public override double Height
		{
			set
			{
			}
		}

		public FixedItemSizes(double width, double height)
			: base(0.0, 0.0, width, height)
		{
		}

		public override void AdjustHeightTo(double amount)
		{
		}

		public override void AdjustWidthTo(double amount)
		{
		}

		public override void MoveVertical(double delta)
		{
		}

		public override void MoveHorizontal(double delta)
		{
		}
	}
}

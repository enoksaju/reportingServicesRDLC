using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	public sealed class TextBoxList : ArrayList
	{
		public new TextBox this[int index]
		{
			get
			{
				return (TextBox)base[index];
			}
		}

		public TextBoxList()
		{
		}

		public TextBoxList(int capacity)
			: base(capacity)
		{
		}
	}
}

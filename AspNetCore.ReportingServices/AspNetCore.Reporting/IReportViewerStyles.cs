
using System.Drawing;
using System.IO;


namespace AspNetCore.Reporting
{
	public interface IReportViewerStyles
	{
		bool GetFontFromCss
		{
			get;
		}



		Color BackColor
		{
			get;
		}

		Color HoverBackColor
		{
			get;
		}

		Color SplitterBackColor
		{
			get;
		}

		string NormalButtonBorderValue
		{
			get;
		}


		string HoverButtonBorderValue
		{
			get;
		}

		Color LinkActiveColor
		{
			get;
		}

		Color LinkDisabledColor
		{
			get;
		}

		Color LinkActiveHoverColor
		{
			get;
		}

		string InternalBorderValue
		{
			get;
		}



		string HoverButtonNormal
		{
			get;
		}

		string HoverButtonHover
		{
			get;
		}

		string HoverButtonDisabled
		{
			get;
		}

		string Image
		{
			get;
		}

		string ToolbarButtonContainer
		{
			get;
		}

		string ToolbarBackground
		{
			get;
		}

		string ToolbarBack
		{
			get;
		}

		string ToolbarGroup
		{
			get;
		}

		string ToolbarGroupSpacer
		{
			get;
		}

		string ToolbarGroupShortSpacer
		{
			get;
		}

		string ToolbarInterGroupSpacing
		{
			get;
		}

		string ToolbarText
		{
			get;
		}

		string ToolbarPageNav
		{
			get;
		}

		string ToolbarCurrentPage
		{
			get;
		}

		string ToolbarRefresh
		{
			get;
		}

		string ToolbarZoom
		{
			get;
		}

		string ToolbarFind
		{
			get;
		}

		string ToolbarExport
		{
			get;
		}

		string ToolbarPrint
		{
			get;
		}

		string ToolbarPowerBI
		{
			get;
		}

		string ToolbarParams
		{
			get;
		}

		string LinkActive
		{
			get;
		}

		string LinkDisabled
		{
			get;
		}

		string SplitterNormal
		{
			get;
		}

		string SplitterHover
		{
			get;
		}

		string ViewerAreaBackground
		{
			get;
		}

		string CheckBox
		{
			get;
		}

		string ToolbarTextBox
		{
			get;
		}

		string ParameterTextBox
		{
			get;
		}

		string ParameterDisabledTextBox
		{
			get;
		}

		string ParameterContainer
		{
			get;
		}

		string EmptyDropDown
		{
			get;
		}

		string ViewReportContainer
		{
			get;
		}

		string ParameterLabel
		{
			get;
		}

		string ParameterInput
		{
			get;
		}

		string ParameterColumnSpacer
		{
			get;
		}

		string MultiValueValidValueDropDown
		{
			get;
		}

		string DocMapAndReportFrame
		{
			get;
		}

		string WaitControlBackground
		{
			get;
		}

		string WaitCell
		{
			get;
		}

		string WaitText
		{
			get;
		}

		string CancelLinkDiv
		{
			get;
		}

		string CancelLinkText
		{
			get;
		}

		string DocMapHeader
		{
			get;
		}

		string DocMapContent
		{
			get;
		}

		void AddInternalBorderAttributes(TextWriter writer, string direction);
	}
}

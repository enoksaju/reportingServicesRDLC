using AspNetCore.ReportingServices.Rendering.WordRenderer.WordOpenXmlRenderer.Parser.drawingml.x2006.main;
using AspNetCore.ReportingServices.Rendering.WordRenderer.WordOpenXmlRenderer.Parser.drawingml.x2006.picture;
using AspNetCore.ReportingServices.Rendering.WordRenderer.WordOpenXmlRenderer.Parser.drawingml.x2006.wordprocessingDrawing;
using AspNetCore.ReportingServices.Rendering.WordRenderer.WordOpenXmlRenderer.Parser.wordprocessingml.x2006.main;
using System.Collections.Generic;
using System.IO;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer.WordOpenXmlRenderer.Models
{
	public sealed class OpenXmlPictureModel : OpenXmlParagraphModel.IParagraphContent
	{
		private CT_Drawing _drawing;

		public CT_Drawing CtDrawing
		{
			get
			{
				return this._drawing;
			}
		}

		public OpenXmlPictureModel(Size size, Size desiredSize, bool clip, uint inlineId, uint picId, string relationshipId, string name)
		{
			this._drawing = new CT_Drawing
			{
				Choice_0 = CT_Drawing.ChoiceBucket_0.inline,
				Inline = new CT_Inline
				{
					Extent = new AspNetCore.ReportingServices.Rendering.WordRenderer.WordOpenXmlRenderer.Parser.drawingml.x2006.wordprocessingDrawing.CT_PositiveSize2D
					{
						Cx_Attr = desiredSize.Width,
						Cy_Attr = desiredSize.Height
					},
					DocPr = new AspNetCore.ReportingServices.Rendering.WordRenderer.WordOpenXmlRenderer.Parser.drawingml.x2006.wordprocessingDrawing.CT_NonVisualDrawingProps
					{
						Id_Attr = inlineId,
						Name_Attr = name
					},
					Graphic = new CT_GraphicalObject
					{
						GraphicData = new CT_GraphicalObjectData
						{
							Uri_Attr = "http://schemas.openxmlformats.org/drawingml/2006/picture",
							Pic = new CT_Picture
							{
								NvPicPr = new CT_PictureNonVisual
								{
									CNvPr = new AspNetCore.ReportingServices.Rendering.WordRenderer.WordOpenXmlRenderer.Parser.drawingml.x2006.picture.CT_NonVisualDrawingProps
									{
										Id_Attr = picId,
										Name_Attr = name
									},
									CNvPicPr = new CT_NonVisualPictureProperties()
								},
								BlipFill = new CT_BlipFillProperties
								{
									Blip = new CT_Blip
									{
										Embed_Attr = relationshipId,
										Cstate_Attr = "print"
									},
									Choice_0 = CT_BlipFillProperties.ChoiceBucket_0.stretch,
									Stretch = new CT_StretchInfoProperties
									{
										FillRect = new CT_RelativeRect
										{
											B_Attr = WordOpenXmlUtils.ThousandthsOfAPercentInverse((double)size.Height, (double)desiredSize.Height),
											R_Attr = WordOpenXmlUtils.ThousandthsOfAPercentInverse((double)size.Width, (double)desiredSize.Width)
										}
									}
								},
								SpPr = new CT_ShapeProperties
								{
									Xfrm = new CT_Transform2D
									{
										Off = new CT_Point2D
										{
											X_Attr = 0L,
											Y_Attr = 0L
										},
										Ext = new AspNetCore.ReportingServices.Rendering.WordRenderer.WordOpenXmlRenderer.Parser.drawingml.x2006.main.CT_PositiveSize2D
										{
											Cx_Attr = desiredSize.Width,
											Cy_Attr = desiredSize.Height
										}
									},
									Choice_0 = CT_ShapeProperties.ChoiceBucket_0.prstGeom,
									PrstGeom = new CT_PresetGeometry2D
									{
										Prst_Attr = "rect",
										AvLst = new CT_GeomGuideList()
									}
								}
							}
						}
					}
				}
			};
		}

		public void Write(TextWriter writer)
		{
			CT_R cT_R = new CT_R();
			cT_R.EG_RunInnerContents = new List<IEG_RunInnerContent>
			{
				(IEG_RunInnerContent)this.CtDrawing
			};
			cT_R.Write(writer, "r");
		}
	}
}

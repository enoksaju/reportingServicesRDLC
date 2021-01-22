using AspNetCore.ReportingServices.ReportIntermediateFormat;
using System.Drawing;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class MapCustomColor : MapObjectCollectionItem
	{
		private Map m_map;

		private AspNetCore.ReportingServices.ReportIntermediateFormat.MapCustomColor m_defObject;

		private ReportColorProperty m_color;

		public ReportColorProperty Color
		{
			get
			{
				if (this.m_color == null && this.m_defObject.Color != null)
				{
					ExpressionInfo color = this.m_defObject.Color;
					if (color != null)
					{
						this.m_color = new ReportColorProperty(color.IsExpression, this.m_defObject.Color.OriginalText, color.IsExpression ? null : new ReportColor(color.StringValue.Trim(), true), color.IsExpression ? new ReportColor("", System.Drawing.Color.Empty, true) : null);
					}
				}
				return this.m_color;
			}
		}

		public Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		public AspNetCore.ReportingServices.ReportIntermediateFormat.MapCustomColor MapCustomColorDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		public MapCustomColorInstance Instance
		{
			get
			{
				if (this.m_map.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (base.m_instance == null)
				{
					base.m_instance = new MapCustomColorInstance(this);
				}
				return (MapCustomColorInstance)base.m_instance;
			}
		}

		public MapCustomColor(AspNetCore.ReportingServices.ReportIntermediateFormat.MapCustomColor defObject, Map map)
		{
			this.m_defObject = defObject;
			this.m_map = map;
		}

		public override void SetNewContext()
		{
			if (base.m_instance != null)
			{
				base.m_instance.SetNewContext();
			}
		}
	}
}

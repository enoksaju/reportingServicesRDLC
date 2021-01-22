using System.Collections.Generic;
using System.ComponentModel;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class Image : ReportItem
	{
		public new class Definition : DefinitionStore<Image, Definition.Properties>
		{
			public enum Properties
			{
				Style,
				Name,
				ActionInfo,
				Top,
				Left,
				Height,
				Width,
				ZIndex,
				Visibility,
				ToolTip,
				ToolTipLocID,
				DocumentMapLabel,
				DocumentMapLabelLocID,
				Bookmark,
				RepeatWith,
				CustomProperties,
				DataElementName,
				DataElementOutput,
				Source,
				Value,
				MIMEType,
				Sizing
			}

			private Definition()
			{
			}
		}

		public SourceType Source
		{
			get
			{
				return (SourceType)base.PropertyStore.GetInteger(18);
			}
			set
			{
				base.PropertyStore.SetInteger(18, (int)value);
			}
		}

		public ReportExpression Value
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(19);
			}
			set
			{
				base.PropertyStore.SetObject(19, value);
			}
		}

		[ReportExpressionDefaultValue]
		public ReportExpression MIMEType
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(20);
			}
			set
			{
				base.PropertyStore.SetObject(20, value);
			}
		}

		[DefaultValue(Sizings.AutoSize)]
		public Sizings Sizing
		{
			get
			{
				return (Sizings)base.PropertyStore.GetInteger(21);
			}
			set
			{
				base.PropertyStore.SetInteger(21, (int)value);
			}
		}

		public Image()
		{
		}

		public Image(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		protected override void GetDependenciesCore(IList<ReportObject> dependencies)
		{
			base.GetDependenciesCore(dependencies);
			Report ancestor = base.GetAncestor<Report>();
			Image.GetEmbeddedImgDependencies(ancestor, dependencies, this.Source, this.Value);
		}

		public static void GetEmbeddedImgDependencies(Report report, ICollection<ReportObject> dependencies, SourceType imageSource, ReportExpression imageValue)
		{
			if (report != null && dependencies != null && !string.IsNullOrEmpty(imageValue.Expression) && imageSource == SourceType.Embedded && !imageValue.IsExpression)
			{
				EmbeddedImage embeddedImageByName = report.GetEmbeddedImageByName(imageValue.Expression);
				if (embeddedImageByName != null && !dependencies.Contains(embeddedImageByName))
				{
					dependencies.Add(embeddedImageByName);
				}
			}
		}
	}
}

using AspNetCore.ReportingServices.ReportProcessing;
using AspNetCore.ReportingServices.ReportRendering;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class ImageMapAreaInstanceCollection : ReportElementCollectionBase<ImageMapAreaInstance>
	{
		private List<ImageMapAreaInstance> m_list;

		public override ImageMapAreaInstance this[int index]
		{
			get
			{
				if (index >= 0 && index < this.Count)
				{
					return this.m_list[index];
				}
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, index, 0, this.Count);
			}
		}

		public override int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		public List<ImageMapAreaInstance> InternalList
		{
			get
			{
				return this.m_list;
			}
		}

		public ImageMapAreaInstanceCollection()
		{
			this.m_list = new List<ImageMapAreaInstance>();
		}

		public ImageMapAreaInstanceCollection(ImageMapAreasCollection imageMaps)
		{
			if (imageMaps == null)
			{
				this.m_list = new List<ImageMapAreaInstance>();
			}
			else
			{
				int count = imageMaps.Count;
				this.m_list = new List<ImageMapAreaInstance>(count);
				for (int i = 0; i < count; i++)
				{
					this.m_list.Add(new ImageMapAreaInstance(imageMaps[i]));
				}
			}
		}

		public ImageMapAreaInstance Add(ImageMapArea.ImageMapAreaShape shape, float[] coordinates)
		{
			return this.Add(shape, coordinates, null);
		}

		public ImageMapAreaInstance Add(ImageMapArea.ImageMapAreaShape shape, float[] coordinates, string toolTip)
		{
			ImageMapAreaInstance imageMapAreaInstance = new ImageMapAreaInstance(shape, coordinates, toolTip);
			this.m_list.Add(imageMapAreaInstance);
			return imageMapAreaInstance;
		}
	}
}

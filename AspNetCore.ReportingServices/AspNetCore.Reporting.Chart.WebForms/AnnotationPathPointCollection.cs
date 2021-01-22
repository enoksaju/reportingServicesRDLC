using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AspNetCore.Reporting.Chart.WebForms
{
	[SRDescription("DescriptionAttributeAnnotationPathPointCollection_AnnotationPathPointCollection")]
	public class AnnotationPathPointCollection : CollectionBase
	{
		public PolylineAnnotation annotation;

		[SRDescription("DescriptionAttributeAnnotationPathPointCollection_Item")]
		public AnnotationPathPoint this[int index]
		{
			get
			{
				return (AnnotationPathPoint)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		public void Remove(AnnotationPathPoint point)
		{
			base.List.Remove(point);
		}

		public int Add(AnnotationPathPoint point)
		{
			return base.List.Add(point);
		}

		public void Insert(int index, AnnotationPathPoint point)
		{
			base.List.Insert(index, point);
		}

		public bool Contains(AnnotationPathPoint value)
		{
			return base.List.Contains(value);
		}

		public int IndexOf(AnnotationPathPoint value)
		{
			return base.List.IndexOf(value);
		}

		protected override void OnInsertComplete(int index, object value)
		{
			this.OnCollectionChanged();
		}

		protected override void OnRemoveComplete(int index, object value)
		{
			this.OnCollectionChanged();
		}

		protected override void OnClearComplete()
		{
			this.OnCollectionChanged();
		}

		protected override void OnSetComplete(int index, object oldValue, object newValue)
		{
			this.OnCollectionChanged();
		}

		private void OnCollectionChanged()
		{
			if (this.annotation != null)
			{
				if (this.annotation.path != null)
				{
					this.annotation.path.Dispose();
					this.annotation.path = null;
				}
				if (base.List.Count > 0)
				{
					PointF[] array = new PointF[base.List.Count];
					byte[] array2 = new byte[base.List.Count];
					for (int i = 0; i < base.List.Count; i++)
					{
						array[i] = new PointF(this[i].X, this[i].Y);
						array2[i] = this[i].PointType;
					}
					this.annotation.path = new GraphicsPath(array, array2);
				}
				else
				{
					this.annotation.path = new GraphicsPath();
				}
				this.annotation.Invalidate();
			}
		}
	}
}

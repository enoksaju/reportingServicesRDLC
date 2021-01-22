using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ReportItemCollection : IDOwner, IRunningValueHolder
	{
		private ReportItemList m_nonComputedReportItems;

		private ReportItemList m_computedReportItems;

		private ReportItemIndexerList m_sortedReportItemList;

		private RunningValueInfoList m_runningValues;

		[NonSerialized]
		private bool m_normal;

		[NonSerialized]
		private bool m_unpopulated;

		[NonSerialized]
		private ReportItemList m_entries;

		[NonSerialized]
		private string m_linkToChildName;

		[NonSerialized]
		private bool m_firstInstance = true;

		public ReportItem this[int index]
		{
			get
			{
				if (this.m_unpopulated)
				{
					Global.Tracer.Assert(null != this.m_entries);
					return this.m_entries[index];
				}
				bool flag = default(bool);
				int num = default(int);
				ReportItem result = default(ReportItem);
				this.GetReportItem(index, out flag, out num, out result);
				return result;
			}
		}

		public int Count
		{
			get
			{
				if (this.m_unpopulated)
				{
					Global.Tracer.Assert(null != this.m_entries);
					return this.m_entries.Count;
				}
				if (this.m_sortedReportItemList == null)
				{
					return 0;
				}
				return this.m_sortedReportItemList.Count;
			}
		}

		public ReportItemList ComputedReportItems
		{
			get
			{
				Global.Tracer.Assert(!this.m_unpopulated);
				return this.m_computedReportItems;
			}
			set
			{
				this.m_computedReportItems = value;
			}
		}

		public ReportItemList NonComputedReportItems
		{
			get
			{
				Global.Tracer.Assert(!this.m_unpopulated);
				return this.m_nonComputedReportItems;
			}
			set
			{
				this.m_nonComputedReportItems = value;
			}
		}

		public ReportItemIndexerList SortedReportItems
		{
			get
			{
				Global.Tracer.Assert(!this.m_unpopulated);
				return this.m_sortedReportItemList;
			}
			set
			{
				this.m_sortedReportItemList = value;
			}
		}

		public RunningValueInfoList RunningValues
		{
			get
			{
				return this.m_runningValues;
			}
			set
			{
				this.m_runningValues = value;
			}
		}

		public bool FirstInstance
		{
			get
			{
				return this.m_firstInstance;
			}
			set
			{
				this.m_firstInstance = value;
			}
		}

		public string LinkToChild
		{
			set
			{
				this.m_linkToChildName = value;
			}
		}

		public ReportItemCollection()
		{
		}

		public ReportItemCollection(int id, bool normal)
			: base(id)
		{
			this.m_runningValues = new RunningValueInfoList();
			this.m_normal = normal;
			this.m_unpopulated = true;
			this.m_entries = new ReportItemList();
		}

		RunningValueInfoList IRunningValueHolder.GetRunningValueList()
		{
			return this.m_runningValues;
		}

		void IRunningValueHolder.ClearIfEmpty()
		{
			Global.Tracer.Assert(null != this.m_runningValues);
			if (this.m_runningValues.Count == 0)
			{
				this.m_runningValues = null;
			}
		}

		public void AddReportItem(ReportItem reportItem)
		{
			Global.Tracer.Assert(this.m_unpopulated);
			Global.Tracer.Assert(null != reportItem);
			Global.Tracer.Assert(null != this.m_entries);
			this.m_entries.Add(reportItem);
		}

		public void AddCustomRenderItem(ReportItem reportItem)
		{
			Global.Tracer.Assert(null != reportItem);
			this.m_unpopulated = false;
			if (this.m_sortedReportItemList == null)
			{
				this.m_nonComputedReportItems = new ReportItemList();
				this.m_computedReportItems = new ReportItemList();
				this.m_sortedReportItemList = new ReportItemIndexerList();
			}
			ReportItemIndexer reportItemIndexer = default(ReportItemIndexer);
			if (reportItem.Computed)
			{
				reportItemIndexer.Index = this.m_computedReportItems.Add(reportItem);
			}
			else
			{
				reportItemIndexer.Index = this.m_nonComputedReportItems.Add(reportItem);
			}
			reportItemIndexer.IsComputed = reportItem.Computed;
			this.m_sortedReportItemList.Add(reportItemIndexer);
		}

		public bool Initialize(InitializationContext context, bool registerRunningValues)
		{
			return this.Initialize(context, registerRunningValues, null);
		}

		public bool Initialize(InitializationContext context, bool registerRunningValues, bool[] tableColumnVisiblity)
		{
			Global.Tracer.Assert(this.m_unpopulated);
			if (registerRunningValues)
			{
				context.RegisterRunningValues(this.m_runningValues);
			}
			if ((context.Location & LocationFlags.InPageSection) == (LocationFlags)0)
			{
				context.RegisterPeerScopes(this);
			}
			Global.Tracer.Assert(null != this.m_entries);
			int count = this.m_entries.Count;
			bool flag = true;
			bool flag2 = false;
			SortedReportItemIndexList sortedReportItemIndexList = new SortedReportItemIndexList(count);
			bool result = true;
			bool tableColumnVisible = context.TableColumnVisible;
			for (int i = 0; i < count; i++)
			{
				ReportItem reportItem = this.m_entries[i];
				Global.Tracer.Assert(null != reportItem);
				if (tableColumnVisiblity != null && i < tableColumnVisiblity.Length && tableColumnVisible)
				{
					context.TableColumnVisible = tableColumnVisiblity[i];
				}
				if (!reportItem.Initialize(context))
				{
					result = false;
				}
				if (i == 0 && reportItem.Parent != null)
				{
					if ((context.Location & LocationFlags.InMatrixOrTable) != 0)
					{
						flag2 = true;
					}
					if (reportItem.Parent.HeightValue < reportItem.Parent.WidthValue)
					{
						flag = false;
					}
				}
				sortedReportItemIndexList.Add(this.m_entries, i, flag);
			}
			if (registerRunningValues)
			{
				context.UnRegisterRunningValues(this.m_runningValues);
			}
			if (count > 1 && !flag2)
			{
				this.RegisterOverlappingItems(context, count, sortedReportItemIndexList, flag);
			}
			return result;
		}

		private void RegisterOverlappingItems(InitializationContext context, int count, SortedReportItemIndexList sortedTop, bool isSortedVertically)
		{
			Hashtable hashtable = new Hashtable(count);
			for (int i = 0; i < count - 1; i++)
			{
				int num = sortedTop[i];
				double num2 = isSortedVertically ? this.m_entries[num].AbsoluteBottomValue : this.m_entries[num].AbsoluteRightValue;
				bool flag = true;
				for (int j = i + 1; j < count; j++)
				{
					if (!flag)
					{
						break;
					}
					int num3 = sortedTop[j];
					Global.Tracer.Assert(num != num3, "(currentIndex != peerIndex)");
					double num4 = isSortedVertically ? this.m_entries[num3].AbsoluteTopValue : this.m_entries[num3].AbsoluteLeftValue;
					if (num2 > num4)
					{
						int num5 = Math.Min(num, num3);
						int num6 = Math.Max(num, num3);
						IntList intList = hashtable[num5] as IntList;
						if (intList == null)
						{
							intList = new IntList();
							hashtable[num5] = intList;
						}
						intList.Add(num6);
					}
					else
					{
						flag = false;
					}
				}
			}
			foreach (int key in hashtable.Keys)
			{
				IntList intList2 = hashtable[key] as IntList;
				double num8 = isSortedVertically ? this.m_entries[key].AbsoluteLeftValue : this.m_entries[key].AbsoluteTopValue;
				double num9 = isSortedVertically ? this.m_entries[key].AbsoluteRightValue : this.m_entries[key].AbsoluteBottomValue;
				for (int k = 0; k < intList2.Count; k++)
				{
					int index = intList2[k];
					double num10 = isSortedVertically ? this.m_entries[index].AbsoluteLeftValue : this.m_entries[index].AbsoluteTopValue;
					double num11 = isSortedVertically ? this.m_entries[index].AbsoluteRightValue : this.m_entries[index].AbsoluteBottomValue;
					if (num10 > num8 && num10 < num9)
					{
						goto IL_023a;
					}
					if (num11 > num8 && num11 < num9)
					{
						goto IL_023a;
					}
					if (num10 <= num8 && num9 <= num11)
					{
						goto IL_023a;
					}
					if (num8 <= num10 && num11 <= num9)
					{
						goto IL_023a;
					}
					continue;
					IL_023a:
					context.ErrorContext.Register(ProcessingErrorCode.rsOverlappingReportItems, Severity.Warning, this.m_entries[key].ObjectType, this.m_entries[key].Name, null, ErrorContext.GetLocalizedObjectTypeString(this.m_entries[index].ObjectType), this.m_entries[index].Name);
				}
			}
		}

		public void CalculateSizes(InitializationContext context, bool overwrite)
		{
			Global.Tracer.Assert(this.m_unpopulated);
			Global.Tracer.Assert(null != this.m_entries);
			for (int i = 0; i < this.m_entries.Count; i++)
			{
				ReportItem reportItem = this.m_entries[i];
				Global.Tracer.Assert(null != reportItem);
				reportItem.CalculateSizes(context, overwrite);
			}
		}

		public void RegisterReceiver(InitializationContext context)
		{
			Global.Tracer.Assert(this.m_unpopulated);
			Global.Tracer.Assert(null != this.m_entries);
			for (int i = 0; i < this.m_entries.Count; i++)
			{
				ReportItem reportItem = this.m_entries[i];
				Global.Tracer.Assert(null != reportItem);
				reportItem.RegisterReceiver(context);
			}
		}

		public void MarkChildrenComputed()
		{
			Global.Tracer.Assert(this.m_unpopulated);
			Global.Tracer.Assert(null != this.m_entries);
			for (int i = 0; i < this.m_entries.Count; i++)
			{
				ReportItem reportItem = this.m_entries[i];
				Global.Tracer.Assert(null != reportItem);
				if (reportItem is TextBox)
				{
					reportItem.Computed = true;
				}
			}
		}

		public void Populate(ErrorContext errorContext)
		{
			Global.Tracer.Assert(this.m_unpopulated);
			Global.Tracer.Assert(null != this.m_entries);
			Hashtable hashtable = new Hashtable();
			int num = -1;
			if (0 < this.m_entries.Count)
			{
				if (this.m_normal)
				{
					this.m_entries.Sort();
				}
				this.m_nonComputedReportItems = new ReportItemList();
				this.m_computedReportItems = new ReportItemList();
				this.m_sortedReportItemList = new ReportItemIndexerList();
				for (int i = 0; i < this.m_entries.Count; i++)
				{
					ReportItem reportItem = this.m_entries[i];
					Global.Tracer.Assert(null != reportItem);
					if (reportItem is DataRegion)
					{
						hashtable[reportItem.Name] = reportItem;
					}
					ReportItemIndexer reportItemIndexer = default(ReportItemIndexer);
					if (reportItem.Computed)
					{
						reportItemIndexer.Index = this.m_computedReportItems.Add(reportItem);
					}
					else
					{
						reportItemIndexer.Index = this.m_nonComputedReportItems.Add(reportItem);
					}
					reportItemIndexer.IsComputed = reportItem.Computed;
					this.m_sortedReportItemList.Add(reportItemIndexer);
				}
			}
			this.m_unpopulated = false;
			this.m_entries = null;
			for (int j = 0; j < this.Count; j++)
			{
				ReportItem reportItem2 = this[j];
				Global.Tracer.Assert(null != reportItem2);
				if (reportItem2.RepeatWith != null)
				{
					if (reportItem2 is DataRegion || reportItem2 is SubReport || (reportItem2 is Rectangle && ((Rectangle)reportItem2).ContainsDataRegionOrSubReport()))
					{
						errorContext.Register(ProcessingErrorCode.rsInvalidRepeatWith, Severity.Error, reportItem2.ObjectType, reportItem2.Name, "RepeatWith");
					}
					if (!this.m_normal || !hashtable.ContainsKey(reportItem2.RepeatWith))
					{
						errorContext.Register(ProcessingErrorCode.rsRepeatWithNotPeerDataRegion, Severity.Error, reportItem2.ObjectType, reportItem2.Name, "RepeatWith", reportItem2.RepeatWith);
					}
					DataRegion dataRegion = (DataRegion)hashtable[reportItem2.RepeatWith];
					if (dataRegion != null)
					{
						if (dataRegion.RepeatSiblings == null)
						{
							dataRegion.RepeatSiblings = new IntList();
						}
						dataRegion.RepeatSiblings.Add(j);
					}
				}
				if (this.m_linkToChildName != null && num < 0 && reportItem2.Name.Equals(this.m_linkToChildName, StringComparison.Ordinal))
				{
					num = j;
					((Rectangle)reportItem2.Parent).LinkToChild = j;
				}
			}
		}

		public bool IsReportItemComputed(int index)
		{
			Global.Tracer.Assert(!this.m_unpopulated);
			Global.Tracer.Assert(0 <= index);
			return this.m_sortedReportItemList[index].IsComputed;
		}

		public ReportItem GetUnsortedReportItem(int index, bool computed)
		{
			Global.Tracer.Assert(!this.m_unpopulated);
			Global.Tracer.Assert(0 <= index);
			return this.InternalGet(index, computed);
		}

		public void GetReportItem(int index, out bool computed, out int internalIndex, out ReportItem reportItem)
		{
			Global.Tracer.Assert(!this.m_unpopulated);
			computed = false;
			internalIndex = -1;
			reportItem = null;
			if (this.m_sortedReportItemList != null && 0 <= index && index < this.m_sortedReportItemList.Count)
			{
				ReportItemIndexer reportItemIndexer = this.m_sortedReportItemList[index];
				if (0 <= reportItemIndexer.Index)
				{
					computed = reportItemIndexer.IsComputed;
					internalIndex = reportItemIndexer.Index;
					reportItem = this.InternalGet(internalIndex, computed);
				}
			}
		}

		private ReportItem InternalGet(int index, bool computed)
		{
			Global.Tracer.Assert(null != this.m_computedReportItems);
			Global.Tracer.Assert(null != this.m_nonComputedReportItems);
			if (computed)
			{
				return this.m_computedReportItems[index];
			}
			return this.m_nonComputedReportItems[index];
		}

		public void ProcessDrillthroughAction(ReportProcessing.ProcessingContext processingContext, NonComputedUniqueNames[] nonCompNames)
		{
			if (nonCompNames != null && this.m_nonComputedReportItems != null && this.m_nonComputedReportItems.Count != 0)
			{
				NonComputedUniqueNames nonComputedUniqueNames = null;
				for (int i = 0; i < this.m_nonComputedReportItems.Count; i++)
				{
					nonComputedUniqueNames = nonCompNames[i];
					this.m_nonComputedReportItems[i].ProcessDrillthroughAction(processingContext, nonComputedUniqueNames);
				}
			}
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.NonComputedReportItems, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemList));
			memberInfoList.Add(new MemberInfo(MemberName.ComputedReportItems, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemList));
			memberInfoList.Add(new MemberInfo(MemberName.SortedReportItems, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemIndexerList));
			memberInfoList.Add(new MemberInfo(MemberName.RunningValues, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.RunningValueInfoList));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.IDOwner, memberInfoList);
		}
	}
}

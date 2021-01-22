using AspNetCore.ReportingServices.ReportIntermediateFormat;
using AspNetCore.ReportingServices.ReportProcessing;
using AspNetCore.ReportingServices.ReportRendering;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class ActionCollection : ReportElementCollectionBase<Action>
	{
		private List<Action> m_list;

		public override Action this[int index]
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

		public ActionCollection(ActionInfo actionInfo, List<AspNetCore.ReportingServices.ReportIntermediateFormat.ActionItem> actions)
		{
			int count = actions.Count;
			this.m_list = new List<Action>(count);
			for (int i = 0; i < count; i++)
			{
				this.m_list.Add(new Action(actionInfo, actions[i], i));
			}
		}

		public ActionCollection(ActionInfo actionInfo, AspNetCore.ReportingServices.ReportRendering.ActionCollection actions)
		{
			int count = actions.Count;
			this.m_list = new List<Action>(count);
			for (int i = 0; i < count; i++)
			{
				this.m_list.Add(new Action(actionInfo, actions[i]));
			}
		}

		public Action Add(ActionInfo owner, AspNetCore.ReportingServices.ReportIntermediateFormat.ActionItem actionItem)
		{
			Action action = new Action(owner, actionItem, this.m_list.Count);
			this.m_list.Add(action);
			return action;
		}

		public void Update(AspNetCore.ReportingServices.ReportRendering.ActionInfo newCollection)
		{
			int count = this.m_list.Count;
			for (int i = 0; i < count; i++)
			{
				this.m_list[i].Update((newCollection != null && newCollection.Actions != null) ? newCollection.Actions[i] : null);
			}
		}

		public void SetNewContext()
		{
			if (this.m_list != null)
			{
				for (int i = 0; i < this.m_list.Count; i++)
				{
					this.m_list[i].SetNewContext();
				}
			}
		}

		public void ConstructActionDefinitions()
		{
			foreach (Action item in this.m_list)
			{
				item.ConstructActionDefinition();
			}
		}
	}
}

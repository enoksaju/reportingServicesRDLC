using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using AspNetCore.ReportingServices.ReportRendering;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class Visibility
	{
		private ExpressionInfo m_hidden;

		private string m_toggle;

		private bool m_recursiveReceiver;

		[NonSerialized]
		private ToggleItemInfo m_toggleItemInfo;

		public ExpressionInfo Hidden
		{
			get
			{
				return this.m_hidden;
			}
			set
			{
				this.m_hidden = value;
			}
		}

		public string Toggle
		{
			get
			{
				return this.m_toggle;
			}
			set
			{
				this.m_toggle = value;
			}
		}

		public bool RecursiveReceiver
		{
			get
			{
				return this.m_recursiveReceiver;
			}
			set
			{
				this.m_recursiveReceiver = value;
			}
		}

		public void Initialize(InitializationContext context, bool isContainer, bool tableRowCol)
		{
			if (this.m_hidden != null)
			{
				this.m_hidden.Initialize("Hidden", context);
				if (tableRowCol)
				{
					context.ExprHostBuilder.TableRowColVisibilityHiddenExpressionsExpr(this.m_hidden);
				}
				else
				{
					context.ExprHostBuilder.GenericVisibilityHidden(this.m_hidden);
				}
			}
			this.m_toggleItemInfo = this.RegisterReceiver(context, isContainer);
		}

		public ToggleItemInfo RegisterReceiver(InitializationContext context, bool isContainer)
		{
			if (context.RegisterHiddenReceiver)
			{
				return context.RegisterReceiver(this.m_toggle, this, isContainer);
			}
			return null;
		}

		public void UnRegisterReceiver(InitializationContext context)
		{
			if (this.m_toggleItemInfo != null)
			{
				context.UnRegisterReceiver(this.m_toggle, this.m_toggleItemInfo);
			}
		}

		public static SharedHiddenState GetSharedHidden(Visibility visibility)
		{
			if (visibility == null)
			{
				return SharedHiddenState.Never;
			}
			if (visibility.Toggle == null)
			{
				if (visibility.Hidden == null)
				{
					return SharedHiddenState.Never;
				}
				if (ExpressionInfo.Types.Constant == visibility.Hidden.Type)
				{
					if (visibility.Hidden.BoolValue)
					{
						return SharedHiddenState.Always;
					}
					return SharedHiddenState.Never;
				}
			}
			return SharedHiddenState.Sometimes;
		}

		public static bool HasToggle(Visibility visibility)
		{
			if (visibility == null)
			{
				return false;
			}
			if (visibility.Toggle == null)
			{
				return false;
			}
			return true;
		}

		public static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.Hidden, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.Toggle, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.RecursiveReceiver, Token.Boolean));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.None, memberInfoList);
		}

		public static bool IsOnePassHierarchyVisible(ReportItem reportItem)
		{
			if (Visibility.IsOnePassVisible(reportItem))
			{
				if (reportItem.Parent != null)
				{
					return Visibility.IsOnePassHierarchyVisible(reportItem.Parent);
				}
				return true;
			}
			return false;
		}

		private static bool IsOnePassVisible(ReportItem reportItem)
		{
			if (reportItem == null)
			{
				return false;
			}
			if (reportItem.Visibility == null)
			{
				return true;
			}
			if (reportItem.Visibility.Toggle != null)
			{
				return false;
			}
			if (reportItem.Visibility.Hidden != null)
			{
				if (ExpressionInfo.Types.Constant == reportItem.Visibility.Hidden.Type)
				{
					return !reportItem.Visibility.Hidden.BoolValue;
				}
				return !reportItem.StartHidden;
			}
			return true;
		}

		public static bool IsVisible(ReportItem reportItem)
		{
			return Visibility.IsVisible(reportItem, null, null);
		}

		public static bool IsVisible(ReportItem reportItem, ReportItemInstance reportItemInstance, ReportItemInstanceInfo reportItemInstanceInfo)
		{
			if (reportItem == null)
			{
				return false;
			}
			bool startHidden = reportItemInstance != null && reportItemInstanceInfo != null && reportItemInstanceInfo.StartHidden;
			return Visibility.IsVisible(reportItem.Visibility, startHidden);
		}

		public static bool IsVisible(Visibility visibility, bool startHidden)
		{
			if (visibility == null)
			{
				return true;
			}
			if (visibility.Toggle != null)
			{
				return true;
			}
			if (visibility.Hidden != null)
			{
				if (ExpressionInfo.Types.Constant == visibility.Hidden.Type)
				{
					return true;
				}
				return !startHidden;
			}
			return true;
		}

		public static bool IsVisible(SharedHiddenState state, bool hidden, bool hasToggle)
		{
			if (state == SharedHiddenState.Always)
			{
				return true;
			}
			if (SharedHiddenState.Never == state)
			{
				return true;
			}
			if (hasToggle)
			{
				return true;
			}
			return !hidden;
		}

		public static bool IsTableCellVisible(bool[] tableColumnsVisible, int startIndex, int colSpan)
		{
			Global.Tracer.Assert(startIndex >= 0 && colSpan > 0 && tableColumnsVisible != null && startIndex + colSpan <= tableColumnsVisible.Length);
			bool flag = false;
			for (int i = 0; i < colSpan; i++)
			{
				if (flag)
				{
					break;
				}
				flag |= tableColumnsVisible[startIndex + i];
			}
			return flag;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	internal sealed class EnumNamesAttribute : Attribute
	{
		private IList<string> m_names;

		public IList<string> Names
		{
			get
			{
				return this.m_names;
			}
		}

		public EnumNamesAttribute(Type type, string field)
		{
			string[] list = (string[])type.InvokeMember(field, BindingFlags.GetField, null, null, null, CultureInfo.InvariantCulture);
			this.m_names = new ReadOnlyCollection<string>(list);
		}
	}
}

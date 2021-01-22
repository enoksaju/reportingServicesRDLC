using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class ValidEnumValuesAttribute : Attribute
	{
		private IList<int> m_validValues;

		public IList<int> ValidValues
		{
			get
			{
				return this.m_validValues;
			}
			set
			{
				this.m_validValues = value;
			}
		}

		public ValidEnumValuesAttribute(string field)
			: this(typeof(InternalConstants), field)
		{
		}

		public ValidEnumValuesAttribute(Type type, string field)
		{
			int[] list = (int[])type.InvokeMember(field, BindingFlags.GetField, null, null, null, CultureInfo.InvariantCulture);
			this.m_validValues = new ReadOnlyCollection<int>(list);
		}
	}
}

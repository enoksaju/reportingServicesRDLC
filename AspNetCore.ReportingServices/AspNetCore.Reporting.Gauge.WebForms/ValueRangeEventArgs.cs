using System;

namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class ValueRangeEventArgs : ValueChangedEventArgs
	{
		private NamedElement pointer;

		public ValueRangeEventArgs(double value, DateTime date, string senderName, bool playbackMode, NamedElement pointer)
			: base(value, date, senderName, playbackMode)
		{
			this.pointer = pointer;
		}
	}
}

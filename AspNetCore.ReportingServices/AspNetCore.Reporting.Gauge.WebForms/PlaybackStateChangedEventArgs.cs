using System;

namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class PlaybackStateChangedEventArgs : EventArgs
	{
		private double value;

		private DateTime date;

		private PlaybackState playbackState;

		private string senderName;

		public PlaybackStateChangedEventArgs(PlaybackState playbackState, double value, DateTime date, string senderName)
		{
			this.value = value;
			this.date = date;
			this.playbackState = playbackState;
			this.senderName = senderName;
		}
	}
}

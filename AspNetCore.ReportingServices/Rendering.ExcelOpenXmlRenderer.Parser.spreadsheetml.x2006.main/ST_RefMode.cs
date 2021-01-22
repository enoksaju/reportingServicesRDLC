namespace AspNetCore.ReportingServices.Rendering.ExcelOpenXmlRenderer.Parser.spreadsheetml.x2006.main
{
	public class ST_RefMode
	{
		private string _ooxmlEnumerationValue;

		private static ST_RefMode _A1;

		private static ST_RefMode _R1C1;

		public static ST_RefMode A1
		{
			get
			{
				return ST_RefMode._A1;
			}
			private set
			{
				ST_RefMode._A1 = value;
			}
		}

		public static ST_RefMode R1C1
		{
			get
			{
				return ST_RefMode._R1C1;
			}
			private set
			{
				ST_RefMode._R1C1 = value;
			}
		}

		static ST_RefMode()
		{
			ST_RefMode.A1 = new ST_RefMode("A1");
			ST_RefMode.R1C1 = new ST_RefMode("R1C1");
		}

		private ST_RefMode(string val)
		{
			this._ooxmlEnumerationValue = val;
		}

		public override string ToString()
		{
			return this._ooxmlEnumerationValue;
		}

		public bool Equals(ST_RefMode other)
		{
			if (other == (ST_RefMode)null)
			{
				return false;
			}
			return this._ooxmlEnumerationValue == other._ooxmlEnumerationValue;
		}

		public static bool operator ==(ST_RefMode one, ST_RefMode two)
		{
			if ((object)one == null && (object)two == null)
			{
				return true;
			}
			if ((object)one != null && (object)two != null)
			{
				return one._ooxmlEnumerationValue == two._ooxmlEnumerationValue;
			}
			return false;
		}

		public static bool operator !=(ST_RefMode one, ST_RefMode two)
		{
			return !(one == two);
		}

		public override int GetHashCode()
		{
			return this._ooxmlEnumerationValue.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}
	}
}

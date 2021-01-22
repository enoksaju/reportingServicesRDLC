namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class CallbackManager
	{
		private string jsCode = "";

		private string controlUpdates = "";

		private bool disableClientUpdate;

		private string returnCommandName = string.Empty;

		private string returnCommandArgument = string.Empty;

		public bool DisableClientUpdate
		{
			get
			{
				return this.disableClientUpdate;
			}
			set
			{
				this.disableClientUpdate = value;
			}
		}

		public string ReturnCommandName
		{
			get
			{
				return this.returnCommandName;
			}
			set
			{
				this.returnCommandName = value;
			}
		}

		public string ReturnCommandArgument
		{
			get
			{
				return this.returnCommandArgument;
			}
			set
			{
				this.returnCommandArgument = value;
			}
		}

		public void ExecuteClientScript(string jsSourceCode)
		{
			this.jsCode = this.jsCode + jsSourceCode + "; ";
		}

		public void Reset()
		{
			this.jsCode = "";
			this.controlUpdates = "";
			this.disableClientUpdate = false;
			this.returnCommandName = "";
			this.returnCommandArgument = "";
		}

		public string GetJavaScript()
		{
			return this.jsCode;
		}

		public string GetControlUpdates()
		{
			return this.controlUpdates;
		}
	}
}

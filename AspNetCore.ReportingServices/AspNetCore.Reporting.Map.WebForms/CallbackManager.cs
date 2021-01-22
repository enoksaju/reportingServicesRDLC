namespace AspNetCore.Reporting.Map.WebForms
{
	public class CallbackManager : MapObject
	{
		private string jsCode = "";

		private string controlUpdates = "";

		private bool disableClientUpdate;

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

		public CallbackManager()
			: this(null)
		{
		}

		public CallbackManager(object parent)
			: base(parent)
		{
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
		}

		public string GetJavaScript()
		{
			return this.jsCode;
		}

		public void ResetControlUpdates()
		{
			this.controlUpdates = "";
		}

		public string GetControlUpdates()
		{
			return this.controlUpdates;
		}
	}
}

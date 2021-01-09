using AspNetCore.ReportingServices.Diagnostics;
using AspNetCore.ReportingServices.Diagnostics.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	internal class ReportProcessingException : ReportProcessingExceptionBase
	{
		private const string ProcessingMessagesName = "ProcessingMessages";

		private const string UseMessageListForExeptionMessageName = "UseMessageListForExeptionMessage";

		private ProcessingMessageList m_processingMessages;

		private bool m_useMessageListForExceptionMessage;

		public ProcessingMessageList ProcessingMessages
		{
			get
			{
				return this.m_processingMessages;
			}
		}

		public override string Message
		{
			get
			{
				if (this.m_useMessageListForExceptionMessage && this.m_processingMessages != null)
				{
					foreach (ProcessingMessage processingMessage in this.m_processingMessages)
					{
						if (processingMessage.Severity == Severity.Error)
						{
							return processingMessage.Message;
						}
					}
				}
				return base.Message;
			}
		}

		internal ReportProcessingException(ProcessingMessageList processingMessages)
			: this(processingMessages, null)
		{
		}

		internal ReportProcessingException(ProcessingMessageList processingMessages, Exception innerException)
			: base(ErrorCode.rsProcessingError, RPRes.Keys.GetString(ErrorCode.rsProcessingError.ToString()), innerException, Global.Tracer, null)
		{
			this.m_useMessageListForExceptionMessage = true;
			this.m_processingMessages = processingMessages;
		}

		internal ReportProcessingException(Exception innerException, ProcessingMessageList processingMessages)
			: base(ErrorCode.rsInternalError, RPRes.Keys.GetString(ErrorCode.rsUnexpectedError.ToString()), innerException, Global.Tracer, null)
		{
			this.m_processingMessages = processingMessages;
		}

		internal ReportProcessingException(ErrorCode code, Exception innerException, params object[] arguments)
			: base(code, string.Format(CultureInfo.CurrentCulture, RPRes.Keys.GetString(code.ToString()), arguments), innerException, Global.Tracer, null)
		{
		}

		internal ReportProcessingException(ErrorCode code)
			: base(code, RPRes.Keys.GetString(code.ToString()), null, Global.Tracer, null)
		{
		}

		internal ReportProcessingException(ErrorCode code, params object[] arguments)
			: base(code, string.Format(CultureInfo.CurrentCulture, RPRes.Keys.GetString(code.ToString()), arguments), null, Global.Tracer, null)
		{
		}

		internal ReportProcessingException(string errMessage, ErrorCode code)
			: base(code, errMessage, null, Global.Tracer, null)
		{
		}

		internal ReportProcessingException(string message, ErrorCode code, Exception innerException)
			: base(code, message, innerException, Global.Tracer, null)
		{
		}

		protected ReportProcessingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.m_processingMessages = (ProcessingMessageList)info.GetValue("ProcessingMessages", typeof(ProcessingMessageList));
			this.m_useMessageListForExceptionMessage = info.GetBoolean("UseMessageListForExeptionMessage");
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ProcessingMessages", this.m_processingMessages);
			info.AddValue("UseMessageListForExeptionMessage", this.m_useMessageListForExceptionMessage);
		}

		protected override XmlNode AddMoreInformationForThis(XmlDocument doc, XmlNode parent, StringBuilder errorMsgBuilder)
		{
			if (this.m_processingMessages == null)
			{
				return base.AddMoreInformationForThis(doc, parent, errorMsgBuilder);
			}
			bool flag = false;
			foreach (ProcessingMessage processingMessage3 in this.m_processingMessages)
			{
				if (processingMessage3.Severity == Severity.Error)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return base.AddMoreInformationForThis(doc, parent, errorMsgBuilder);
			}
			XmlNode xmlNode = RSException.CreateMoreInfoNode(this.Source, doc, parent);
			foreach (ProcessingMessage processingMessage4 in this.m_processingMessages)
			{
				if (processingMessage4.Severity == Severity.Error && xmlNode != null)
				{
					string text = ReportProcessingException.CodeFromMessage(processingMessage4);
					string helpLink = base.CreateHelpLink(typeof(RPRes).FullName, text);
					RSException.AddMessageToMoreInfoNode(doc, xmlNode, text, processingMessage4.Message, helpLink);
				}
			}
			return xmlNode;
		}

		protected override void AddWarnings(XmlDocument doc, XmlNode parent)
		{
			if (this.m_processingMessages != null)
			{
				foreach (ProcessingMessage processingMessage in this.m_processingMessages)
				{
					if (processingMessage.Severity == Severity.Warning)
					{
						string code = ReportProcessingException.CodeFromMessage(processingMessage);
						RSException.AddWarningNode(doc, parent, code, processingMessage.Severity.ToString(), processingMessage.ObjectName, processingMessage.ObjectType.ToString(), processingMessage.Message);
					}
				}
			}
		}

		protected override List<AdditionalMessage> GetAdditionalMessages()
		{
			if (this.m_processingMessages == null)
			{
				return null;
			}
			List<AdditionalMessage> list = new List<AdditionalMessage>(this.m_processingMessages.Count);
			foreach (ProcessingMessage processingMessage in this.m_processingMessages)
			{
				list.Add(new AdditionalMessage(ReportProcessingException.CodeFromMessage(processingMessage), processingMessage.Severity.ToString(), processingMessage.Message, processingMessage.ObjectType.ToString(), processingMessage.ObjectName, processingMessage.PropertyName, null));
			}
			return list;
		}

		private static string CodeFromMessage(ProcessingMessage message)
		{
			if (message.Code == ProcessingErrorCode.rsNone)
			{
				return message.CommonCode.ToString();
			}
			return message.Code.ToString();
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;

namespace AspNetCore.ReportingServices.ProgressivePackaging
{
	public class ProgressiveTypeDictionary
	{
		public const string DummyContent = ".";

		public const string KeyRdlx = "rdlx";

		public const string KeyRdlxPath = "rdlxPath";

		public const string KeyDataSegmentationQuery = "dsq";

		public const string KeyExecuteQueriesRequest = "eqr";

		public const string KeyInteractiveState = "is";

		public const string KeyGetExternalImagesRequest = "getExternalImagesRequest";

		public const string KeyLogClientTraceEventsRequest = "logClientTraceEventsRequest";

		public const string KeySessionId = "sessionId";

		public const string KeyPublishingWarnings = "publishingWarnings";

		public const string KeyProcessingWarnings = "processingWarnings";

		public const string KeyRpds = "rpds";

		public const string KeyModelDefinition = "modelDefinition";

		public const string KeyDataSources = "dataSources";

		public const string KeyGetExternalImagesResponse = "getExternalImagesResponse";

		public const string KeyLogClientTraceEventsResponse = "processedTraceEvents";

		public const string KeyNumCancellableJobs = "numCancellableJobs";

		public const string KeyNumCancelledJobs = "numCancelledJobs";

		public const string KeyWasReportLastModifiedByCurrentUser = "wasReportLastModifiedByCurrentUser";

		public const string KeyAdditionalInformation = "additionalInformation";

		public const string KeyAdditionalModelMetadata = "additionalModelMetadata";

		public const string KeyServerError = "serverError";

		public const string KeyServerErrorCode = "serverErrorCode";

		public const string ServerErrorCodeInvalidReportArchiveFormat = "rsInvalidReportArchiveFormat";

		public const string ServerErrorCodeSessionNotFound = "SessionNotFound";

		public const string ServerErrorCodeInvalidSessionId = "InvalidSessionId";

		public const string ServerErrorCodeProcessingError = "rsProcessingError";

		public const string ServerErrorCodeRenderingError = "rsRenderingError";

		public const string ServerErrorCodeCommandExecutionError = "rsErrorExecutingCommand";

		public const string ServerErrorCodeMissingExecuteQueriesRequest = "MissingExecuteQueriesRequest";

		public const string ServerErrorCodeInvalidConcurrentRenderEditSessionRequest = "InvalidConcurrentRenderEditSessionRequest";

		public const string ServerErrorCodeMissingGetExternalImagesRequest = "MissingGetExternalImagesRequest";

		public const string ServerErrorCodeExternalImageInvalidUri = "ExternalImageInvalidUri";

		public const string ServerErrorCodeExternalImageHttpError = "ExternalImageHttpError";

		public const string ServerErrorCodeExternalImageNetworkError = "ExternalImageNetworkError";

		public const string ServerErrorCodeExternalImageInvalidContent = "ExternalImageInvalidContent";

		public const string ServerErrorCodeExternalImageUnexpectedError = "ExternalImageUnexpectedError";

		public const string ServerErrorCodeExternalImageDisallowedError = "ExternalImageDisallowedError";

		public const string ServerErrorCodeMissingLogClientTraceEventsRequest = "MissingLogClientTraceEventsRequest";

		public const string ServerErrorCodeReadingNextDataRowError = "rsErrorReadingNextDataRow";

		public const string ServerErrorCodeReadingDataFieldError = "rsErrorReadingDataField";

		public const string ServerErrorCodeASCloudConnectionError = "ASCloudConnectionError";

		public const string ServerErrorCodeASQueryExceededMemoryLimitError = "ASQueryExceededMemoryLimitError";

		public const string ServerErrorCodeGatewayCommunicationError = "gwCommunicationError";

		private static readonly Dictionary<string, Type> m_TypeDictionary;

		static ProgressiveTypeDictionary()
		{
			ProgressiveTypeDictionary.m_TypeDictionary = new Dictionary<string, Type>(StringComparer.Ordinal);
			ProgressiveTypeDictionary.m_TypeDictionary.Add("rdlx", typeof(Stream));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("rdlxPath", typeof(string));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("dsq", typeof(Stream));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("eqr", typeof(Stream));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("is", typeof(Stream));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("getExternalImagesRequest", typeof(Stream));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("logClientTraceEventsRequest", typeof(Stream));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("additionalInformation", typeof(Dictionary<string, object>));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("sessionId", typeof(string));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("publishingWarnings", typeof(string[]));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("processingWarnings", typeof(string[]));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("rpds", typeof(Stream));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("modelDefinition", typeof(Stream));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("dataSources", typeof(Stream));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("getExternalImagesResponse", typeof(Stream));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("processedTraceEvents", typeof(bool));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("numCancellableJobs", typeof(int));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("numCancelledJobs", typeof(int));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("serverError", typeof(string));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("serverErrorCode", typeof(string));
			ProgressiveTypeDictionary.m_TypeDictionary.Add("additionalModelMetadata", typeof(Stream));
		}

		public static Type GetType(string name)
		{
			if (ProgressiveTypeDictionary.m_TypeDictionary.ContainsKey(name))
			{
				return ProgressiveTypeDictionary.m_TypeDictionary[name];
			}
			return null;
		}

		public static bool IsErrorMessageElement(MessageElement messageElement)
		{
			if (messageElement == null)
			{
				return false;
			}
			string name = messageElement.Name;
			if (!"serverError".Equals(name, StringComparison.Ordinal))
			{
				return "serverErrorCode".Equals(name, StringComparison.Ordinal);
			}
			return true;
		}
	}
}

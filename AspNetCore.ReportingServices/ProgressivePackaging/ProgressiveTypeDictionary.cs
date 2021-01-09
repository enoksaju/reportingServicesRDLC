using System;
using System.Collections.Generic;
using System.IO;

namespace AspNetCore.ReportingServices.ProgressivePackaging
{
	internal class ProgressiveTypeDictionary
	{
		internal const string DummyContent = ".";

		internal const string KeyRdlx = "rdlx";

		internal const string KeyRdlxPath = "rdlxPath";

		internal const string KeyDataSegmentationQuery = "dsq";

		internal const string KeyExecuteQueriesRequest = "eqr";

		internal const string KeyInteractiveState = "is";

		internal const string KeyGetExternalImagesRequest = "getExternalImagesRequest";

		internal const string KeyLogClientTraceEventsRequest = "logClientTraceEventsRequest";

		internal const string KeySessionId = "sessionId";

		internal const string KeyPublishingWarnings = "publishingWarnings";

		internal const string KeyProcessingWarnings = "processingWarnings";

		internal const string KeyRpds = "rpds";

		internal const string KeyModelDefinition = "modelDefinition";

		internal const string KeyDataSources = "dataSources";

		internal const string KeyGetExternalImagesResponse = "getExternalImagesResponse";

		internal const string KeyLogClientTraceEventsResponse = "processedTraceEvents";

		internal const string KeyNumCancellableJobs = "numCancellableJobs";

		internal const string KeyNumCancelledJobs = "numCancelledJobs";

		internal const string KeyWasReportLastModifiedByCurrentUser = "wasReportLastModifiedByCurrentUser";

		internal const string KeyAdditionalInformation = "additionalInformation";

		internal const string KeyAdditionalModelMetadata = "additionalModelMetadata";

		internal const string KeyServerError = "serverError";

		internal const string KeyServerErrorCode = "serverErrorCode";

		internal const string ServerErrorCodeInvalidReportArchiveFormat = "rsInvalidReportArchiveFormat";

		internal const string ServerErrorCodeSessionNotFound = "SessionNotFound";

		internal const string ServerErrorCodeInvalidSessionId = "InvalidSessionId";

		internal const string ServerErrorCodeProcessingError = "rsProcessingError";

		internal const string ServerErrorCodeRenderingError = "rsRenderingError";

		internal const string ServerErrorCodeCommandExecutionError = "rsErrorExecutingCommand";

		internal const string ServerErrorCodeMissingExecuteQueriesRequest = "MissingExecuteQueriesRequest";

		internal const string ServerErrorCodeInvalidConcurrentRenderEditSessionRequest = "InvalidConcurrentRenderEditSessionRequest";

		internal const string ServerErrorCodeMissingGetExternalImagesRequest = "MissingGetExternalImagesRequest";

		internal const string ServerErrorCodeExternalImageInvalidUri = "ExternalImageInvalidUri";

		internal const string ServerErrorCodeExternalImageHttpError = "ExternalImageHttpError";

		internal const string ServerErrorCodeExternalImageNetworkError = "ExternalImageNetworkError";

		internal const string ServerErrorCodeExternalImageInvalidContent = "ExternalImageInvalidContent";

		internal const string ServerErrorCodeExternalImageUnexpectedError = "ExternalImageUnexpectedError";

		internal const string ServerErrorCodeExternalImageDisallowedError = "ExternalImageDisallowedError";

		internal const string ServerErrorCodeMissingLogClientTraceEventsRequest = "MissingLogClientTraceEventsRequest";

		internal const string ServerErrorCodeReadingNextDataRowError = "rsErrorReadingNextDataRow";

		internal const string ServerErrorCodeReadingDataFieldError = "rsErrorReadingDataField";

		internal const string ServerErrorCodeASCloudConnectionError = "ASCloudConnectionError";

		internal const string ServerErrorCodeASQueryExceededMemoryLimitError = "ASQueryExceededMemoryLimitError";

		internal const string ServerErrorCodeGatewayCommunicationError = "gwCommunicationError";

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

		internal static Type GetType(string name)
		{
			if (ProgressiveTypeDictionary.m_TypeDictionary.ContainsKey(name))
			{
				return ProgressiveTypeDictionary.m_TypeDictionary[name];
			}
			return null;
		}

		internal static bool IsErrorMessageElement(MessageElement messageElement)
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

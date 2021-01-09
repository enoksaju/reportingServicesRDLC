using Microsoft.Win32;
using System;
using System.IO;
using System.Net;

namespace AspNetCore.ReportingServices.Diagnostics
{
	internal static class ExternalResourceLoader
	{
		internal static readonly int MaxResourceSizeUnlimited = -1;

		public static byte[] GetExternalResource(string resourceUrl, bool impersonate, string surrogateUser, string surrogatePassword, string surrogateDomain, int webTimeout, int maxResourceSizeBytes, ExternalResourceAbortHelper abortHelper, out string mimeType, out bool resourceExceededMaxSize)
		{
			byte[] result = null;
			mimeType = null;
			Uri uri = new Uri(resourceUrl);
			WebRequest webRequest = (!uri.IsFile) ? ((WebRequest)(HttpWebRequest)WebRequest.Create(uri)) : ((WebRequest)(FileWebRequest)WebRequest.Create(uri));
			int timeout = 600000;
			if (webTimeout > 0 && webTimeout < 2147483)
			{
				webRequest.Timeout = webTimeout * 1000;
			}
			else
			{
				webRequest.Timeout = timeout;
			}
			if (surrogateUser != null)
			{
				webRequest.Credentials = new NetworkCredential(surrogateUser, surrogatePassword, surrogateDomain);
			}
			else if (impersonate)
			{
				webRequest.Credentials = CredentialCache.DefaultCredentials;
			}
			else
			{
				webRequest.Credentials = null;
			}
			resourceExceededMaxSize = false;
			using (WebResponse webResponse = ExternalResourceLoader.RequestExternalResource(webRequest, abortHelper))
			{
				mimeType = webResponse.ContentType;
				using (Stream s = webResponse.GetResponseStream())
				{
					result = ((maxResourceSizeBytes != ExternalResourceLoader.MaxResourceSizeUnlimited) ? StreamSupport.ReadToEndNotUsingLength(s, 1024, maxResourceSizeBytes, out resourceExceededMaxSize) : StreamSupport.ReadToEndNotUsingLength(s, 1024));
				}
			}
			if (uri.IsFile && !resourceExceededMaxSize)
			{
				string text = Path.GetExtension(uri.LocalPath).ToUpperInvariant();
				if (text != null && text.StartsWith(".", StringComparison.Ordinal))
				{
					text = text.Substring(1);
				}
				string mimeTypeByRegistryLookup = ExternalResourceLoader.GetMimeTypeByRegistryLookup(text);
				if (mimeTypeByRegistryLookup != null)
				{
					mimeType = mimeTypeByRegistryLookup;
				}
			}
			return result;
		}

		public static bool IsValidResourceSize(int maxResourceBytes, byte[] contents)
		{
			if (maxResourceBytes != ExternalResourceLoader.MaxResourceSizeUnlimited && contents != null)
			{
				return contents.Length <= maxResourceBytes;
			}
			return true;
		}

		private static WebResponse RequestExternalResource(WebRequest request, ExternalResourceAbortHelper abortHelper)
		{
			if (abortHelper == null)
			{
				return request.GetResponse();
			}
			IAsyncResult asyncResult = request.BeginGetResponse(null, null);
			while (!asyncResult.AsyncWaitHandle.WaitOne(1000, false))
			{
				if (abortHelper.IsAborted)
				{
					request.Abort();
				}
			}
			return request.EndGetResponse(asyncResult);
		}

		public static string GetMimeTypeByRegistryLookup(string extension)
		{
			string mimeType = null;
            //todo: can delete?
            RevertImpersonationContext.Run(delegate
			{
				 
				try
				{
                    using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey("." + extension))
                    {
                        if (registryKey != null)
                        {
                            object value = registryKey.GetValue("Content Type");
                            if (value is string)
                            {
                                mimeType = (string)value;
                            }
                        }
                    }
                }
				catch (Exception)
				{
				}
				 
			});

            //try
            //{
            //    using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey("." + extension))
            //    {
            //        if (registryKey != null)
            //        {
            //            object value = registryKey.GetValue("Content Type");
            //            if (value is string)
            //            {
            //                mimeType = (string)value;
            //            }
            //        }
            //    }
            //}
            //catch (Exception)
            //{
            //}
            return mimeType;
		}
	}
}

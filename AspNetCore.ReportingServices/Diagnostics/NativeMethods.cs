using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AspNetCore.ReportingServices.Diagnostics
{
	public sealed class NativeMethods
	{
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct DATA_BLOB
		{
			public int cbData;

			public IntPtr pbData;
		}

		public const int CurrentUser = 0;

		public const int UIForbidden = 1;

		public const int LocalMachine = 4;

		[DllImport("crypt32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool CryptProtectData(SafeCryptoBlobIn dataIn, string szDataDescr, IntPtr optionalEntropy, IntPtr pvReserved, IntPtr pPromptStruct, int dwFlags, SafeCryptoBlobOut pDataOut);

		[DllImport("crypt32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool CryptUnprotectData(SafeCryptoBlobIn dataIn, StringBuilder ppszDataDescr, IntPtr optionalEntropy, IntPtr pvReserved, IntPtr pPromptStruct, int dwFlags, SafeCryptoBlobOut pDataOut);

		[DllImport("kernel32.dll")]
		public static extern IntPtr LocalFree(IntPtr hMem);
	}
}

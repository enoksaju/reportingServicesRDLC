using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace AspNetCore.ReportingServices.Diagnostics
{
	public class NativeMemoryMethods
	{
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern SafeLocalFree LocalAlloc(int uFlags, UIntPtr sizetdwBytes);

		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static extern IntPtr LocalFree(IntPtr handle);

		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool AllocateAndInitializeSid(SafeLocalFree pSidAuthPtr, byte nSubAuthorityCount, uint nSubAuthority0, uint nSubAuthority1, uint nSubAuthority2, uint nSubAuthority3, uint nSubAuthority4, uint nSubAuthority5, uint nSubAuthority6, uint nSubAuthority7, out SafeSidPtr pSid);

		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr FreeSid(IntPtr pSid);

		[DllImport("ole32.dll")]
		public static extern SafeCoTaskMem CoTaskMemAlloc(int cb);

		[DllImport("ole32.dll")]
		public static extern void CoTaskMemFree(IntPtr ptr);
	}
}

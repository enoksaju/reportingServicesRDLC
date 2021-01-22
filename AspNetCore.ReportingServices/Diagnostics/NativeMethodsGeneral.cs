using System.Runtime.InteropServices;

namespace AspNetCore.ReportingServices.Diagnostics
{
	public static class NativeMethodsGeneral
	{
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		private struct MEMORYSTATUSEX
		{
			public int dwLength;

			public int dwMemoryLoad;

			public long ullTotalPhys;

			public long ullAvailPhys;

			public long ullTotalPageFile;

			public long ullAvailPageFile;

			public long ullTotalVirtual;

			public long ullAvailVirtual;

			public long ullAvailExtendedVirtual;

			public void Init()
			{
				this.dwLength = Marshal.SizeOf(typeof(MEMORYSTATUSEX));
			}
		}

		public static bool GlobalMemoryStatusEx(out long ullAvailPhys, out long ullAvailVirtual)
		{
			MEMORYSTATUSEX mEMORYSTATUSEX = default(MEMORYSTATUSEX);
			mEMORYSTATUSEX.Init();
			if (NativeMethodsGeneral.GlobalMemoryStatusEx(ref mEMORYSTATUSEX) != 0)
			{
				ullAvailPhys = mEMORYSTATUSEX.ullAvailPhys;
				ullAvailVirtual = mEMORYSTATUSEX.ullAvailVirtual;
				return true;
			}
			ullAvailPhys = 0L;
			ullAvailVirtual = 0L;
			return false;
		}

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern int GlobalMemoryStatusEx(ref MEMORYSTATUSEX memoryStatusEx);
	}
}

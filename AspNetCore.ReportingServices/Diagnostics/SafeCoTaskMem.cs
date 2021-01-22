using Microsoft.Win32.SafeHandles;
using System;

namespace AspNetCore.ReportingServices.Diagnostics
{
	public sealed class SafeCoTaskMem : SafeHandleZeroOrMinusOneIsInvalid
	{
		private SafeCoTaskMem()
			: base(true)
		{
		}

		private SafeCoTaskMem(bool ownsHandle)
			: base(ownsHandle)
		{
		}

		public SafeCoTaskMem Alloc(int cb)
		{
			SafeCoTaskMem safeCoTaskMem = NativeMemoryMethods.CoTaskMemAlloc(cb);
			if (safeCoTaskMem.IsInvalid)
			{
				safeCoTaskMem.SetHandleAsInvalid();
				throw new OutOfMemoryException();
			}
			return safeCoTaskMem;
		}

		protected override bool ReleaseHandle()
		{
			NativeMemoryMethods.CoTaskMemFree(base.handle);
			return true;
		}
	}
}

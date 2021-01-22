using System;
using System.Runtime.InteropServices;

namespace AspNetCore.ReportingServices.Rendering.RichText
{
	public class ScriptProperties
	{
		private long m_value;

		private static ScriptProperties[] ScriptsProps;

		public bool IsComplex
		{
			get
			{
				return (this.m_value >> 17 & 1) > 0;
			}
		}

		public byte CharSet
		{
			get
			{
				return (byte)(this.m_value >> 20 & 0xFF);
			}
		}

		public bool IsAmbiguousCharSet
		{
			get
			{
				return (this.m_value >> 34 & 1) > 0;
			}
		}

		public static int Length
		{
			get
			{
				return ScriptProperties.ScriptsProps.Length;
			}
		}

		public ScriptProperties(long value)
		{
			this.m_value = value;
		}

		static ScriptProperties()
		{
			IntPtr ptr = default(IntPtr);
			int num = default(int);
			int num2 = Win32.ScriptGetProperties(out ptr, out num);
			if (Win32.Failed(num2))
			{
				Marshal.ThrowExceptionForHR(num2);
			}
			ScriptProperties.ScriptsProps = new ScriptProperties[num];
			for (int i = 0; i < num; i++)
			{
				IntPtr ptr2 = Marshal.ReadIntPtr(ptr, i * IntPtr.Size);
				long value = Marshal.ReadInt64(ptr2);
				ScriptProperties.ScriptsProps[i] = new ScriptProperties(value);
			}
		}

		public static ScriptProperties GetProperties(int script)
		{
			return ScriptProperties.ScriptsProps[script];
		}
	}
}

using Microsoft.Win32.SafeHandles;
using System;
using System.Globalization;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer
{
	public static class StructuredStorage
	{
		public sealed class OLEStructuredStorage
		{
			[Guid("0000000a-0000-0000-C000-000000000046")]
			[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
			public interface UCOMILockBytes
			{
				void ReadAt(ulong offset, [Out] [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] pv, int cb, out int pcbRead);

				void WriteAt(ulong offset, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] pv, int cb, out int pcbWritten);

				void Flush();

				void SetSize(int cb);

				void LockRegion(int libOffset, int cb, long dwLoclType);

				void UnlockRegion(int libOffset, int cb, long dwLoclType);

				void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag);
			}

			[Guid("0000000d-0000-0000-C000-000000000046")]
			[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
			public interface IEnumSTATSTG
			{
				void Next(int celt, out System.Runtime.InteropServices.ComTypes.STATSTG rgelt, out int pceltFetched);
			}

			[Guid("0000000b-0000-0000-C000-000000000046")]
			[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
			public interface UCOMIStorage
			{
				void CreateStream([MarshalAs(UnmanagedType.LPWStr)] string wcsName, int grfMode, int reserved1, int reserved2, out IStream stream);

				void OpenStream([MarshalAs(UnmanagedType.LPWStr)] string wcsName, IntPtr reserved1, int grfMode, int reserved2, out IStream stream);

				void CreateStorage([MarshalAs(UnmanagedType.LPWStr)] string wcsName, int grfMode, int reserved1, int reserved2, out UCOMIStorage storage);

				void OpenStorage([MarshalAs(UnmanagedType.LPWStr)] string wcsName, UCOMIStorage pstgPriority, int grfMode, IntPtr[] snbExclude, int reserved1, out UCOMIStorage storage);

				void CopyTo(int ciidExclude, IntPtr[] rgiidExclude, IntPtr[] snbExclude, out UCOMIStorage storage);

				void MoveTo([MarshalAs(UnmanagedType.LPWStr)] string wcsName, UCOMIStorage pstgDest, [MarshalAs(UnmanagedType.LPWStr)] string pwcsNewName, int grfFlags);

				void Commit(int grfCommitFlags);

				void Revert();

				void EnumElements(int reserved1, IntPtr reserved2, int reserved3, out IEnumSTATSTG ppenum);

				void DestroyElement([MarshalAs(UnmanagedType.LPWStr)] string pwcsName);

				void RenameElement([MarshalAs(UnmanagedType.LPWStr)] string pwcsOldName, [MarshalAs(UnmanagedType.LPWStr)] string pwcsNewName);

				void SetElementTimes([MarshalAs(UnmanagedType.LPWStr)] string pwcsName, ref System.Runtime.InteropServices.ComTypes.FILETIME pctime, ref System.Runtime.InteropServices.ComTypes.FILETIME patime, ref System.Runtime.InteropServices.ComTypes.FILETIME pmtime);

				void SetClass(ref Guid clsid);
			}

			[ComImport]
			[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
			[Guid("0000013A-0000-0000-C000-000000000046")]
			[ComVisible(true)]
			public interface IPropertySetStorage
			{
				uint Create([In] [MarshalAs(UnmanagedType.Struct)] ref Guid rfmtid, [In] IntPtr pclsid, [In] int grfFlags, [In] int grfMode, out IPropertyStorage propertyStorage);

				int Open([In] [MarshalAs(UnmanagedType.Struct)] ref Guid rfmtid, [In] int grfMode, [Out] IPropertyStorage propertyStorage);
			}

			[ComImport]
			[ComVisible(true)]
			[Guid("00000138-0000-0000-C000-000000000046")]
			[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
			public interface IPropertyStorage
			{
				int ReadMultiple(uint numProperties, PropSpec[] propertySpecifications, PropVariant[] propertyValues);

				int WriteMultiple(uint numProperties, ref PropSpec propertySpecification, ref PropVariant propertyValues, int propIDNameFirst);

				uint Commit(int commitFlags);
			}

			public struct PropSpec
			{
				[MarshalAs(UnmanagedType.U4)]
				public int ulKind;

				public IntPtr strPtr;
			}

			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
			public struct PropVariant : IDisposable
			{
				public short variantType;

				public short wReserved1;

				public short wReserved2;

				public short wReserved3;

				public HGlobalSafeHandle pointerValue;

				public void FromString(string str)
				{
					this.variantType = 31;
					this.pointerValue = new HGlobalSafeHandle(Marshal.StringToHGlobalUni(str));
				}

				public void Dispose()
				{
					if (this.pointerValue != null)
					{
						this.pointerValue.Close();
						this.pointerValue = null;
					}
				}

				public string PointerValue()
				{
					if (this.pointerValue != null)
					{
						return this.pointerValue.MarshalToString();
					}
					return null;
				}
			}

			public class HGlobalSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
			{
				public HGlobalSafeHandle(IntPtr handle)
					: base(true)
				{
					base.SetHandle(handle);
				}

				[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
				protected override bool ReleaseHandle()
				{
					Marshal.FreeHGlobal(base.handle);
					return true;
				}

				public string MarshalToString()
				{
					return Marshal.PtrToStringUni(base.handle);
				}
			}

			public const long MEMORY_THRESHOLD = 11534336L;

			public const int STGM_SIMPLE = 134217728;

			public const int STGM_READ = 0;

			public const int STGM_READWRITE = 2;

			public const int STGM_SHARE_DENY_NONE = 64;

			public const int STGM_SHARE_EXCLUSIVE = 16;

			public const int STGM_CREATE = 4096;

			public const int STGM_DELETEONRELEASE = 67108864;

			public const int PIDSI_TITLE = 2;

			public const int PIDSI_AUTHOR = 4;

			public const int PIDSI_COMMENTS = 6;

			public const short VT_LPWSTR = 31;

			public const int PRSPEC_PROPID = 1;

			[DllImport("ole32.dll")]
			public static extern int StgCreateDocfile([MarshalAs(UnmanagedType.LPWStr)] string wcsName, int grfMode, int reserved, out UCOMIStorage storage);

			[DllImport("ole32.dll")]
			public static extern int StgOpenStorage([MarshalAs(UnmanagedType.LPWStr)] string wcsName, IntPtr stgPriority, int grfMode, IntPtr snbExclude, int reserved, out UCOMIStorage storage);

			[DllImport("ole32.dll")]
			public static extern int StgCreateDocfileOnILockBytes(UCOMILockBytes plkbyt, int grfMode, int reserved, out UCOMIStorage storage);

			[DllImport("ole32.dll")]
			public static extern int CreateILockBytesOnHGlobal(IntPtr hGlobal, bool fDeleteOnRelease, out UCOMILockBytes lockBytes);

			[DllImport("ole32.dll")]
			public static extern int StgCreateStorageEx([MarshalAs(UnmanagedType.LPWStr)] string wcsName, int grfMode, int stgfmt, int grfAttr, IntPtr StgOptions, IntPtr reserved2, ref Guid refiid, out UCOMIStorage storage);

			[DllImport("ole32.dll")]
			public static extern int StgCreatePropSetStg(UCOMIStorage storage, int reserverd, out IPropertySetStorage propSetStg);

			public static bool Failed(int hr)
			{
				return hr < 0;
			}
		}

		private const int BUFFERSIZE = 1048576;

		public static bool CreateMultiStreamFile(Stream[] sources, string[] streamNames, string clsId, string author, string title, string comments, Stream output, bool forceInMemory)
		{
			OLEStructuredStorage.UCOMIStorage uCOMIStorage = null;
			OLEStructuredStorage.UCOMILockBytes uCOMILockBytes = null;
			IStream stream = null;
			string text = null;
			Guid guid = new Guid(clsId);
			long num = 0L;
			long num2 = 0L;
			for (int i = 0; i < sources.Length; i++)
			{
				num2 = Math.Max(sources[i].Length, num2);
				num += sources[i].Length;
			}
			int num3 = Math.Min(1048576, (int)num2 + 512);
			try
			{
				int grfMode = 134221842;
				int grfMode2 = 18;
				text = Path.GetTempPath() + Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture) + ".doc.tmp";
				if (OLEStructuredStorage.StgCreateDocfile(text, grfMode, 0, out uCOMIStorage) != 0)
				{
					return false;
				}
				byte[] array = new byte[num3];
				for (int j = 0; j < streamNames.Length; j++)
				{
					sources[j].Seek(0L, SeekOrigin.Begin);
					uCOMIStorage.CreateStream(streamNames[j], grfMode2, 0, 0, out stream);
					if (stream != null)
					{
						IntPtr pcbWritten = default(IntPtr);
						int num4 = 0;
						while ((num4 = sources[j].Read(array, 0, num3)) > 0)
						{
							stream.Write(array, num4, pcbWritten);
						}
						sources[j] = null;
					}
					Marshal.ReleaseComObject(stream);
					stream = null;
				}
				OLEStructuredStorage.IPropertySetStorage propertySetStorage = null;
				OLEStructuredStorage.IPropertyStorage propertyStorage = null;
				int num5 = OLEStructuredStorage.StgCreatePropSetStg(uCOMIStorage, 0, out propertySetStorage);
				Guid guid2 = new Guid("{F29F85E0-4FF9-1068-AB91-08002B27B3D9}");
				propertySetStorage.Create(ref guid2, IntPtr.Zero, 0, grfMode2, out propertyStorage);
				StructuredStorage.WriteProperty(propertyStorage, 2, title);
				StructuredStorage.WriteProperty(propertyStorage, 4, author);
				StructuredStorage.WriteProperty(propertyStorage, 6, comments);
				Marshal.ReleaseComObject(propertyStorage);
				Marshal.ReleaseComObject(propertySetStorage);
				uCOMIStorage.SetClass(ref guid);
				uCOMIStorage.Commit(0);
				Marshal.ReleaseComObject(uCOMIStorage);
				uCOMIStorage = null;
				using (Stream stream2 = File.OpenRead(text))
				{
					int num6 = 0;
					while ((num6 = stream2.Read(array, 0, array.Length)) > 0)
					{
						output.Write(array, 0, num6);
					}
				}
				array = null;
				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (!string.IsNullOrEmpty(text) && File.Exists(text))
				{
					File.Delete(text);
				}
				if (stream != null)
				{
					Marshal.ReleaseComObject(stream);
					stream = null;
				}
				if (uCOMIStorage != null)
				{
					Marshal.ReleaseComObject(uCOMIStorage);
					uCOMIStorage = null;
				}
				if (uCOMILockBytes != null)
				{
					Marshal.ReleaseComObject(uCOMILockBytes);
					uCOMILockBytes = null;
				}
			}
		}

		private static void WriteProperty(OLEStructuredStorage.IPropertyStorage propertyStorage, int propid, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				OLEStructuredStorage.PropVariant propVariant = default(OLEStructuredStorage.PropVariant);
				OLEStructuredStorage.PropSpec propSpec = default(OLEStructuredStorage.PropSpec);
				propSpec.ulKind = 1;
				propSpec.strPtr = new IntPtr(propid);
				try
				{
					propVariant.FromString(value);
					propertyStorage.WriteMultiple(1u, ref propSpec, ref propVariant, 2);
					propertyStorage.Commit(0);
				}
				finally
				{
					propVariant.Dispose();
				}
			}
		}
	}
}

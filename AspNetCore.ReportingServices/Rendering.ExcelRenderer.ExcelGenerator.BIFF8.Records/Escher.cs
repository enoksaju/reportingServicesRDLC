using AspNetCore.ReportingServices.Diagnostics.Utilities;
using AspNetCore.ReportingServices.Rendering.ExcelRenderer.Excel.BIFF8;
using AspNetCore.ReportingServices.Rendering.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.ExcelGenerator.BIFF8.Records
{
	public sealed class Escher
	{
		public class EscherHeader
		{
			public const uint CbLengthOffset = 4u;

			private uint m_escherHeader;

			private uint m_cbLength;

			public uint Instance
			{
				set
				{
					if (value < 4096)
					{
						this.m_escherHeader &= 4294901775u;
						this.m_escherHeader |= value << 4;
					}
				}
			}

			public virtual uint Length
			{
				get
				{
					return this.m_cbLength;
				}
				set
				{
					this.m_cbLength = value;
				}
			}

			public EscherHeader(ushort ver, uint inst, EscherType fbt, uint cbLength)
			{
				if (ver < 16)
				{
					this.m_escherHeader = ver;
				}
				if (inst < 4096)
				{
					this.m_escherHeader |= inst << 4;
				}
				if (65535 >= (int)fbt)
				{
					this.m_escherHeader = ((ushort)this.m_escherHeader | (uint)fbt << 16);
				}
				this.m_cbLength = cbLength;
			}

			public virtual byte[] GetData()
			{
				byte[] array = new byte[8];
				LittleEndianHelper.WriteIntU(this.m_escherHeader, array, 0);
				LittleEndianHelper.WriteIntU(this.m_cbLength, array, 4);
				return array;
			}
		}

		public sealed class DrawingGroupContainer : EscherHeader
		{
			public sealed class CheckSumImage
			{
				private byte[] m_checkSum;

				private int m_streamIndex;

				public byte[] CheckSum
				{
					get
					{
						return this.m_checkSum;
					}
				}

				public int StreamIndex
				{
					get
					{
						return this.m_streamIndex;
					}
				}

				public CheckSumImage(byte[] checkSum, int streamIndex)
				{
					this.m_checkSum = checkSum;
					this.m_streamIndex = streamIndex;
				}
			}

			public class ImageStream
			{
				private Stream m_stream;

				private string m_name;

				private int m_offset;

				public Stream Stream
				{
					get
					{
						return this.m_stream;
					}
				}

				public string Name
				{
					get
					{
						return this.m_name;
					}
				}

				public int Offset
				{
					get
					{
						return this.m_offset;
					}
					set
					{
						RSTrace.ExcelRendererTracer.Assert(value < this.m_stream.Length, "The current position in the stream cannot exceed the stream length");
						this.m_offset = value;
					}
				}

				public ImageStream(Stream stream, string name, EscherType escherType)
				{
					this.m_stream = stream;
					this.m_name = name;
					if (escherType == EscherType.MSOFBTBLIP_DIB)
					{
						this.m_offset = 14;
					}
				}
			}

			public const int BitmapFileHeaderSize = 14;

			private DrawingGroup m_drawingGroup;

			private BlipStoreContainer m_bStoreContainer;

			private List<ImageStream> m_images;

			private Dictionary<int, ushort> m_clusters;

			private Dictionary<string, CheckSumImage> m_imageTable;

			public List<ImageStream> StreamList
			{
				get
				{
					return this.m_images;
				}
			}

			public override uint Length
			{
				get
				{
					return 50 + this.m_bStoreContainer.Length + this.m_drawingGroup.Length + 8;
				}
			}

			public byte[] DrawingGroupContainerData
			{
				get
				{
					base.Length = this.Length;
					return this.GetData();
				}
			}

			public byte[] DrawingGroupData
			{
				get
				{
					if (this.m_drawingGroup == null)
					{
						return null;
					}
					return this.m_drawingGroup.GetData();
				}
			}

			public byte[] BStoreContainerData
			{
				get
				{
					if (this.m_bStoreContainer == null)
					{
						return null;
					}
					return this.m_bStoreContainer.GetData();
				}
			}

			public Hashtable BSEList
			{
				get
				{
					if (this.m_bStoreContainer == null)
					{
						return null;
					}
					return this.m_bStoreContainer.BSEList;
				}
			}

			public ArrayList BlipList
			{
				get
				{
					if (this.m_bStoreContainer == null)
					{
						return null;
					}
					return this.m_bStoreContainer.BlipList;
				}
			}

			public byte[] ShapePropertyData
			{
				get
				{
					return ShapeProperty.GetData();
				}
			}

			public DrawingGroupContainer()
				: base(15, 0u, EscherType.MSOFBTDGGCONTAINER, 0u)
			{
				this.m_drawingGroup = new DrawingGroup();
			}

			private void UpdateImageStreamDGC(string name, Stream imageData, EscherType escherType, out int streamIndex)
			{
				if (this.m_images != null)
				{
					for (int i = 0; i < this.m_images.Count; i++)
					{
						if (this.m_images[i].Name.Equals(name))
						{
							streamIndex = i;
							return;
						}
					}
				}
				else
				{
					this.m_images = new List<ImageStream>();
				}
				ImageStream item = new ImageStream(imageData, name, escherType);
				this.m_images.Add(item);
				streamIndex = this.m_images.Count - 1;
			}

			public uint AddImage(Stream imageData, ImageFormat format, string imageName, int workSheetId, out uint startSPID, out ushort dgID)
			{
				EscherType escherType = EscherType.MSOFBTUNKNOWN;
				BlipType blipType = BlipType.MSOBLIPUNKNOWN;
				BlipSignature blipSignature = BlipSignature.MSOBIUNKNOWN;
				if (format.Equals(ImageFormat.Bmp))
				{
					escherType = EscherType.MSOFBTBLIP_DIB;
					blipType = BlipType.MSOBLIPDIB;
					blipSignature = BlipSignature.MSOBIDIB;
				}
				else if (format.Equals(ImageFormat.Jpeg))
				{
					escherType = EscherType.MSOFBTBLIP_JPEG;
					blipType = BlipType.MSOBLIPJPEG;
					blipSignature = BlipSignature.MSOBIJFIF;
				}
				else if (format.Equals(ImageFormat.Gif))
				{
					escherType = EscherType.MSOFBTBLIP_GIF;
					blipType = BlipType.MSOBLIPPNG;
					blipSignature = BlipSignature.MSOBIPNG;
				}
				else if (format.Equals(ImageFormat.Png))
				{
					escherType = EscherType.MSOFBTBLIP_GIF;
					blipType = BlipType.MSOBLIPPNG;
					blipSignature = BlipSignature.MSOBIPNG;
				}
				if (this.m_clusters == null)
				{
					this.m_clusters = new Dictionary<int, ushort>();
				}
				if (this.m_bStoreContainer == null)
				{
					this.m_bStoreContainer = new BlipStoreContainer();
				}
				if (this.m_imageTable == null)
				{
					this.m_imageTable = new Dictionary<string, CheckSumImage>();
				}
				int num = (int)imageData.Length;
				CheckSumImage checkSumImage;
				if (this.m_imageTable.ContainsKey(imageName))
				{
					checkSumImage = this.m_imageTable[imageName];
				}
				else
				{
					byte[] checkSum = Escher.CheckSum(imageData);
					int streamPosFromCheckSum = this.m_bStoreContainer.GetStreamPosFromCheckSum(checkSum);
					if (streamPosFromCheckSum == -1)
					{
						this.UpdateImageStreamDGC(imageName, imageData, escherType, out streamPosFromCheckSum);
					}
					checkSumImage = new CheckSumImage(checkSum, streamPosFromCheckSum);
					this.m_imageTable.Add(imageName, checkSumImage);
				}
				if (escherType == EscherType.MSOFBTBLIP_DIB)
				{
					num -= 14;
					this.StreamList[checkSumImage.StreamIndex].Offset = 14;
				}
				uint result = this.AddImage(checkSumImage.CheckSum, checkSumImage.StreamIndex, num, escherType, blipType, blipSignature, workSheetId);
				dgID = this.m_clusters[workSheetId];
				startSPID = this.m_drawingGroup.GetStartingSPID(dgID);
				return result;
			}

			private uint AddImage(byte[] checkSum, int streamIndex, int imageLength, EscherType escherType, BlipType blipType, BlipSignature blipSignature, int workSheetId)
			{
				if (this.m_clusters.ContainsKey(workSheetId))
				{
					int dgID = this.m_clusters[workSheetId];
					this.m_drawingGroup.IncrementShapeCount(dgID);
				}
				else
				{
					this.m_clusters.Add(workSheetId, (ushort)(this.m_clusters.Count + 1));
					int dgID = this.m_clusters.Count;
					this.m_drawingGroup.AddCluster(dgID);
					this.m_drawingGroup.IncrementShapeCount(dgID);
				}
				return this.m_bStoreContainer.AddImage(checkSum, streamIndex, imageLength, escherType, blipType, blipSignature);
			}
		}

		public sealed class FIDCL
		{
			public uint m_dgid;

			public uint m_cspidCurr;

			public FIDCL(uint dgid, uint cspidCurr)
			{
				this.m_dgid = dgid;
				this.m_cspidCurr = cspidCurr;
			}
		}

		public sealed class DrawingGroup : EscherHeader
		{
			private const uint FixedRecordLength = 16u;

			private ArrayList m_dgCluster;

			private uint m_cdgSaved;

			public override uint Length
			{
				get
				{
					return 16 + this.m_cdgSaved * 8;
				}
			}

			public DrawingGroup()
				: base(0, 0u, EscherType.MSOFBTDGG, 0u)
			{
			}

			public uint GetStartingSPID(int dgID)
			{
				if (this.m_dgCluster != null && dgID >= 1)
				{
					if (dgID == 1)
					{
						return 1024u;
					}
					return this.m_cdgSaved * 1024;
				}
				return 0u;
			}

			public void AddCluster(int dgID)
			{
				if (this.m_dgCluster == null)
				{
					this.m_dgCluster = new ArrayList();
				}
				FIDCL value = new FIDCL((uint)dgID, 1u);
				if (dgID > this.m_dgCluster.Count)
				{
					ArrayList arrayList = new ArrayList();
					arrayList.Add(value);
					this.m_dgCluster.Add(arrayList);
				}
				else
				{
					ArrayList arrayList2 = (ArrayList)this.m_dgCluster[dgID - 1];
					arrayList2.Add(value);
				}
				this.m_cdgSaved += 1u;
			}

			public uint GetCurrentSpid(int dgID)
			{
				if (this.m_dgCluster != null && dgID >= 1 && dgID <= this.m_dgCluster.Count)
				{
					ArrayList arrayList = (ArrayList)this.m_dgCluster[dgID - 1];
					return ((FIDCL)arrayList[arrayList.Count - 1]).m_cspidCurr;
				}
				return 0u;
			}

			public void IncrementShapeCount(int dgID)
			{
				if (this.m_dgCluster != null && dgID >= 1 && dgID <= this.m_dgCluster.Count)
				{
					if (this.GetCurrentSpid(dgID) % 1024u == 0)
					{
						this.AddCluster(dgID);
					}
					else
					{
						ArrayList arrayList = (ArrayList)this.m_dgCluster[dgID - 1];
						((FIDCL)arrayList[arrayList.Count - 1]).m_cspidCurr += 1u;
					}
				}
			}

			public override byte[] GetData()
			{
				byte[] array = new byte[this.Length + 8];
				base.Length = this.Length;
				int num = 0;
				byte[] data = base.GetData();
				data.CopyTo(array, num);
				num += data.Length;
				ArrayList arrayList = (ArrayList)this.m_dgCluster[this.m_dgCluster.Count - 1];
				uint value = this.m_cdgSaved * 1024 + ((FIDCL)arrayList[arrayList.Count - 1]).m_cspidCurr;
				data = BitConverter.GetBytes(value);
				data.CopyTo(array, num);
				num += data.Length;
				data = BitConverter.GetBytes(this.m_cdgSaved + 1);
				data.CopyTo(array, num);
				num += data.Length;
				uint num2 = 0u;
				int num3 = 0;
				byte[] array2 = new byte[this.m_cdgSaved * 8];
				for (int i = 0; i < this.m_dgCluster.Count; i++)
				{
					ArrayList arrayList2 = (ArrayList)this.m_dgCluster[i];
					for (int j = 0; j < arrayList2.Count; j++)
					{
						data = BitConverter.GetBytes(((FIDCL)arrayList2[j]).m_dgid);
						data.CopyTo(array2, num3);
						num3 += data.Length;
						num2 += ((FIDCL)arrayList2[j]).m_cspidCurr;
						data = BitConverter.GetBytes(((FIDCL)arrayList2[j]).m_cspidCurr);
						data.CopyTo(array2, num3);
						num3 += data.Length;
					}
				}
				data = BitConverter.GetBytes(num2);
				data.CopyTo(array, num);
				num += data.Length;
				data = BitConverter.GetBytes(this.m_dgCluster.Count);
				data.CopyTo(array, num);
				num += data.Length;
				array2.CopyTo(array, num);
				return array;
			}
		}

		public class BlipStoreContainer : EscherHeader
		{
			private const int BSELength = 44;

			private Hashtable m_bSEList;

			private ArrayList m_blipList;

			private int m_totalLength;

			public Hashtable BSEList
			{
				get
				{
					return this.m_bSEList;
				}
			}

			public ArrayList BlipList
			{
				get
				{
					return this.m_blipList;
				}
			}

			public override uint Length
			{
				get
				{
					return (uint)(8 + this.m_totalLength);
				}
			}

			public uint ShapeCount
			{
				get
				{
					return (uint)this.m_blipList.Count;
				}
			}

			public BlipStoreContainer()
				: base(15, 0u, EscherType.MSOFBTBSTORECONTAINER, 0u)
			{
			}

			public uint AddImage(byte[] checkSum, int streamIndex, int imageLength, EscherType escherType, BlipType blipType, BlipSignature blipSignature)
			{
				if (this.m_bSEList == null)
				{
					this.m_bSEList = new Hashtable();
				}
				string @string = Encoding.ASCII.GetString(checkSum);
				if (this.m_bSEList.ContainsKey(@string))
				{
					BlipStoreEntry blipStoreEntry = (BlipStoreEntry)this.m_bSEList[@string];
					blipStoreEntry.ReferenceCount += 1u;
					return blipStoreEntry.ReferenceIndex;
				}
				if (this.m_blipList == null)
				{
					this.m_blipList = new ArrayList();
				}
				Blip blip = new Blip(checkSum, streamIndex, imageLength, escherType, blipSignature);
				this.m_blipList.Add(blip);
				base.Instance = (uint)this.m_blipList.Count;
				BlipStoreEntry value = new BlipStoreEntry(checkSum, blipType, blip.Length, (uint)this.m_blipList.Count);
				this.m_bSEList.Add(@string, value);
				this.m_totalLength += 44 + imageLength + 8 + 16 + 1;
				return (uint)this.m_blipList.Count;
			}

			public int GetStreamPosFromCheckSum(byte[] checkSum)
			{
				if (this.m_bSEList != null && this.m_bSEList.ContainsKey(Encoding.ASCII.GetString(checkSum)))
				{
					BlipStoreEntry blipStoreEntry = (BlipStoreEntry)this.m_bSEList[Encoding.ASCII.GetString(checkSum)];
					return ((Blip)this.m_blipList[(int)(blipStoreEntry.ReferenceIndex - 1)]).StreamIndex;
				}
				return -1;
			}

			public override byte[] GetData()
			{
				if (this.m_blipList == null)
				{
					return null;
				}
				base.Length = (uint)this.m_totalLength;
				return base.GetData();
			}
		}

		public sealed class BlipStoreEntry : EscherHeader
		{
			private const ushort RecordLength = 36;

			private byte m_btWin32;

			private byte m_btMacOS;

			private byte[] m_rgbUID;

			private ushort m_tag = 255;

			private uint m_size;

			private uint m_cRef;

			private uint m_MSOFO;

			private byte usage;

			private byte cbName;

			private byte unused2;

			private byte unused3;

			private uint m_referenceIndex;

			public uint ReferenceIndex
			{
				get
				{
					return this.m_referenceIndex;
				}
			}

			public uint ReferenceCount
			{
				get
				{
					return this.m_cRef;
				}
				set
				{
					this.m_cRef = value;
				}
			}

			public BlipStoreEntry(byte[] checkSum, BlipType blipType, uint atomLength, uint referenceIndex)
				: base(2, (uint)blipType, EscherType.MSOFBTBSE, (ushort)(atomLength + 36 + 8))
			{
				this.m_btWin32 = (byte)blipType;
				this.m_btMacOS = (byte)blipType;
				this.m_rgbUID = checkSum;
				this.m_size = atomLength + 8;
				this.m_referenceIndex = referenceIndex;
				this.m_cRef = 1u;
			}

			public override byte[] GetData()
			{
				byte[] array = new byte[44];
				int num = 0;
				byte[] data = base.GetData();
				data.CopyTo(array, num);
				num += data.Length;
				array[num] = this.m_btWin32;
				num++;
				array[num] = this.m_btMacOS;
				num++;
				this.m_rgbUID.CopyTo(array, num);
				num += this.m_rgbUID.Length;
				data = BitConverter.GetBytes(this.m_tag);
				data.CopyTo(array, num);
				num += data.Length;
				data = BitConverter.GetBytes(this.m_size);
				data.CopyTo(array, num);
				num += data.Length;
				data = BitConverter.GetBytes(this.m_cRef);
				data.CopyTo(array, num);
				num += data.Length;
				data = BitConverter.GetBytes(this.m_MSOFO);
				data.CopyTo(array, num);
				num += data.Length;
				array[num] = this.usage;
				num++;
				array[num] = this.cbName;
				num++;
				array[num] = this.unused2;
				num++;
				array[num] = this.unused3;
				return array;
			}
		}

		public sealed class Blip : EscherHeader
		{
			private byte[] m_rgbUID;

			private byte m_bTag;

			private int m_imageLength;

			private int m_streamIndex = -1;

			public int StreamIndex
			{
				get
				{
					return this.m_streamIndex;
				}
			}

			public override uint Length
			{
				get
				{
					return (uint)(this.m_imageLength + 16 + 1);
				}
			}

			public byte[] CheckSum
			{
				get
				{
					return this.m_rgbUID;
				}
			}

			public Blip(byte[] checkSum, int streamIndex, int imageLength, EscherType escherType, BlipSignature blipSignature)
				: base(0, (uint)blipSignature, escherType, (uint)(imageLength + 16 + 1))
			{
				this.m_rgbUID = checkSum;
				this.m_bTag = 255;
				this.m_streamIndex = streamIndex;
				this.m_imageLength = imageLength;
			}

			public byte[] GetHeaderData()
			{
				byte[] array = new byte[8 + this.m_rgbUID.Length + 1];
				int num = 0;
				byte[] data = this.GetData();
				data.CopyTo(array, num);
				num += data.Length;
				this.m_rgbUID.CopyTo(array, num);
				num += this.m_rgbUID.Length;
				array[num] = this.m_bTag;
				return array;
			}
		}

		public sealed class ShapeProperty
		{
			public static byte[] GetData()
			{
				return new byte[50]
				{
					51,
					0,
					11,
					240,
					18,
					0,
					0,
					0,
					191,
					0,
					8,
					0,
					8,
					0,
					129,
					1,
					65,
					0,
					0,
					8,
					192,
					1,
					64,
					0,
					0,
					8,
					64,
					0,
					30,
					241,
					16,
					0,
					0,
					0,
					13,
					0,
					0,
					8,
					12,
					0,
					0,
					8,
					23,
					0,
					0,
					8,
					247,
					0,
					0,
					16
				};
			}
		}

		public sealed class DrawingContainer : EscherHeader
		{
			private Drawing m_drawing;

			private ShapeGroupContainer m_shapeGroupContainer;

			private ArrayList m_shapeContainer;

			public DrawingContainer(ushort drawingID)
				: base(15, 0u, EscherType.MSOFBTDGCONTAINER, 0u)
			{
				this.m_drawing = new Drawing(drawingID);
				this.m_shapeGroupContainer = new ShapeGroupContainer();
			}

			public int AddShape(uint spid, string imageName, ClientAnchor.SPRC clientAnchorInfo, uint referenceIndex)
			{
				if (this.m_shapeContainer == null)
				{
					this.m_shapeContainer = new ArrayList();
				}
				if (this.m_shapeContainer.Count == 0)
				{
					uint spid2 = spid / 1024u * 1024;
					this.m_shapeContainer.Add(new ShapeContainer(spid2, ShapeType.MSOSPTMIN, (ShapeFlag)5));
				}
				this.m_shapeContainer.Add(new ShapeContainer(spid, ShapeType.MSOSPTPICTUREFRAME, (ShapeFlag)2560, clientAnchorInfo, referenceIndex, imageName));
				this.m_drawing.LastSPID = spid;
				this.m_drawing.ShapeCount = (uint)this.m_shapeContainer.Count;
				return this.m_shapeContainer.Count;
			}

			public int AddShape(uint spid, string imageName, ClientAnchor.SPRC clientAnchorInfo, uint referenceIndex, string hyperLinkName, HyperlinkType hyperLinkType)
			{
				if (this.m_shapeContainer == null)
				{
					this.m_shapeContainer = new ArrayList();
				}
				if (this.m_shapeContainer.Count == 0)
				{
					uint spid2 = spid / 1024u * 1024;
					this.m_shapeContainer.Add(new ShapeContainer(spid2, ShapeType.MSOSPTMIN, (ShapeFlag)5));
				}
				this.m_shapeContainer.Add(new ShapeContainer(spid, ShapeType.MSOSPTPICTUREFRAME, (ShapeFlag)2560, clientAnchorInfo, referenceIndex, imageName, hyperLinkName, hyperLinkType));
				this.m_drawing.LastSPID = spid;
				this.m_drawing.ShapeCount = (uint)this.m_shapeContainer.Count;
				return this.m_shapeContainer.Count;
			}

			public override byte[] GetData()
			{
				return null;
			}

			public void WriteToStream(BinaryWriter output)
			{
				if (this.m_shapeContainer != null)
				{
					long position = output.BaseStream.Position;
					RecordFactory.WriteHeader(output, 236, 0);
					int num = 0;
					long position2 = output.BaseStream.Position;
					byte[] data = base.GetData();
					output.BaseStream.Write(data, 0, data.Length);
					num += data.Length;
					data = this.m_drawing.GetData();
					output.BaseStream.Write(data, 0, data.Length);
					num += data.Length;
					long position3 = output.BaseStream.Position + 4;
					data = this.m_shapeGroupContainer.GetData();
					output.BaseStream.Write(data, 0, data.Length);
					num += data.Length;
					int num2 = 0;
					int num3 = num;
					ushort num4 = 1;
					for (int i = 0; i < this.m_shapeContainer.Count; i++)
					{
						data = ((ShapeContainer)this.m_shapeContainer[i]).GetData();
						if (i < 2)
						{
							num += data.Length;
						}
						else
						{
							num4 = (ushort)(num4 + 1);
							RecordFactory.WriteHeader(output, 236, data.Length);
						}
						output.BaseStream.Write(data, 0, data.Length);
						num2 += data.Length;
						num3 += data.Length;
						if (i > 0)
						{
							RecordFactory.OBJ(output, num4);
						}
					}
					long position4 = output.BaseStream.Position;
					uint value = (uint)(num3 - 8);
					output.BaseStream.Position = position2 + 4;
					byte[] bytes = BitConverter.GetBytes(value);
					output.BaseStream.Write(bytes, 0, bytes.Length);
					output.BaseStream.Position = position3;
					bytes = BitConverter.GetBytes((uint)num2);
					output.BaseStream.Write(bytes, 0, bytes.Length);
					bytes = BitConverter.GetBytes((ushort)num);
					output.BaseStream.Position = position + 2;
					output.BaseStream.Write(bytes, 0, bytes.Length);
					output.BaseStream.Position = position4;
				}
			}
		}

		public sealed class Drawing : EscherHeader
		{
			private uint m_csp;

			private uint m_spidCur;

			public uint LastSPID
			{
				set
				{
					this.m_spidCur = value;
				}
			}

			public uint ShapeCount
			{
				set
				{
					this.m_csp = value;
				}
			}

			public Drawing(ushort drawingID)
				: base(0, drawingID, EscherType.MSOFBTDG, 8u)
			{
			}

			public override byte[] GetData()
			{
				byte[] array = new byte[16];
				int num = 0;
				byte[] data = base.GetData();
				data.CopyTo(array, num);
				num += data.Length;
				data = BitConverter.GetBytes(this.m_csp);
				data.CopyTo(array, num);
				num += data.Length;
				data = BitConverter.GetBytes(this.m_spidCur);
				data.CopyTo(array, num);
				return array;
			}
		}

		public sealed class ShapeGroupContainer : EscherHeader
		{
			public ShapeGroupContainer()
				: base(15, 0u, EscherType.MSOFBTSPGRCONTAINER, 0u)
			{
			}

			public override byte[] GetData()
			{
				byte[] array = new byte[8];
				int index = 0;
				byte[] data = base.GetData();
				data.CopyTo(array, index);
				return array;
			}
		}

		public sealed class ShapeContainer : EscherHeader
		{
			private ShapeGroup m_shapeGroup;

			private Shape m_shape;

			private DrawingOpt m_drawingOpt;

			private ClientAnchor m_clientAnchor;

			private ClientData m_clientData;

			public ShapeContainer(uint spid, ShapeType shapeType, ShapeFlag shapeFlags)
				: base(15, 0u, EscherType.MSOFBTSPCONTAINER, 0u)
			{
				this.m_shapeGroup = new ShapeGroup(0u, 0u, 0u, 0u);
				this.m_shape = new Shape(shapeType, shapeFlags, spid);
			}

			public ShapeContainer(uint spid, ShapeType shapeType, ShapeFlag shapeFlags, ClientAnchor.SPRC clientAnchorInfo, uint refIndex, string imageName)
				: base(15, 0u, EscherType.MSOFBTSPCONTAINER, 0u)
			{
				this.m_shape = new Shape(shapeType, shapeFlags, spid);
				this.m_drawingOpt = new DrawingOpt(imageName, refIndex);
				this.m_clientAnchor = new ClientAnchor(clientAnchorInfo);
				this.m_clientData = new ClientData();
			}

			public ShapeContainer(uint spid, ShapeType shapeType, ShapeFlag shapeFlags, ClientAnchor.SPRC clientAnchorInfo, uint refIndex, string imageName, string hyperLinkName, HyperlinkType hyperLinkType)
				: base(15, 0u, EscherType.MSOFBTSPCONTAINER, 0u)
			{
				this.m_shape = new Shape(shapeType, shapeFlags, spid);
				this.m_drawingOpt = new DrawingOpt(imageName, refIndex, hyperLinkName, hyperLinkType);
				this.m_clientAnchor = new ClientAnchor(clientAnchorInfo);
				this.m_clientData = new ClientData();
			}

			public override byte[] GetData()
			{
				MemoryStream memoryStream = new MemoryStream();
				byte[] data = base.GetData();
				memoryStream.Write(data, 0, data.Length);
				if (this.m_shapeGroup != null)
				{
					data = this.m_shapeGroup.GetData();
					memoryStream.Write(data, 0, data.Length);
					data = this.m_shape.GetData();
					memoryStream.Write(data, 0, data.Length);
				}
				else
				{
					data = this.m_shape.GetData();
					memoryStream.Write(data, 0, data.Length);
					data = this.m_drawingOpt.GetData();
					memoryStream.Write(data, 0, data.Length);
					data = this.m_clientAnchor.GetData();
					memoryStream.Write(data, 0, data.Length);
					data = this.m_clientData.GetData();
					memoryStream.Write(data, 0, data.Length);
				}
				uint value = (uint)(memoryStream.Length - 8);
				byte[] bytes = BitConverter.GetBytes(value);
				bytes.CopyTo(memoryStream.GetBuffer(), 4L);
				memoryStream.Position = 0L;
				return memoryStream.ToArray();
			}
		}

		public sealed class ShapeGroup : EscherHeader
		{
			private uint m_left;

			private uint m_top;

			private uint m_right;

			private uint m_bottom;

			public override uint Length
			{
				get
				{
					return 16u;
				}
			}

			public ShapeGroup(uint left, uint right, uint top, uint bottom)
				: base(1, 0u, EscherType.MSOFBTSPGR, 16u)
			{
				this.m_left = left;
				this.m_top = top;
				this.m_right = right;
				this.m_bottom = bottom;
			}

			public override byte[] GetData()
			{
				byte[] array = new byte[24];
				int num = 0;
				byte[] data = base.GetData();
				data.CopyTo(array, num);
				num += data.Length;
				data = BitConverter.GetBytes(this.m_left);
				data.CopyTo(array, num);
				num += data.Length;
				data = BitConverter.GetBytes(this.m_top);
				data.CopyTo(array, num);
				num += data.Length;
				data = BitConverter.GetBytes(this.m_right);
				data.CopyTo(array, num);
				num += data.Length;
				data = BitConverter.GetBytes(this.m_bottom);
				data.CopyTo(array, num);
				return array;
			}
		}

		public sealed class Shape : EscherHeader
		{
			private uint m_spid;

			private ShapeFlag m_shapeFlag;

			public override uint Length
			{
				get
				{
					return 8u;
				}
			}

			public Shape(ShapeType shapeType, ShapeFlag shapeFlags, uint spid)
				: base(2, (uint)shapeType, EscherType.MSOFBTSP, 8u)
			{
				this.m_spid = spid;
				this.m_shapeFlag = shapeFlags;
			}

			public override byte[] GetData()
			{
				byte[] array = new byte[16];
				int num = 0;
				byte[] data = base.GetData();
				data.CopyTo(array, num);
				num += data.Length;
				data = BitConverter.GetBytes(this.m_spid);
				data.CopyTo(array, num);
				num += data.Length;
				data = BitConverter.GetBytes((uint)this.m_shapeFlag);
				data.CopyTo(array, num);
				return array;
			}
		}

		public sealed class DrawingOpt : EscherHeader
		{
			private const ushort PropertyIDShapeCount = 16644;

			private const ushort PropertyIDImageName = 49413;

			private const ushort PropertyPihlShape = 50050;

			private const ushort BookMarkLength = 48;

			private const ushort HyperlinkLength = 64;

			private const int RecordLength = 14;

			private string m_imageName;

			private uint m_referenceIndex;

			private string m_hyperLinkName;

			private HyperlinkType m_hyperLinkType;

			public DrawingOpt(string imageName, uint refIndex)
				: base(3, 2u, EscherType.MSOFBTOPT, 0u)
			{
				this.m_imageName = imageName;
				this.m_referenceIndex = refIndex;
			}

			public DrawingOpt(string imageName, uint refIndex, string hyperLinkName, HyperlinkType hyperLinkType)
				: base(3, 5u, EscherType.MSOFBTOPT, 0u)
			{
				this.m_imageName = imageName;
				this.m_referenceIndex = refIndex;
				this.m_hyperLinkName = hyperLinkName;
				this.m_hyperLinkType = hyperLinkType;
			}

			public override byte[] GetData()
			{
				int num = 14 + this.m_imageName.Length * 2;
				if (this.m_hyperLinkName != null && this.m_hyperLinkName.Length > 0)
				{
					num = ((this.m_hyperLinkType != HyperlinkType.BOOKMARK) ? (num + (this.m_hyperLinkName.Length * 2 + 64)) : (num + (this.m_hyperLinkName.Length * 2 + 48)));
				}
				this.Length = (uint)num;
				byte[] array = new byte[num + 8];
				int num2 = 0;
				byte[] data = base.GetData();
				data.CopyTo(array, num2);
				num2 += data.Length;
				data = BitConverter.GetBytes((ushort)16644);
				data.CopyTo(array, num2);
				num2 += data.Length;
				data = BitConverter.GetBytes(this.m_referenceIndex);
				data.CopyTo(array, num2);
				num2 += data.Length;
				data = BitConverter.GetBytes((ushort)49413);
				data.CopyTo(array, num2);
				num2 += data.Length;
				uint value = (uint)(this.m_imageName.Length * 2 + 2);
				data = BitConverter.GetBytes(value);
				data.CopyTo(array, num2);
				num2 += data.Length;
				if (this.m_hyperLinkName != null && this.m_hyperLinkName.Length > 0)
				{
					byte[] array2 = new byte[6]
					{
						191,
						1,
						1,
						0,
						1,
						0
					};
					byte[] array3 = new byte[6]
					{
						191,
						3,
						8,
						0,
						8,
						0
					};
					array2.CopyTo(array, num2);
					num2 += 6;
					data = BitConverter.GetBytes((ushort)50050);
					data.CopyTo(array, num2);
					num2 += data.Length;
					uint value2 = (uint)((this.m_hyperLinkType != HyperlinkType.BOOKMARK) ? (44 + (this.m_hyperLinkName.Length + 1) * 2) : (28 + (this.m_hyperLinkName.Length + 1) * 2));
					data = BitConverter.GetBytes(value2);
					data.CopyTo(array, num2);
					num2 += data.Length;
					array3.CopyTo(array, num2);
					num2 += 6;
				}
				UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
				data = unicodeEncoding.GetBytes(this.m_imageName);
				data.CopyTo(array, num2);
				num2 += data.Length + 2;
				if (this.m_hyperLinkName != null && this.m_hyperLinkName.Length > 0)
				{
					data = new Guid("79EAC9D0-BAF9-11CE-8C82-00AA004BA90B").ToByteArray();
					data.CopyTo(array, num2);
					num2 += data.Length;
					uint value3;
					if (this.m_hyperLinkType == HyperlinkType.BOOKMARK)
					{
						value3 = (uint)(this.m_hyperLinkName.Length + 1);
						uint value4 = 2u;
						uint value5 = 8u;
						data = BitConverter.GetBytes(value4);
						data.CopyTo(array, num2);
						num2 += data.Length;
						data = BitConverter.GetBytes(value5);
						data.CopyTo(array, num2);
						num2 += data.Length;
					}
					else
					{
						value3 = (uint)((this.m_hyperLinkName.Length + 1) * 2);
						uint value6 = 2u;
						uint value7 = 3u;
						data = BitConverter.GetBytes(value6);
						data.CopyTo(array, num2);
						num2 += data.Length;
						data = BitConverter.GetBytes(value7);
						data.CopyTo(array, num2);
						num2 += data.Length;
						data = new Guid("79EAC9E0-BAF9-11CE-8C82-00AA004BA90B").ToByteArray();
						data.CopyTo(array, num2);
						num2 += data.Length;
					}
					data = BitConverter.GetBytes(value3);
					data.CopyTo(array, num2);
					num2 += data.Length;
					data = unicodeEncoding.GetBytes(this.m_hyperLinkName);
					data.CopyTo(array, num2);
				}
				return array;
			}
		}

		public sealed class ClientAnchor : EscherHeader
		{
			public sealed class SPRC
			{
				public sealed class ORC
				{
					public ushort m_colL;

					public short m_dxL;

					public ushort m_rwT;

					public short m_dyT;

					public ushort m_colR;

					public short m_dxR;

					public ushort m_rwB;

					public short m_dyB;
				}

				public ushort wFlags;

				public ORC m_orc;

				public SPRC(ushort leftTopColumn, short leftTopOffset, ushort topLeftRow, short topLeftOffset, ushort rightBottomColumn, short rightBottomOffset, ushort bottomRightRow, short bottomRightOffset)
				{
					this.m_orc = new ORC();
					this.m_orc.m_colL = leftTopColumn;
					this.m_orc.m_dxL = leftTopOffset;
					this.m_orc.m_rwT = topLeftRow;
					this.m_orc.m_dyT = topLeftOffset;
					this.m_orc.m_colR = rightBottomColumn;
					this.m_orc.m_dxR = rightBottomOffset;
					this.m_orc.m_rwB = bottomRightRow;
					this.m_orc.m_dyB = bottomRightOffset;
				}
			}

			private const uint RecordLength = 18u;

			private SPRC m_sprc;

			public override uint Length
			{
				get
				{
					return 18u;
				}
			}

			public ClientAnchor(SPRC clientAnchorInfo)
				: base(0, 0u, EscherType.MSOFBTCLIENTANCHOR, 18u)
			{
				this.m_sprc = clientAnchorInfo;
			}

			public override byte[] GetData()
			{
				byte[] array = new byte[26];
				int num = 0;
				byte[] data = base.GetData();
				data.CopyTo(array, num);
				num += data.Length;
				data = BitConverter.GetBytes(this.m_sprc.wFlags);
				data.CopyTo(array, num);
				num += data.Length;
				data = BitConverter.GetBytes(this.m_sprc.m_orc.m_colL);
				data.CopyTo(array, num);
				num += data.Length;
				data = BitConverter.GetBytes(this.m_sprc.m_orc.m_dxL);
				data.CopyTo(array, num);
				num += data.Length;
				data = BitConverter.GetBytes(this.m_sprc.m_orc.m_rwT);
				data.CopyTo(array, num);
				num += data.Length;
				data = BitConverter.GetBytes(this.m_sprc.m_orc.m_dyT);
				data.CopyTo(array, num);
				num += data.Length;
				data = BitConverter.GetBytes(this.m_sprc.m_orc.m_colR);
				data.CopyTo(array, num);
				num += data.Length;
				data = BitConverter.GetBytes(this.m_sprc.m_orc.m_dxR);
				data.CopyTo(array, num);
				num += data.Length;
				data = BitConverter.GetBytes(this.m_sprc.m_orc.m_rwB);
				data.CopyTo(array, num);
				num += data.Length;
				data = BitConverter.GetBytes(this.m_sprc.m_orc.m_dyB);
				data.CopyTo(array, num);
				return array;
			}
		}

		public sealed class ClientData : EscherHeader
		{
			public ClientData()
				: base(0, 0u, EscherType.MSOFBTCLIENTDATA, 0u)
			{
			}
		}

		public enum EscherType : ushort
		{
			MSOFBTUNKNOWN,
			MSOFBTDGGCONTAINER = 61440,
			MSOFBTDGG = 61446,
			MSOFBTCLSID = 61462,
			MSOFBTOPT = 61451,
			MSOFBTBSTORECONTAINER = 61441,
			MSOFBTBSE = 61447,
			MSOFBTBLIP = 61464,
			MSOFBTBLIP_JPEG = 61469,
			MSOFBTBLIP_GIF,
			MSOFBTBLIP_PNG = 61470,
			MSOFBTBLIP_DIB,
			MSOFBTDGCONTAINER = 61442,
			MSOFBTDG = 61448,
			MSOFBTSPGRCONTAINER = 61443,
			MSOFBTSPCONTAINER,
			MSOFBTSPGR = 61449,
			MSOFBTSP,
			MSOFBTCLIENTANCHOR = 61456,
			MSOFBTCLIENTDATA
		}

		public enum BlipType
		{
			MSOBLIPERROR,
			MSOBLIPUNKNOWN,
			MSOBLIPEMF,
			MSOBLIPWMF,
			MSOBLIPPICT,
			MSOBLIPJPEG,
			MSOBLIPPNG,
			MSOBLIPDIB,
			MSOBLITIFF = 17,
			MSOBLIPCMYKJPEG,
			MSLBLIPFIRSTCLIENT = 0x20,
			MSLBLIPLASTCLIENT = 0xFF
		}

		public enum BlipUsage
		{
			MSOBLIPUSAGEDEFAULT,
			MSOBLIPUSAGETEXTURE,
			MSOBLIPUSAGEMAX = 0xFF
		}

		public enum BlipSignature
		{
			MSOBIUNKNOWN,
			MSOBIWMF = 534,
			MSOBIEMF = 980,
			MSOBIPICT = 1346,
			MSOBIPNG = 1760,
			MSOBIJFIF = 1130,
			MSOBIJPEG = 1130,
			MSOBIDIB = 1960,
			MSOBICMYKJPEG = 1762,
			MSOBITIFF = 1764,
			MSOBICLIENT = 0x800
		}

		public enum ShapeType
		{
			MSOSPTMIN,
			MSOSPTPICTUREFRAME = 75
		}

		public enum ShapeFlag
		{
			NONE,
			GROUP,
			CHILD,
			PATRIARCH = 4,
			DELETED = 8,
			OLESHAPE = 0x10,
			HAVEMASTER = 0x20,
			FLIPH = 0x40,
			FLIPV = 0x80,
			CONNECTOR = 0x100,
			HAVEANCHOR = 0x200,
			BACKGROUND = 0x400,
			HAVESPT = 0x800
		}

		public enum HyperlinkType
		{
			URL,
			LOCALFILE,
			UNC,
			BOOKMARK
		}

		public const ushort RecordHeaderLength = 8;

		private const int ClusterSize = 1024;

		private const ushort ContainerVersion = 15;

		public static byte[] CheckSum(Stream imageBits)
		{
			if (imageBits == null)
			{
				return null;
			}
			imageBits.Position = 0L;
			OfficeImageHasher officeImageHasher = new OfficeImageHasher(imageBits);
			return officeImageHasher.Hash;
		}
	}
}

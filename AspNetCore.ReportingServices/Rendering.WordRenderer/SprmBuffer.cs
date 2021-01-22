using AspNetCore.ReportingServices.Rendering.Utilities;
using System;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer
{
	public class SprmBuffer : ICloneable
	{
		private byte[] m_grpprl;

		private int m_offset;

		private int m_initialOffset;

		public byte[] Buf
		{
			get
			{
				return this.m_grpprl;
			}
		}

		public int Offset
		{
			get
			{
				return this.m_offset;
			}
		}

		public short StyleIndex
		{
			get
			{
				return LittleEndian.getShort(this.m_grpprl, 0);
			}
			set
			{
				LittleEndian.PutShort(this.m_grpprl, value);
			}
		}

		public SprmBuffer(int initialSize, int initialOffset)
		{
			this.m_grpprl = new byte[initialSize];
			this.m_offset = initialOffset;
			this.m_initialOffset = initialOffset;
		}

		public void AddSprm(ushort instruction, int param, byte[] varParam)
		{
			int num = (instruction & 0xE000) >> 13;
			byte[] array = null;
			switch (num)
			{
			case 0:
			case 1:
				array = new byte[3]
				{
					0,
					0,
					(byte)param
				};
				break;
			case 2:
				array = new byte[4];
				LittleEndian.PutUShort(array, 2, (ushort)param);
				break;
			case 3:
				array = new byte[6];
				LittleEndian.PutInt(array, 2, param);
				break;
			case 4:
			case 5:
				array = new byte[4];
				LittleEndian.PutUShort(array, 2, (ushort)param);
				break;
			case 6:
				array = new byte[3 + varParam.Length];
				array[2] = (byte)varParam.Length;
				Array.Copy(varParam, 0, array, 3, varParam.Length);
				break;
			case 7:
			{
				array = new byte[5];
				byte[] array2 = new byte[4];
				LittleEndian.PutInt(array2, 0, param);
				Array.Copy(array2, 0, array, 2, 3);
				break;
			}
			}
			LittleEndian.PutUShort(array, 0, instruction);
			Array.Copy(array, 0, this.m_grpprl, this.m_offset, array.Length);
			this.m_offset += array.Length;
		}

		public void AddRawSprmData(byte[] buf)
		{
			Array.Copy(buf, 0, this.m_grpprl, this.m_offset, buf.Length);
			this.m_offset += buf.Length;
		}

		public void Clear(int start, int length)
		{
			Array.Clear(this.m_grpprl, start, length);
		}

		public void Reset(int offset)
		{
			this.m_offset = offset;
		}

		public void Reset()
		{
			this.Reset(this.m_initialOffset);
		}

		public void ClearStyle()
		{
			Array.Clear(this.m_grpprl, 0, 2);
		}

		public object Clone()
		{
			SprmBuffer sprmBuffer = new SprmBuffer(this.m_grpprl.Length, this.m_initialOffset);
			sprmBuffer.m_offset = this.m_offset;
			Array.Copy(this.m_grpprl, sprmBuffer.m_grpprl, this.m_offset);
			return sprmBuffer;
		}
	}
}

using System;
using System.Data.SqlTypes;
using System.IO;

namespace AspNetCore.Reporting.Map.WebForms
{
	public class SqlBytesReader
	{
		private SqlBytes _data;

		private int _position;

		private int _length;

		private byte[] _shortBinData = new byte[8];

		public int Position
		{
			get
			{
				return this._position;
			}
		}

		public int Length
		{
			get
			{
				return this._length;
			}
		}

		public bool EOF
		{
			get
			{
				return this._position == this._length;
			}
		}

		public SqlBytesReader(SqlBytes data)
		{
			this._data = data;
			this._length = (int)this._data.Length;
		}

		public byte ReadByte()
		{
			if (this.EOF)
			{
				throw new EndOfStreamException();
			}
			return this._data[this._position++];
		}

		public int ReadInt32()
		{
			if (this._position + 4 > this._length)
			{
				throw new EndOfStreamException();
			}
			this._position += (int)this._data.Read(this._position, this._shortBinData, 0, 4);
			return BitConverter.ToInt32(this._shortBinData, 0);
		}

		public double ReadDouble()
		{
			if (this._position + 8 > this._length)
			{
				throw new EndOfStreamException();
			}
			this._position += (int)this._data.Read(this._position, this._shortBinData, 0, 8);
			return BitConverter.ToDouble(this._shortBinData, 0);
		}

		public ushort ReadUInt16()
		{
			if (this._position + 2 > this._length)
			{
				throw new EndOfStreamException();
			}
			this._position += (int)this._data.Read(this._position, this._shortBinData, 0, 2);
			return BitConverter.ToUInt16(this._shortBinData, 0);
		}

		public uint ReadUInt32()
		{
			if (this._position + 4 > this._length)
			{
				throw new EndOfStreamException();
			}
			this._position += (int)this._data.Read(this._position, this._shortBinData, 0, 4);
			return BitConverter.ToUInt32(this._shortBinData, 0);
		}

		public char ReadChar()
		{
			if (this.EOF)
			{
				throw new EndOfStreamException();
			}
			return (char)this._data[this._position++];
		}

		public char[] ReadChars(int count)
		{
			if (this.EOF)
			{
				throw new EndOfStreamException();
			}
			if (this._position + count > this._length)
			{
				count = this._length - this._position;
			}
			char[] array = new char[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = (char)this._data[this._position++];
			}
			return array;
		}
	}
}

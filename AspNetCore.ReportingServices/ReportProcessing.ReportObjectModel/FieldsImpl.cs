using AspNetCore.ReportingServices.Common;
using AspNetCore.ReportingServices.Diagnostics.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel
{
	public sealed class FieldsImpl : Fields
	{
		public const string Name = "Fields";

		private Hashtable m_nameMap;

		private bool[] m_fieldMissing;

		private bool[] m_fieldError;

		private FieldImpl[] m_collection;

		private int m_count;

		private bool m_referenced;

		private bool m_readerExtensionsSupported;

		private bool m_readerFieldProperties;

		private bool m_isAggregateRow;

		private int m_aggregationFieldCount;

		private int m_aggregationFieldCountForDetailRow;

		private bool m_noRows;

		private bool m_validAggregateRow;

		private bool m_addRowIndex;

		public override Field this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ReportProcessingException_NonExistingFieldReference();
				}
				this.ValidateFieldCollection();
				try
				{
					FieldImpl fieldImpl = this.m_collection[(int)this.m_nameMap[key]];
					fieldImpl.UsedInExpression = true;
					return fieldImpl;
				}
				catch (RSException)
				{
					throw;
				}
				catch (Exception e)
				{
					if (AsynchronousExceptionDetection.IsStoppingException(e))
					{
						throw;
					}
					throw new ReportProcessingException_NonExistingFieldReference();
				}
			}
		}

		public FieldImpl this[int index]
		{
			get
			{
				this.ValidateFieldCollection();
				try
				{
					FieldImpl fieldImpl = this.m_collection[index];
					fieldImpl.UsedInExpression = true;
					return fieldImpl;
				}
				catch (RSException)
				{
					throw;
				}
				catch (Exception e)
				{
					if (AsynchronousExceptionDetection.IsStoppingException(e))
					{
						throw;
					}
					throw new ReportProcessingException_NonExistingFieldReference();
				}
			}
			set
			{
				Global.Tracer.Assert(null != this.m_collection);
				this.m_collection[index] = value;
			}
		}

		public int Count
		{
			get
			{
				return this.m_count - (this.m_addRowIndex ? 1 : 0);
			}
		}

		public int CountWithRowIndex
		{
			get
			{
				return this.m_count;
			}
		}

		public bool ReaderExtensionsSupported
		{
			get
			{
				return this.m_readerExtensionsSupported;
			}
			set
			{
				this.m_readerExtensionsSupported = value;
			}
		}

		public bool ReaderFieldProperties
		{
			get
			{
				return this.m_readerFieldProperties;
			}
			set
			{
				this.m_readerFieldProperties = value;
			}
		}

		public bool IsAggregateRow
		{
			get
			{
				return this.m_isAggregateRow;
			}
			set
			{
				this.m_isAggregateRow = value;
			}
		}

		public int AggregationFieldCount
		{
			get
			{
				return this.m_aggregationFieldCount;
			}
			set
			{
				this.m_aggregationFieldCount = value;
			}
		}

		public int AggregationFieldCountForDetailRow
		{
			set
			{
				this.m_aggregationFieldCountForDetailRow = value;
			}
		}

		public bool ValidAggregateRow
		{
			get
			{
				return this.m_validAggregateRow;
			}
			set
			{
				this.m_validAggregateRow = value;
			}
		}

		public bool AddRowIndex
		{
			get
			{
				return this.m_addRowIndex;
			}
		}

		public FieldsImpl(int size, bool addRowIndex)
		{
			if (addRowIndex)
			{
				this.m_collection = new FieldImpl[size + 1];
			}
			else
			{
				this.m_collection = new FieldImpl[size];
			}
			this.m_nameMap = new Hashtable(size);
			this.m_fieldMissing = null;
			this.m_count = 0;
			this.m_referenced = false;
			this.m_readerExtensionsSupported = false;
			this.m_isAggregateRow = false;
			this.m_aggregationFieldCount = size;
			this.m_aggregationFieldCountForDetailRow = size;
			this.m_noRows = true;
			this.m_validAggregateRow = true;
			this.m_addRowIndex = addRowIndex;
		}

		public FieldsImpl()
		{
			this.m_collection = null;
			this.m_nameMap = null;
			this.m_fieldMissing = null;
			this.m_count = 0;
			this.m_referenced = false;
			this.m_readerExtensionsSupported = false;
			this.m_isAggregateRow = false;
			this.m_aggregationFieldCount = 0;
			this.m_aggregationFieldCountForDetailRow = 0;
			this.m_noRows = true;
			this.m_validAggregateRow = true;
			this.m_addRowIndex = false;
		}

		public void Add(string name, FieldImpl field)
		{
			Global.Tracer.Assert(null != this.m_collection, "(null != m_collection)");
			Global.Tracer.Assert(null != this.m_nameMap, "(null != m_nameMap)");
			Global.Tracer.Assert(this.m_count < this.m_collection.Length, "(m_count < m_collection.Length)");
			this.m_nameMap.Add(name, this.m_count);
			this.m_collection[this.m_count] = field;
			this.m_count++;
		}

		public void AddRowIndexField()
		{
			Global.Tracer.Assert(null != this.m_collection, "(null != m_collection)");
			Global.Tracer.Assert(this.m_count < this.m_collection.Length, "(m_count < m_collection.Length)");
			this.m_collection[this.m_count] = null;
			this.m_count++;
		}

		public void SetFieldIsMissing(int index)
		{
			if (this.m_fieldMissing == null)
			{
				this.m_fieldMissing = new bool[this.m_collection.Length];
			}
			this.m_fieldMissing[index] = true;
		}

		public bool IsFieldMissing(int index)
		{
			if (this.m_fieldMissing == null)
			{
				return false;
			}
			return this.m_fieldMissing[index];
		}

		public void SetFieldErrorRegistered(int index)
		{
			if (this.m_fieldError == null)
			{
				this.m_fieldError = new bool[this.m_collection.Length];
			}
			this.m_fieldError[index] = true;
		}

		public bool IsFieldErrorRegistered(int index)
		{
			if (this.m_fieldError == null)
			{
				return false;
			}
			return this.m_fieldError[index];
		}

		public void NewRow()
		{
			this.m_noRows = false;
			if (this.m_referenced)
			{
				this.m_collection = new FieldImpl[this.m_count];
				this.m_referenced = false;
			}
		}

		public void SetRowIndex(int rowIndex)
		{
			Global.Tracer.Assert(this.m_addRowIndex, "(m_addRowIndex)");
			Global.Tracer.Assert(this.m_count > 0, "(m_count > 0)");
			this.m_collection[this.m_count - 1] = new FieldImpl(rowIndex, false, null);
		}

		public void SetFields(FieldImpl[] fields)
		{
			this.NewRow();
			Global.Tracer.Assert(null != this.m_collection, "(null != m_collection)");
			if (fields == null)
			{
				for (int i = 0; i < this.m_count; i++)
				{
					FieldImpl fieldImpl = this.m_collection[i];
					AspNetCore.ReportingServices.ReportProcessing.Field fieldDef = (fieldImpl == null) ? null : fieldImpl.FieldDef;
					this.m_collection[i] = new FieldImpl(null, false, fieldDef);
				}
			}
			else
			{
				Global.Tracer.Assert(fields.Length == this.m_count, "(fields.Length == m_count)");
				for (int j = 0; j < this.m_count; j++)
				{
					this.m_collection[j] = fields[j];
				}
				this.m_isAggregateRow = false;
				this.m_aggregationFieldCount = this.m_aggregationFieldCountForDetailRow;
			}
		}

		public void SetFields(FieldImpl[] fields, bool isAggregateRow, int aggregationFieldCount, bool validAggregateRow)
		{
			this.SetFields(fields);
			this.m_isAggregateRow = isAggregateRow;
			this.m_aggregationFieldCount = aggregationFieldCount;
			this.m_validAggregateRow = validAggregateRow;
		}

		public FieldImpl[] GetAndSaveFields()
		{
			Global.Tracer.Assert(null != this.m_collection, "(null != m_collection)");
			this.m_referenced = true;
			return this.m_collection;
		}

		public FieldImpl[] GetFields()
		{
			Global.Tracer.Assert(null != this.m_collection, "(null != m_collection)");
			return this.m_collection;
		}

		public int GetRowIndex()
		{
			Global.Tracer.Assert(this.m_addRowIndex, "(m_addRowIndex)");
			Global.Tracer.Assert(this.m_count > 0, "(m_count > 0)");
			return (int)this.m_collection[this.m_count - 1].Value;
		}

		public void Clone(FieldsImpl fields)
		{
			if (fields != null)
			{
				this.m_collection = fields.m_collection;
				this.m_nameMap = fields.m_nameMap;
				this.m_count = fields.m_count;
				this.m_referenced = fields.m_referenced;
				this.m_noRows = fields.m_noRows;
				this.m_fieldMissing = fields.m_fieldMissing;
			}
		}

		private bool ValidateFieldCollection()
		{
			if (this.m_nameMap != null && this.m_collection != null)
			{
				if (this.m_noRows)
				{
					throw new ReportProcessingException_NoRowsFieldAccess();
				}
				return true;
			}
			throw new ReportProcessingException_NonExistingFieldReference();
		}

		public void ResetUsedInExpression()
		{
			if (this.m_collection != null)
			{
				for (int i = 0; i < this.m_collection.Length; i++)
				{
					this.m_collection[i].UsedInExpression = false;
				}
			}
		}

		public void AddFieldsUsedInExpression(List<string> fieldsUsedInValueExpression)
		{
			if (this.m_collection != null)
			{
				for (int i = 0; i < this.m_collection.Length; i++)
				{
					FieldImpl fieldImpl = this.m_collection[i];
					if (fieldImpl.UsedInExpression && fieldImpl.FieldDef != null)
					{
						fieldsUsedInValueExpression.Add(fieldImpl.FieldDef.DataField);
					}
				}
			}
		}
	}
}

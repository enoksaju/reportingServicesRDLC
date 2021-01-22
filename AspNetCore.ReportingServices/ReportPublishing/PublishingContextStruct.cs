using AspNetCore.ReportingServices.RdlExpressions;
using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.ReportPublishing
{
	public struct PublishingContextStruct
	{
		private LocationFlags m_location;

		private ObjectType m_objectType;

		private string m_objectName;

		private string m_prefixPropertyName;

		private int m_maxExpressionLength;

		private PublishingErrorContext m_errorContext;

		public LocationFlags Location
		{
			get
			{
				return this.m_location;
			}
			set
			{
				this.m_location = value;
			}
		}

		public ObjectType ObjectType
		{
			get
			{
				return this.m_objectType;
			}
			set
			{
				this.m_objectType = value;
			}
		}

		public string ObjectName
		{
			get
			{
				return this.m_objectName;
			}
			set
			{
				this.m_objectName = value;
			}
		}

		public string PrefixPropertyName
		{
			get
			{
				return this.m_prefixPropertyName;
			}
			set
			{
				this.m_prefixPropertyName = value;
			}
		}

		public PublishingErrorContext ErrorContext
		{
			get
			{
				return this.m_errorContext;
			}
			set
			{
				this.m_errorContext = value;
			}
		}

		public PublishingContextStruct(LocationFlags location, ObjectType objectType, int maxExpressionLength, PublishingErrorContext errorContext)
		{
			this.m_location = location;
			this.m_objectType = objectType;
			this.m_objectName = null;
			this.m_prefixPropertyName = null;
			this.m_maxExpressionLength = maxExpressionLength;
			this.m_errorContext = errorContext;
		}

		public AspNetCore.ReportingServices.RdlExpressions.ExpressionParser.ExpressionContext CreateExpressionContext(AspNetCore.ReportingServices.RdlExpressions.ExpressionParser.ExpressionType expressionType, DataType constantType, string propertyName, string dataSetName, PublishingContextBase publishingContext)
		{
			string propertyName2 = (!string.IsNullOrEmpty(propertyName)) ? ((!string.IsNullOrEmpty(this.m_prefixPropertyName)) ? (this.m_prefixPropertyName + propertyName) : propertyName) : this.m_prefixPropertyName;
			return new AspNetCore.ReportingServices.RdlExpressions.ExpressionParser.ExpressionContext(expressionType, constantType, this.m_location, this.m_objectType, this.m_objectName, propertyName2, dataSetName, this.m_maxExpressionLength, publishingContext);
		}

		public double ValidateSize(string size, string propertyName, ErrorContext errorContext)
		{
			double result = default(double);
			string text = default(string);
			PublishingValidator.ValidateSize(size, this.m_objectType, this.m_objectName, propertyName, true, errorContext, out result, out text);
			return result;
		}
	}
}

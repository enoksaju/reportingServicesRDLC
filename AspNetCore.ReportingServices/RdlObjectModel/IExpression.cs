using System.Collections.Generic;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public interface IExpression
	{
		object Value
		{
			get;
			set;
		}

		string Expression
		{
			get;
			set;
		}

		bool IsExpression
		{
			get;
		}

		void GetDependencies(IList<ReportObject> dependencies, ReportObject parent);
	}
}

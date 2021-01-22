using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AspNetCore.ReportingServices.Diagnostics
{
	public static class SkuUtil
	{
		public enum SkuVerificationErrorCode
		{
			Success,
			LocalConnectionRequired,
			DatabaseSkuRequired,
			DatabaseSkuRestricted
		}

		public delegate bool LocalDbServerVerifier(string dbServer);

		private static bool IsWorkgroupOrHigher(SkuType sku)
		{
			if (sku != SkuType.Workgroup)
			{
				return SkuUtil.IsWebOrHigher(sku);
			}
			return true;
		}

		private static bool IsWebOrHigher(SkuType sku)
		{
			if (sku != SkuType.Web)
			{
				return SkuUtil.IsStandardOrHigher(sku);
			}
			return true;
		}

		private static bool IsStandardOrHigher(SkuType sku)
		{
			if (sku != SkuType.Standard && sku != SkuType.SBS)
			{
				return SkuUtil.IsEnterpriseOrHigher(sku);
			}
			return true;
		}

		private static bool IsEnterpriseOrHigher(SkuType sku)
		{
			return SkuUtil.IsDataCenterOrHigher(sku);
		}

		private static bool IsDataCenterOrHigher(SkuType sku)
		{
			if (sku != SkuType.DataCenter && sku != SkuType.Developer && sku != SkuType.Evaluation && sku != SkuType.Enterprise && sku != SkuType.EnterpriseCore)
			{
				return sku == SkuType.BusinessIntelligence;
			}
			return true;
		}

		public static bool IsBusinessIntelligenceOrHigher(SkuType sku)
		{
			return SkuUtil.IsDataCenterOrHigher(sku);
		}

		public static SkuType GetSqlSku(SqlConnection sqlConn)
		{
			object obj = null;
			SqlCommand sqlCommand = new SqlCommand("SELECT SERVERPROPERTY('Edition')", sqlConn);
			sqlCommand.CommandType = CommandType.Text;
			obj = sqlCommand.ExecuteScalar();
			if (obj == null)
			{
				return SkuType.None;
			}
			string edition = (string)obj;
			return SkuUtil.SkuFromString(edition);
		}

		public static SkuVerificationErrorCode EnsureCorrectEdition(SkuType rsSku, SkuType sqlSku, string connectionString, LocalDbServerVerifier localDbServerVerifier, bool checkRestrictedSkus)
		{
			if (!SkuUtil.IsStandardOrHigher(rsSku) && !SkuUtil.IsLocal(connectionString, localDbServerVerifier))
			{
				return SkuVerificationErrorCode.LocalConnectionRequired;
			}
			List<SkuType> list = new List<SkuType>();
			List<SkuType> databaseSku = SkuUtil.GetDatabaseSku(rsSku, out list);
			if (databaseSku.Count > 0 && !databaseSku.Contains(sqlSku))
			{
				return SkuVerificationErrorCode.DatabaseSkuRequired;
			}
			if (checkRestrictedSkus && list.Count > 0 && list.Contains(sqlSku))
			{
				return SkuVerificationErrorCode.DatabaseSkuRestricted;
			}
			return SkuVerificationErrorCode.Success;
		}

		public static List<SkuType> GetDatabaseSku(SkuType reportServerSku, out List<SkuType> restrictedSkus)
		{
			List<SkuType> list = new List<SkuType>();
			restrictedSkus = new List<SkuType>();
			switch (reportServerSku)
			{
			case SkuType.Express:
				list.Add(SkuType.Express);
				break;
			case SkuType.Web:
				list.Add(SkuType.Web);
				break;
			case SkuType.Workgroup:
				list.Add(SkuType.Workgroup);
				break;
			case SkuType.Standard:
			case SkuType.Enterprise:
			case SkuType.DataCenter:
			case SkuType.BusinessIntelligence:
			case SkuType.EnterpriseCore:
				restrictedSkus.Add(SkuType.Developer);
				restrictedSkus.Add(SkuType.Evaluation);
				break;
			case SkuType.Developer:
				restrictedSkus.Add(SkuType.Evaluation);
				break;
			case SkuType.Evaluation:
				restrictedSkus.Add(SkuType.Developer);
				break;
			}
			if (SkuUtil.IsStandardOrHigher(reportServerSku))
			{
				restrictedSkus.Add(SkuType.Workgroup);
				restrictedSkus.Add(SkuType.Express);
				restrictedSkus.Add(SkuType.Web);
			}
			return list;
		}

		public static SkuType SkuFromString(string edition)
		{
			if (!edition.ToUpperInvariant().Contains("EVALUATION") && !edition.ToUpperInvariant().Contains("BETA"))
			{
				if (edition.ToUpperInvariant().Contains("CORE"))
				{
					return SkuType.EnterpriseCore;
				}
				if (edition.ToUpperInvariant().Contains("DEVELOPER"))
				{
					return SkuType.Developer;
				}
				if (edition.ToUpperInvariant().Contains("ENTERPRISE"))
				{
					return SkuType.Enterprise;
				}
				if (edition.ToUpperInvariant().Contains("STANDARD"))
				{
					return SkuType.Standard;
				}
				if (edition.ToUpperInvariant().Contains("WORKGROUP"))
				{
					return SkuType.Workgroup;
				}
				if (edition.ToUpperInvariant().Contains("EXPRESS"))
				{
					return SkuType.Express;
				}
				if (edition.ToUpperInvariant().Contains("WEB"))
				{
					return SkuType.Web;
				}
				if (!edition.ToUpperInvariant().Contains("DATA CENTER") && !edition.ToUpperInvariant().Contains("DATACENTER"))
				{
					if (!edition.ToUpperInvariant().Contains("BUSINESS INTELLIGENCE") && !edition.ToUpperInvariant().Contains("BUSINESSINTELLIGENCE"))
					{
						return SkuType.None;
					}
					return SkuType.BusinessIntelligence;
				}
				return SkuType.DataCenter;
			}
			return SkuType.Evaluation;
		}

		private static bool IsLocal(string sqlConnectionString, LocalDbServerVerifier localDbServerVerifier)
		{
			sqlConnectionString = sqlConnectionString.ToUpperInvariant();
			string[] array = new string[5]
			{
				"DATA SOURCE",
				"SERVER",
				"ADDRESS",
				"ADDR",
				"NETWORK ADDRESS"
			};
			foreach (string value in array)
			{
				int num = sqlConnectionString.IndexOf(value, StringComparison.Ordinal);
				if (num != -1)
				{
					int num2 = sqlConnectionString.IndexOf("=", num, StringComparison.Ordinal);
					if (num2 != -1 && num2 != sqlConnectionString.Length - 1)
					{
						int num3 = num2 + 1;
						int num4 = sqlConnectionString.IndexOf(";", num3, StringComparison.Ordinal);
						if (num4 != sqlConnectionString.Length - 1)
						{
							string sqlInstanceName = sqlConnectionString.Substring(num3, num4 - num3);
							string machineNameFromSqlInstanceName = SkuUtil.GetMachineNameFromSqlInstanceName(sqlInstanceName);
							return localDbServerVerifier(machineNameFromSqlInstanceName);
						}
					}
				}
			}
			return false;
		}

		public static string GetMachineNameFromSqlInstanceName(string sqlInstanceName)
		{
			string text = sqlInstanceName;
			int num = sqlInstanceName.IndexOf('\\');
			if (num != -1)
			{
				text = sqlInstanceName.Substring(0, num);
			}
			num = text.IndexOf(',');
			if (num != -1)
			{
				text = text.Substring(0, num);
			}
			return text;
		}

		public static bool IsFeatureEnabled(SkuType sku, RestrictedFeatures feature, out bool isFeatureExpected)
		{
			isFeatureExpected = true;
			switch (feature)
			{
			case RestrictedFeatures.CustomAuth:
				return true;
			case RestrictedFeatures.ReportBuilder:
				return SkuUtil.IsWorkgroupOrHigher(sku);
			case RestrictedFeatures.NoCpuThrottling:
			case RestrictedFeatures.NoMemoryThrottling:
				return SkuUtil.IsDataCenterOrHigher(sku);
			case RestrictedFeatures.ScaleOut:
			case RestrictedFeatures.DataDrivenSubscriptions:
			case RestrictedFeatures.DataAlerting:
			case RestrictedFeatures.Crescent:
			case RestrictedFeatures.KpiItems:
			case RestrictedFeatures.MobileReportItems:
			case RestrictedFeatures.Branding:
				return SkuUtil.IsEnterpriseOrHigher(sku);
			case RestrictedFeatures.NonSqlDataSources:
			case RestrictedFeatures.OtherSkuDatasources:
			case RestrictedFeatures.RemoteDataSources:
			case RestrictedFeatures.Caching:
			case RestrictedFeatures.ExecutionSnapshots:
			case RestrictedFeatures.History:
			case RestrictedFeatures.Delivery:
			case RestrictedFeatures.Scheduling:
			case RestrictedFeatures.Extensibility:
			case RestrictedFeatures.Sharepoint:
			case RestrictedFeatures.Subscriptions:
			case RestrictedFeatures.CustomRolesSecurity:
			case RestrictedFeatures.ModelItemSecurity:
			case RestrictedFeatures.DynamicDrillthrough:
			case RestrictedFeatures.EventGeneration:
			case RestrictedFeatures.ComponentLibrary:
			case RestrictedFeatures.SharedDataset:
			case RestrictedFeatures.PowerBI:
				return SkuUtil.IsStandardOrHigher(sku);
			default:
				isFeatureExpected = false;
				return false;
			}
		}

		public static bool IsFeatureEnabled(SkuType sku, RestrictedFeatures feature)
		{
			bool flag = default(bool);
			return SkuUtil.IsFeatureEnabled(sku, feature, out flag);
		}

		public static bool IsSUMRequired(SkuType sku)
		{
			bool result = false;
			switch (sku)
			{
			case SkuType.Standard:
			case SkuType.Developer:
			case SkuType.BusinessIntelligence:
				result = true;
				break;
			}
			return result;
		}

		public static void GetConcurrencyLimit(SkuType sku, out long maxPhysicalCpu, out long maxCores, out long minLogicalCpu)
		{
			minLogicalCpu = 0L;
			switch (sku)
			{
			case SkuType.Express:
				maxPhysicalCpu = 1L;
				maxCores = 4L;
				break;
			case SkuType.Web:
				maxPhysicalCpu = 4L;
				maxCores = 16L;
				break;
			case SkuType.Standard:
				maxPhysicalCpu = 4L;
				maxCores = 24L;
				break;
			case SkuType.Developer:
			case SkuType.Evaluation:
			case SkuType.BusinessIntelligence:
			case SkuType.EnterpriseCore:
				maxPhysicalCpu = 0L;
				maxCores = 0L;
				break;
			case SkuType.Enterprise:
				maxPhysicalCpu = 20L;
				maxCores = 20L;
				break;
			default:
				throw new ArgumentException("sku", sku.ToString());
			}
		}

		public static long GetMaxMemoryThresholdMB(SkuType sku)
		{
			long num;
			switch (sku)
			{
			case SkuType.Express:
			case SkuType.Workgroup:
				num = 4L;
				break;
			case SkuType.Standard:
			case SkuType.Web:
			case SkuType.SBS:
				num = 64L;
				break;
			default:
				throw new ArgumentException("sku", sku.ToString());
			}
			return num * 1024;
		}
	}
}

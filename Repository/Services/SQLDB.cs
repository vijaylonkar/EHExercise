using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Repository.Interfaces;

namespace Repository.Services
{
	public class SQLDB
	{
		private readonly string connectionString;
		protected readonly ILogger logger;
		protected int queryTimeout = 600;

		public SQLDB(ILogger logger)
		{
			this.logger = logger;
			this.connectionString = new Settings(this.logger).GetAppSettingsValue("ConnectionString", "");
		}

		public async Task<DataSet> ExecuteSP(String storedProcName, ParamArray ps)
		{
			return await Task.Run(() =>
			{
				DataSet dataset = new DataSet();
				using (var connection = new SqlConnection(this.connectionString))
				{
					using (SqlCommand cmd = connection.CreateCommand())
					{
						cmd.Parameters.Clear();
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.CommandText = storedProcName;
						foreach (SqlParameter parm in ps)
						{
							cmd.Parameters.Add(parm);
						}
						cmd.CommandTimeout = queryTimeout;

						using (SqlDataAdapter adapter = new SqlDataAdapter())
						{
							adapter.SelectCommand = cmd;
							adapter.Fill(dataset);
						}
					}
				}
				return dataset;
			});
		}
		public async Task<int> ExecuteSP_NonQuery(String strSPName, ParamArray ps)
		{
			return await Task.Run(() =>
			{
				Int32 ReturnValue = 0;
				Int32 nRowCount = 0;
				using (SqlConnection connection = new SqlConnection(this.connectionString))
				{
					connection.Open();
					using (SqlCommand cmd = connection.CreateCommand())
					{
						cmd.Parameters.Clear();
						cmd.CommandText = strSPName;
						cmd.CommandType = CommandType.StoredProcedure;
						foreach (SqlParameter parm in ps)
						{
							cmd.Parameters.Add(parm);
						}
						SqlParameter retval = cmd.Parameters.Add("return_value", SqlDbType.Int);
						retval.Direction = ParameterDirection.ReturnValue;

						cmd.CommandTimeout = queryTimeout;
						nRowCount = cmd.ExecuteNonQuery();
						if (cmd.Parameters["return_value"].Value != System.DBNull.Value)
							ReturnValue = Convert.ToInt32(cmd.Parameters["return_value"].Value);
					}
				}
				return ReturnValue;
			});
		}

	}
}

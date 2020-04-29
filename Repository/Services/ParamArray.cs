using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Repository.Services
{
	public class ParamArray : List<SqlParameter>
	{
		public void AddParam(string paramName, SqlDbType sqlDbType, object value)
		{
			SqlParameter param = new SqlParameter(paramName, sqlDbType);
			param.Value = value;
			this.Add(param);
		}


		public SqlParameter AddParam(string paramName, SqlDbType sqlDbType, object value, ParameterDirection paramDirection)
		{
			SqlParameter param = new SqlParameter(paramName, sqlDbType);
			param.Value = value;
			param.Direction = paramDirection;
			this.Add(param);

			return param;
		}
	}

}

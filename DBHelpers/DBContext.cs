using Microsoft.Data.SqlClient;
using RCMS.IRepositories;
using RCMS.ModelHelpers;
using System.Data;

namespace RCMS.DBHelpers
{
    public class DBContext : IDBContextRepository
    {
        // 🔹 Execute INSERT, UPDATE, DELETE
        public async Task<CommonModel> ExecuteNonQuery_SP(string procedureName, SqlParameter[] parameters, string connectionString)
        {
            CommonModel commonModel = new CommonModel();
            try
            {
                using SqlConnection con = new SqlConnection(connectionString);
                using SqlCommand cmd = new SqlCommand(procedureName, con);

                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                await con.OpenAsync();
                int i = await cmd.ExecuteNonQueryAsync();

                if (i > 0)
                {
                    commonModel.Code = "100";
                    commonModel.Status = i.ToString();
                    commonModel.Msg = "Success";
                }
                else
                {
                    commonModel.Code = "101";
                    commonModel.Status = "0";
                    commonModel.Msg = "Fail";
                }
            }
            catch (Exception ex)
            {
                commonModel.Code = "102";
                commonModel.Status = "-1";
                commonModel.Msg = ex.Message;
            }
            return commonModel;
        }

        // 🔹 Execute SELECT (Multiple Rows)
        public async Task<CommonModel> ExecuteQueryDT_SP(string procedureName, SqlParameter[] parameters, string connectionString)
        {
            CommonModel commonModel = new CommonModel();
            try
            {
                using SqlConnection con = new SqlConnection(connectionString);
                using SqlCommand cmd = new SqlCommand(procedureName, con);

                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                await con.OpenAsync();

                using SqlDataReader reader = await cmd.ExecuteReaderAsync();

                //using SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                //da.Fill(dt);
                //dt.Load(reader);
                if (reader.HasRows)
                {
                    dt.Load(reader);
                    commonModel.Code = "100";
                    commonModel.Status = "1";
                    commonModel.DTDetails = dt;
                    commonModel.Msg = "Success";
                }
                else
                {
                    // optional: return empty table or null
                    commonModel.Code = "101";
                    commonModel.Status = "0";
                    commonModel.DTDetails = new DataTable();
                    commonModel.Msg = "Fail";
                }
                // return dt;
            }
            catch (Exception ex)
            {
                //throw new Exception("Database error", ex);
                commonModel.Code = "102";
                commonModel.Status = "-1";
                commonModel.DTDetails = new DataTable();
                commonModel.Msg = ex.Message;
            }
            return commonModel;

        }

        // 🔹 Execute SELECT (Single Row)
        public async Task<CommonModel> ExecuteSingleRowDT_SP(string procedureName, SqlParameter[] parameters, string connectionString)
        {
            CommonModel commonModel = new CommonModel();
            try
            {
                CommonModel data = await ExecuteQueryDT_SP(procedureName, parameters, connectionString);
                if (data.Code=="100" && data.DTDetails != null && data.DTDetails.Rows.Count > 0)
                {
                    commonModel.Code = "100";
                    commonModel.Status = "1";
                    commonModel.DTRowDetails = data.DTDetails.Rows[0];
                    commonModel.Msg = "Success";
                }
                else
                {
                    commonModel.Code = "101";
                    commonModel.Status = "0";
                    commonModel.Msg = "Fail";
                }
                //return dt.Rows.Count > 0 ? dt.Rows[0] : null;
            }
            catch (Exception ex)
            {
                //throw new Exception("Database error", ex);
                commonModel.Code = "102";
                commonModel.Status = "-1";
                // commonModel.DTDetails = new DataTable();
                commonModel.Msg = ex.Message;
            }
            return commonModel;
        }

        // 🔹 Execute Scalar (Single Value)
        public async Task<CommonModel> ExecuteScalarSingleval_SP(string procedureName, SqlParameter[] parameters, string connectionString)
        {
            CommonModel commonModel = new CommonModel();
            try
            {
                using SqlConnection con = new SqlConnection(connectionString);
                using SqlCommand cmd = new SqlCommand(procedureName, con);

                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                await con.OpenAsync();
                object? result = await cmd.ExecuteScalarAsync();

                if (result != null && result != DBNull.Value)
                {
                    //int count = Convert.ToInt32(result); // or string/date/etc.
                    commonModel.Code = "100";
                    commonModel.Status = "1";
                    //commonModel.SingleResult = result;
                    commonModel.Msg = "Success";
                }
                else
                {
                    commonModel.Code = "101";
                    commonModel.Status = "0";
                    commonModel.Msg = "Fail";
                }
                //return await cmd.ExecuteScalarAsync() ?? throw new Exception("");
            }
            catch (Exception ex)
            {
                //throw new Exception("Database error", ex);
                commonModel.Code = "102";
                commonModel.Status = "-1";
                commonModel.Msg = ex.Message;
            }
            return commonModel;
        }

        // 🔹 Execute INSERT, UPDATE, DELETE
        public async Task<CommonModel> ExecuteNonQuery_Query(string QueryString, SqlParameter[] parameters, string connectionString)
        {
            CommonModel commonModel = new CommonModel();
            try
            {
                using SqlConnection con = new SqlConnection(connectionString);
                using SqlCommand cmd = new SqlCommand(QueryString, con);

                cmd.CommandType = CommandType.Text;

                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                await con.OpenAsync();
                int i = await cmd.ExecuteNonQueryAsync();

                if (i > 0)
                {
                    commonModel.Code = "100";
                    commonModel.Status = i.ToString();
                    commonModel.Msg = "Success";
                }
                else
                {
                    commonModel.Code = "101";
                    commonModel.Status = "0";
                    commonModel.Msg = "Fail";
                }
            }
            catch (Exception ex)
            {
                commonModel.Code = "102";
                commonModel.Status = "-1";
                commonModel.Msg = ex.Message;
            }

            return commonModel;
        }

        // 🔹 Execute SELECT (Multiple Rows)
        public async Task<CommonModel> ExecuteQueryDT_Query(string QueryString, SqlParameter[] parameters, string connectionString)
        {
            CommonModel commonModel = new CommonModel();
            try
            {
                using SqlConnection con = new SqlConnection(connectionString);
                using SqlCommand cmd = new SqlCommand(QueryString, con);

                cmd.CommandType = CommandType.Text;

                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                await con.OpenAsync();

                using SqlDataReader reader = await cmd.ExecuteReaderAsync();

                //using SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                //da.Fill(dt);
                //dt.Load(reader);
                if (reader.HasRows)
                {
                    dt.Load(reader);
                    commonModel.Code = "100";
                    commonModel.Status = "1";
                    commonModel.DTDetails = dt;
                    commonModel.Msg = "Success";
                }
                else
                {
                    // optional: return empty table or null
                    commonModel.Code = "101";
                    commonModel.Status = "0";
                    commonModel.DTDetails = new DataTable();
                    commonModel.Msg = "Fail";
                }
                // return dt;
            }
            catch (Exception ex)
            {
                //throw new Exception("Database error", ex);
                commonModel.Code = "102";
                commonModel.Status = "-1";
                commonModel.DTDetails = new DataTable();
                commonModel.Msg = ex.Message;
            }
            return commonModel;

        }

        // 🔹 Execute SELECT (Single Row)
        public async Task<CommonModel> ExecuteSingleRowDT_Query(string QueryString, SqlParameter[] parameters, string connectionString)
        {
            CommonModel commonModel = new CommonModel();
            try
            {
                CommonModel data = await ExecuteQueryDT_Query(QueryString, parameters, connectionString);
                if (data.Code=="100" && data.DTDetails != null && data.DTDetails.Rows.Count > 0)
                {
                    commonModel.Code = "100";
                    commonModel.Status = "1";
                    commonModel.DTRowDetails = data.DTDetails.Rows[0];
                    commonModel.Msg = "Success";
                }
                else
                {
                    commonModel.Code = "101";
                    commonModel.Status = "0";
                    commonModel.Msg = "Fail";
                }
                //return dt.Rows.Count > 0 ? dt.Rows[0] : null;
            }
            catch (Exception ex)
            {
                //throw new Exception("Database error", ex);
                commonModel.Code = "102";
                commonModel.Status = "-1";
                // commonModel.DTDetails = new DataTable();
                commonModel.Msg = ex.Message;
            }
            return commonModel;
        }

        // 🔹 Execute Scalar (Single Value)
        public async Task<CommonModel> ExecuteScalarSingleval_Query(string QueryString, SqlParameter[] parameters, string connectionString)
        {
            CommonModel commonModel = new CommonModel();
            try
            {
                using SqlConnection con = new SqlConnection(connectionString);
                using SqlCommand cmd = new SqlCommand(QueryString, con);

                cmd.CommandType = CommandType.Text;

                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                await con.OpenAsync();
                object? result = await cmd.ExecuteScalarAsync();

                if (result != null && result != DBNull.Value)
                {
                    //int count = Convert.ToInt32(result); // or string/date/etc.
                    commonModel.Code = "100";
                    commonModel.Status = "1";
                    //commonModel.SingleResult = result;
                    commonModel.Msg = "Success";
                }
                else
                {
                    commonModel.Code = "101";
                    commonModel.Status = "0";
                    commonModel.Msg = "Fail";
                }
                //return await cmd.ExecuteScalarAsync() ?? throw new Exception("");
            }
            catch (Exception ex)
            {
                //throw new Exception("Database error", ex);
                commonModel.Code = "102";
                commonModel.Status = "-1";
                commonModel.Msg = ex.Message;
            }
            return commonModel;
        }

        public SqlParameter CreateParameter(string name, object value, int size, SqlDbType? type)
        {
            var param = new SqlParameter(name, value ?? DBNull.Value);

            if (type.HasValue)
            {
                param.SqlDbType = type.Value;
                param.Size = size;
            }

            return param;
        }
    }
}

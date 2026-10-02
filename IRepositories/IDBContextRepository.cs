using Microsoft.Data.SqlClient;
using RCMS.ModelHelpers;
using System.Data;

namespace RCMS.IRepositories
{
    public interface IDBContextRepository
    {
        Task<CommonModel> ExecuteNonQuery_SP(string procedureName, SqlParameter[] parameters, string connectionString);
        Task<CommonModel> ExecuteQueryDT_SP(string procedureName, SqlParameter[] parameters, string connectionString);
        Task<CommonModel> ExecuteSingleRowDT_SP(string procedureName, SqlParameter[] parameters, string connectionString);
        Task<CommonModel> ExecuteScalarSingleval_SP(string procedureName, SqlParameter[] parameters, string connectionString);
        Task<CommonModel> ExecuteNonQuery_Query(string QueryString, SqlParameter[] parameters, string connectionString);
        Task<CommonModel> ExecuteQueryDT_Query(string QueryString, SqlParameter[] parameters, string connectionString);
        Task<CommonModel> ExecuteSingleRowDT_Query(string QueryString, SqlParameter[] parameters, string connectionString);
        Task<CommonModel> ExecuteScalarSingleval_Query(string QueryString, SqlParameter[] parameters, string connectionString);

        SqlParameter CreateParameter(string name, object value, int size, SqlDbType? type = null);
    }
}

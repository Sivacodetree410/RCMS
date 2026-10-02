using Microsoft.Data.SqlClient;
using RCMS.IRepositories;
using RCMS.ModelHelpers;
using static System.Net.Mime.MediaTypeNames;
using System.Data;

namespace RCMS.Repositories
{
    public class RegistrationsRepositories : IRegistrationsRepositories
    {
        private readonly IDBContextRepository _dbContext;
        private readonly ISqlDBConnectionRepository _connectionRepository;
        public RegistrationsRepositories(IDBContextRepository dbContext, ISqlDBConnectionRepository sqlDBConnectionRepository)
        {
            _dbContext = dbContext;
            _connectionRepository = sqlDBConnectionRepository;
        }

        public async Task<CommonModel> RCMSRegistrations(RegistrationModel registration)
        {
            SqlParameter[] parameters =
            {
                     _dbContext.CreateParameter("@Type", registration.Type,20, SqlDbType.VarChar),
                     _dbContext.CreateParameter("@FullName", registration.Input1,100, SqlDbType.NVarChar),
                     _dbContext.CreateParameter("@MobileNo",registration.Input2,10, SqlDbType.VarChar),
                     _dbContext.CreateParameter("@Email", registration.Input3,100, SqlDbType.VarChar),
                     _dbContext.CreateParameter("@Username", registration.Input4,100, SqlDbType.NVarChar),
                     _dbContext.CreateParameter("@PasswordHash",registration.Input5,500, SqlDbType.VarChar),
                     _dbContext.CreateParameter("@PasswordSalt", registration.Input6,500, SqlDbType.VarChar),
                     _dbContext.CreateParameter("@InputDatetime", DateTime.UtcNow,7, SqlDbType.DateTime2)
            };
            return await _dbContext.ExecuteNonQuery_SP("USP_USERS", parameters, _connectionRepository.GetRCMSDBConnection());
        }
    }
}

using RCMS.IRepositories;

namespace RCMS.DBHelpers
{
    public class SqlDBConnectionHelper : ISqlDBConnectionRepository
    {

        private readonly string _testDBconnection;
        private readonly string _DBDistrictswisemastersConn;
        private readonly string _DBRCMSConnection;

        public SqlDBConnectionHelper(IConfiguration config)
        {
            _testDBconnection = config.GetConnectionString("SqlDBTestDBConnection") ?? throw new Exception("connection string is missing in configuration");
            _DBDistrictswisemastersConn = config.GetConnectionString("DBDistrictswisemastersConn") ?? throw new Exception("connection string is missing in configuration");
            _DBRCMSConnection= config.GetConnectionString("DBRCMSConnection") ?? throw new Exception("connection string is missing in configuration");
        }

        public string GettestDBConnection()
        {
            return _testDBconnection;
        }

        public string getDBDistrictswisemastersConn()
        {
            return _DBDistrictswisemastersConn;
        }

        public string GetRCMSDBConnection()
        {
            return _DBRCMSConnection;
        }


    }
}

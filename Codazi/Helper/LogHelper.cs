using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using com.Xgensoftware.DAL;
using com.Xgensoftware.Core;

namespace Codazi.Helper
{
    public class LogHelper
    {
        private IDataProvider _dbProvider = null;
        private static LogHelper _instance;

        private LogHelper()
        {
            _dbProvider = DALFactory.CreateSqlProvider(DatabaseProvider_Type.MSSQLProvider,
                ConfigurationManager.AppSettings["CodaziDB"]);
        }

        private void LogMessage(string logType, string message)
        {
            List<SQLParam> sqlParms = new List<SQLParam>();
            sqlParms.Add(SQLParam.GetParam("@logType", logType));
            sqlParms.Add(SQLParam.GetParam("@logMessage", message));

            this._dbProvider.ExecuteNonQuery("sp_LogMessage", sqlParms);
        }

        public static LogHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LogHelper();
                }
                return _instance;
            }
        }

        public void LogInfoMesage(string msg)
        {
            LogMessage("INFO",msg);
        }

        public void LogErrorMessage(string msg)
        {
            LogMessage("ERROR", msg);
        }

        public void LogExecption(Exception ex)
        {
            LogMessage("ERROR", ex.Message);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using com.Xgensoftware.Core;
using com.Xgensoftware.DAL;
using Codazi.Helper;
namespace Codazi.Repository
{
    public class BaseRepository : IDisposable
    {
        protected IDataProvider _dataContext;
        protected List<SQLParam> _params;
        protected LogHelper _logging;


        public BaseRepository()
        {
            this._dataContext = DALFactory.CreateSqlProvider(DatabaseProvider_Type.MSSQLProvider,
                ConfigurationManager.AppSettings["CodaziDB"]);
            
        }

        #region " Methods "
        //public virtual List<T> FillCollection<T>(CommandType commandType, string spName, List<SQLParam> parms)
        //{
        //    ConcurrentBag<T> collection = new ConcurrentBag<T>();
        //    DataTable dtCollection = null;

        //    dtCollection = this._dataContext.GetData(commandType, spName, parms);

        //    Parallel.ForEach(dtCollection.AsEnumerable(), d =>
        //    {
        //        IEntity e = (IEntity)Activator.CreateInstance<T>();
        //        e.LoadFrom(d);
        //        collection.Add((T)e);
        //    });

        //    return collection.ToList<T>();

        //}
        #endregion

        #region " Virtual Method "
        public virtual void Dispose()
        {
            if (_dataContext != null)
                _dataContext = null;
        }
        #endregion
    }
}
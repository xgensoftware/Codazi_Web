using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using com.Xgensoftware.DAL;
using Codazi.Entities;
using Codazi.Helper;

namespace Codazi.Repository
{
    public class PurchaseRepository : BaseRepository
    {
        public PurchaseRepository() :base()
        {
            
            
        }

        public void SaveTransaction(Company c, Contact ct, Purchase p, string item, string sn, int quantity)
        {
            _params = new List<SQLParam>();
            _params.Add(SQLParam.GetParam("@companyName",c.CompanyName));
            _params.Add(SQLParam.GetParam("@firstName", ct.FirstName));
            _params.Add(SQLParam.GetParam("@lastName", ct.LastName));
            _params.Add(SQLParam.GetParam("@phone", ct.Phone));
            _params.Add(SQLParam.GetParam("@email", ct.Email));
            _params.Add(SQLParam.GetParam("@transId", p.TransactionId));
            _params.Add(SQLParam.GetParam("@receiptId", p.ReceiptId));
            _params.Add(SQLParam.GetParam("@status", p.Status));
            _params.Add(SQLParam.GetParam("@quantity", quantity));
            _params.Add(SQLParam.GetParam("@licenseType", "FULL"));
            _params.Add(SQLParam.GetParam("@license", sn));
            _params.Add(SQLParam.GetParam("@item", item));
            _params.Add(SQLParam.GetParam("@rawData", p.RawData));

            try
            {
                this._dataContext.ExecuteNonQuery("sp_SavePurchaseTransaction", _params);
            }
            catch(Exception e)
            {

                LogHelper.Instance.LogErrorMessage(string.Format("Failed to save transaction {0}.  Transaction detail: {1}. ERROR: {2}",p.TransactionId, p.RawData, e.Message));
            }

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Codazi.Repository;
namespace Codazi.Entities
{
    public class Purchase : BaseEntity
    {
        #region Member Variables
        PurchaseRepository _repo = new PurchaseRepository();
        private string _receiptId = string.Empty;
        private string _tranId = string.Empty;
        private string _status = string.Empty;
        private string _rawData = string.Empty;
        #endregion

        #region  Public Properties

        public string ReceiptId { get { return _receiptId; } }
        public string TransactionId { get { return _tranId; } }
        public string Status { get { return _status; } }

        public string RawData { get { return _rawData; } }

        #endregion

        public Purchase(string rcptId, string tranId, string status, string raw)
        {

            _receiptId = rcptId;
            _tranId = tranId;
            _status = status;
            _rawData = raw;
            _id = -1;


        }

        public void Save(Company c, Contact ct, string sn, int quantity,string itemId)
        {
            _repo.SaveTransaction(c,ct,this,itemId,sn,quantity);
        }
    }
}